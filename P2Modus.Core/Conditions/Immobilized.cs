using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Immobilized condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=24
    //
    // This condition must be manually applied during gameplay.
    public class Immobilized : ConditionBase
    {
        public Immobilized() 
            : base(DefaultName, DefaultDescription, EntityIds.IMMOBILIZED_CONDITION_ID)
        {
        }

        public Immobilized(string name, string description)
            :base(name, description, EntityIds.IMMOBILIZED_CONDITION_ID)
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.IMMOBILIZED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.IMMOBILIZED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Immobilized"; }

        private static string DefaultDescription 
        { 
            get => "You can’t use any action with the move trait. If you’re immobilized by something holding you in place and an " +
                   "external force would move you out of your space, the force must succeed at a check against either the DC of " +
                   "the effect holding you in place or the relevant defense (usually Fortitude DC) of the monster holding you in " +
                   "place."; 
        }
    }    
}