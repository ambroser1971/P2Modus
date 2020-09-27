using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Hostile condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=23
    //
    // This condition must be manually applied during gameplay.
    public class Hostile : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.Attitude; }
        
        public Hostile() 
            : base(DefaultName, DefaultDescription, EntityIds.HOSTILE_CONDITION_ID)
        {
        }

        public Hostile(string name, string description)
            :base(name, description, EntityIds.HOSTILE_CONDITION_ID)
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.HOSTILE_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.HOSTILE_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Hostile"; }

        private static string DefaultDescription 
        { 
            get => "This condition reflects a creature’s disposition toward a particular character, and only supernatural " +
                   "effects (like a spell) can impose these conditions on player characters. A creature that is hostile to a " +
                   "character actively seeks to harm that character. It doesn’t necessarily attack, but it won’t accept " +
                   "Requests from the character."; 
        }
    }    
}