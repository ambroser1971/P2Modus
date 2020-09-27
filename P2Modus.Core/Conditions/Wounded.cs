using System;
using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Wounded condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=42
    //
    // NOTE: Adding and incrementing of Wounded condition level is found in the Dying condition.
    //
    // This condition must be manually applied during gameplay.
    public class Wounded : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.DeathAndDying; }

        public override bool SupportsLevel { get => true; }

        public override int Level
        {
            get => base.Level;
            set
            {
                if(value < 1)
                {
                    throw new ArgumentOutOfRangeException("Value must be 1 or higher.");
                }

                base.Level = value;
            }
        }

        public Wounded(int level) 
            : base(DefaultName, DefaultDescription, EntityIds.WOUNDED_CONDITION_ID)
        {
            _conditionLevel = level;
        }

        public Wounded(int level, string name, string description)
            :base(name, description, EntityIds.WOUNDED_CONDITION_ID)
        {
            _conditionLevel = level;
        }

        protected override void SetupAppliesToList()
        {
        }

        protected override bool CustomApply(IEntity entity)
        {
            if(entity is ICharacter character)
            {
                if(!character.Conditions.Any(c => c.Id == Id))
                {                    
                    character.Conditions.Add(this);
                    return true;
                }
                throw new ConditionExistsException(this, character.Conditions.First(c => c.Id == Id));
            }
            return false;
        }

        protected override bool CustomRemove(IEntity entity)
        {
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.WOUNDED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.WOUNDED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Wounded"; }

        private static string DefaultDescription 
        { 
            get => "You have been seriously injured. If you lose the dying condition and do not already have the wounded " +
            "condition, you become wounded 1. If you already have the wounded condition when you lose the dying condition, " +
            "your wounded condition value increases by 1. If you gain the dying condition while wounded, increase your " +
            "dying condition value by your wounded value.\n\n" +
            "The wounded condition ends if someone successfully restores Hit Points to you with Treat Wounds, or if you " +
            "are restored to full Hit Points and rest for 10 minutes."; 
        }
    }    
}