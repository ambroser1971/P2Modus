using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Dying condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=11
    //
    // The following is automated:
    //      * Incrementing of Dying level by existing Wounded level on apply.
    //      * Adding of Wounded or incrementing of Wounded leve lon remove.
    public class Dying : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.DeathAndDying; }

        public Dying(int level) 
            : base(DefaultName, DefaultDescription, EntityIds.DYING_CONDITION_ID)
        {
            _conditionLevel = level;
        }

        public Dying(int level, string name, string description)
            :base(name, description, EntityIds.DYING_CONDITION_ID)
        {
            _conditionLevel = level;
        }

        public override bool SupportsLevel { get => true; }
        
        protected override void SetupAppliesToList()
        {
        }

        protected override bool CustomApply(IEntity entity)
        {
            if(entity is ICharacter character)
            {
                if(!character.Conditions.Any(c => c.Id == Id))
                {
                    var wounded = character.Conditions.FirstOrDefault(c => c.Id == EntityIds.WOUNDED_CONDITION_ID);
                    if(wounded != null)
                    {
                        Level += wounded.Level;
                    }

                    character.Conditions.Add(this);                    
                    var unconscious = character.Conditions.FirstOrDefault(c => c.Id == EntityIds.UNCONSCIOUS_CONDITION_ID);
                    if(unconscious == null)
                    {
                        character.Conditions.Add(new Unconscious());
                    }
                    return true;
                }
                throw new ConditionExistsException(this, character.Conditions.First(c => c.Id == Id));
            }
            return false;
        }

        protected override bool CustomRemove(IEntity entity)
        {
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == Id))
            {
                var wounded = character.Conditions.FirstOrDefault(c => c.Id == EntityIds.WOUNDED_CONDITION_ID);
                if(wounded == null)
                {
                    wounded = new Wounded(1);
                    wounded.Apply(character);
                }
                else
                {
                    wounded.Level++;
                }
                
                var condition = character.Conditions.First(c => c.Id == Id); 
                character.Conditions.Remove(condition);
                
                var unconscious = character.Conditions.FirstOrDefault(c => c.Id == EntityIds.UNCONSCIOUS_CONDITION_ID);
                if(unconscious != null)
                {
                    character.Conditions.Remove(unconscious);
                }
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Dying"; }

        private static string DefaultDescription 
        { 
            get => "You are bleeding out or otherwise at death’s door. While you have this condition, you are unconscious. " +
                   "Dying always includes a value, and if it ever reaches dying 4, you die. If you’re dying, you must " +
                   "attempt a recovery check at the start of your turn each round to determine whether you get better or " +
                   "worse. Your dying condition increases by 1 if you take damage while dying, or by 2 if you take damage " +
                   "from an enemy’s critical hit or a critical failure on your save\n\n" +
                   "If you lose the dying condition by succeeding at a recovery check and are still at 0 Hit Points, you " +
                   "remain unconscious, but you can wake up as described in that condition. You lose the dying condition " +
                   "automatically and wake up if you ever have 1 Hit Point or more. Any time you lose the dying condition, " +
                   "you gain the wounded 1 condition, or increase your wounded condition value by 1 if you already have that " +
                   "condition."; 
        }
    }    
}