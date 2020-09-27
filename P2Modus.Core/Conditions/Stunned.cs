using System;
using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Stunned condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=36
    //
    // This condition must be manually applied during gameplay.
    public class Stunned : ConditionBase
    {
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
        public Stunned(int level) 
            : base(DefaultName, DefaultDescription, EntityIds.STUNNED_CONDITION_ID)
        {
            _conditionLevel = level;
        }

        public Stunned(int level, string name, string description)
            :base(name, description, EntityIds.STUNNED_CONDITION_ID)
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.STUNNED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.STUNNED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Stunned"; }

        private static string DefaultDescription 
        { 
            get => "You’ve become senseless. You can’t act while stunned. Stunned usually includes a value, which indicates " +
                   "how many total actions you lose, possibly over multiple turns, from being stunned. Each time you regain " +
                   "actions (such as at the start of your turn), reduce the number you regain by your stunned value, then " +
                   "reduce your stunned value by the number of actions you lost. For example, if you were stunned 4, you " +
                   "would lose all 3 of your actions on your turn, reducing you to stunned 1; on your next turn, you would " +
                   "lose 1 more action, and then be able to use your remaining 2 actions normally. Stunned might also have a " +
                   "duration instead of a value, such as “stunned for 1 minute.” In this case, you lose all your actions for " +
                   "the listed duration.\n\n" + 
                   "Stunned overrides slowed. If the duration of your stunned condition ends while you are slowed, you count " +
                   "the actions lost to the stunned condition toward those lost to being slowed. So, if you were stunned 1 " +
                   "and slowed 2 at the beginning of your turn, you would lose 1 action from stunned, and then lose only 1 " +
                   "additional action by being slowed, so you would still have 1 action remaining to use that turn."; 
        }
    }    
}