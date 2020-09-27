using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Slowed condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=35
    //
    // This condition must be manually applied during gameplay.
    public class Slowed : ConditionBase
    {
        public Slowed() 
            : base(DefaultName, DefaultDescription, EntityIds.SLOWED_CONDITION_ID)
        {
        }

        public Slowed(string name, string description)
            :base(name, description, EntityIds.SLOWED_CONDITION_ID)
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.SLOWED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.SLOWED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Slowed"; }

        private static string DefaultDescription 
        { 
            get => "You have fewer actions. Slowed always includes a value. When you regain your actions at the start of " +
            "your turn, reduce the number of actions you regain by your slowed value. Because slowed has its effect at the " +
            "start of your turn, you don’t immediately lose actions if you become slowed during your turn. "; 
        }
    }    
}