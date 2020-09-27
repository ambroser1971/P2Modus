using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Quickened condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=32
    //
    // This condition must be manually applied during gameplay.
    public class Quickened : ConditionBase
    {
        public Quickened() 
            : base(DefaultName, DefaultDescription, EntityIds.QUICKENED_CONDITION_ID)
        {
        }

        public Quickened(string name, string description)
            :base(name, description, EntityIds.QUICKENED_CONDITION_ID)
        {
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.QUICKENED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.QUICKENED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Quickened"; }

        private static string DefaultDescription 
        { 
            get => "You gain 1 additional action at the start of your turn each round. Many effects that make you quickened " +
                   "specify the types of actions you can use with this additional action. If you become quickened from multiple " +
                   "sources, you can use the extra action you’ve been granted for any single action allowed by any of the " +
                   "effects that made you quickened. Because quickened has its effect at the start of your turn, you don’t " +
                   "immediately gain actions if you become quickened during your turn."; 
        }
    }    
}