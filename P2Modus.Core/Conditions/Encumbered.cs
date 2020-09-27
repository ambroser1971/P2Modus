using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Encumbered condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=12
    //
    // The following is automated:
    //      * Addition of clumsy 1 condition when encumbered.
    //      * Removal of clumsy 1 condition when unencumbered.
    //
    // The following must be implemented by the GM during play:
    //      * 10-foot penaly to all your speeds (minimum Speed of 5 feet).
    public class Encumbered : ConditionBase
    {
        public Encumbered() 
            : base(DefaultName, DefaultDescription, EntityIds.ENCUMBERED_CONDITION_ID)
        {
        }

        public Encumbered(string name, string description)
            :base(name, description, EntityIds.ENCUMBERED_CONDITION_ID)
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
                    var clumsy = character.Conditions.FirstOrDefault(c => c.Id == EntityIds.CLUMSY_CONDITION_ID);
                    if(clumsy == null)
                    {
                        character.Conditions.Add(new Clumsy(1));
                    }
                    character.Conditions.Add(this);
                    return true;
                }
                throw new ConditionExistsException(this, character.Conditions.First(c => c.Id == Id));
            }
            return false;
        }

        protected override bool CustomRemove(IEntity entity)
        {
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.ENCUMBERED_CONDITION_ID))
            {
                var clumsy = character.Conditions.FirstOrDefault(c => c.Id == EntityIds.CLUMSY_CONDITION_ID);
                if(clumsy != null && clumsy.Level == 1)
                {
                    clumsy.Remove(character);
                }
                var encumbered = character.Conditions.First(c => c.Id == EntityIds.ENCUMBERED_CONDITION_ID); 
                character.Conditions.Remove(encumbered);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Encumbered"; }

        private static string DefaultDescription 
        { 
            get => "You are carrying more weight than you can manage. While you’re encumbered, you’re clumsy 1 and " +
                   "take a 10-foot penalty to all your Speeds. As with all penalties to your Speed, this can’t reduce " +
                   "your Speed below 5 feet."; 
        }
    }    
}