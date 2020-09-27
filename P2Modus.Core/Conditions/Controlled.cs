using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Controlled condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=6
    //
    // This condition must be manually applied during gameplay.
    public class Controlled : ConditionBase
    {
        public Controlled() 
            : base(DefaultName, DefaultDescription, EntityIds.CONTROLLED_CONDITION_ID)
        {
        }

        public Controlled(string name, string description)
            :base(name, description, EntityIds.CONTROLLED_CONDITION_ID)
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.CONTROLLED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.CONTROLLED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Controlled"; }

        private static string DefaultDescription 
        { 
            get => "Someone else is making your decisions for you, usually because you’re being commanded or magically dominated. " +
                   "The controller dictates how you act and can make you use any of your actions, including attacks, reactions, or " +
                   "even Delay. The controller usually does not have to spend their own actions when controlling you."; 
        }
    }    
}