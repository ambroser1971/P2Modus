using System;
using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Drained condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=6
    //
    // This condition must be manually applied during gameplay.
    public class Drained : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.LoweredAbilities; }
        
        public override int Level
        {
            get => base.Level;
            set
            {
                if(value < 1)
                {
                    throw new ArgumentOutOfRangeException();
                }
                var levelChange = value -_conditionLevel;
                if(levelChange > 0)
                {
                    ApplyDrainedDamage((ICharacter)EntityAffected, levelChange);
                }
                else
                {
                    RestoreMaxHitPoints((ICharacter)EntityAffected, levelChange * -1);
                }
                _conditionLevel = value;
                FireConditionLevelAdjustedEvent(this);
            }
        }
        public override bool SupportsLevel { get => true; }

        public Drained(int level) 
            : base(DefaultName, DefaultDescription, EntityIds.DRAINED_CONDITION_ID)
        {
            _conditionLevel = level;
        }

        public Drained(int level, string name, string description)
            :base(name, description, EntityIds.DRAINED_CONDITION_ID)
        {
            _conditionLevel = level;
        }

        protected override void SetupAppliesToList()
        {
            _appliesToList.Add(EntityIds.MAX_HP_ID);
        }

        protected override bool CustomApply(IEntity entity)
        {
            if(entity is ICharacter character)
            {
                var condition = character.Conditions.FirstOrDefault(c => c.Id == Id);
                if(condition == null)
                {
                    condition = this;
                    character.Conditions.Add(this);
                    EntityAffected = character;
                    ApplyDrainedDamage(character, this.Level);
                    return true;
                }
               throw new ConditionExistsException(this, condition);
            }
            return false;
        }

        protected override bool CustomRemove(IEntity entity)
        {            
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == Id))
            {
                var condition = character.Conditions.First(c => c.Id == Id); 
                RestoreMaxHitPoints(character, Level);
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, Level);
        }

        private void ApplyDrainedDamage(ICharacter character, int level)
        {
            var damage = level * character.Level;
            if(damage > 0)
            {
                character.HitPoints.Current -= damage;
                character.HitPoints.ModifiedMax -= damage;
                if(character.HitPoints.Current <= 0)
                {
                    character.HitPoints.Current = 0;
                    var dying = character.Conditions.FirstOrDefault(c => c.Id == EntityIds.DYING_CONDITION_ID);
                    if(dying == null)
                    {
                        dying = new Dying(1);
                        dying.Apply(character);
                    }
                }
            }
        }

        private void RestoreMaxHitPoints(ICharacter character, int levelChange)
        {
            var restoreValue = levelChange * character.Level;
            if(restoreValue > 0)
            {
                var newValue = character.HitPoints.ModifiedMax + restoreValue;
                if(newValue > character.HitPoints.Max)
                {
                    newValue = character.HitPoints.Max;
                }
                character.HitPoints.ModifiedMax = newValue;
            }
        }

        private static string DefaultName { get => "Drained"; }

        private static string DefaultDescription 
        { 
            get => "When a creature successfully drains you of blood or life force, you become less healthy. Drained always " +
                   "includes a value. You take a status penalty equal to your drained value on Constitution-based checks, such " +
                   "as Fortitude saves. You also lose a number of Hit Points equal to your level (minimum 1) times the drained " +
                   "value, and your maximum Hit Points are reduced by the same amount. For example, if you’re hit by an effect " +
                   "that inflicts drained 3 and you’re a 3rd-level character, you lose 9 Hit Points and reduce your maximum Hit " +
                   "Points by 9. Losing these Hit Points doesn’t count as taking damage.\n\n" + 
                   "Each time you get a full night’s rest, your drained value decreases by 1. This increases your maximum Hit " +
                   "Points, but you don’t immediately recover the lost Hit Point"; 
        }
    }    
}