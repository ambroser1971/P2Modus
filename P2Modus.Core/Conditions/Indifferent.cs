using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Indifferent condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=25
    //
    // This condition must be manually applied during gameplay.
    public class Indifferent : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.Attitude; }
        
        public Indifferent() 
            : base(DefaultName, DefaultDescription, EntityIds.INDIFFERENT_CONDITION_ID)
        {
        }

        public Indifferent(string name, string description)
            :base(name, description, EntityIds.INDIFFERENT_CONDITION_ID)
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.INDIFFERENT_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.INDIFFERENT_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Indifferent"; }

        private static string DefaultDescription 
        { 
            get => "This condition reflects a creature’s disposition toward a particular character, and only supernatural effects " +
                   "(like a spell) can impose these conditions on player characters. A creature that is indifferent to a character " +
                   "doesn’t really care one way or the other about that character. Assume a creature’s attitude to a given character " +
                   "is indifferent unless specified otherwise."; 
        }
    }    
}