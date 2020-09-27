using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Helpful condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=21
    //
    // This condition must be manually applied during gameplay.
    public class Helpful : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.Attitude; }
        
        public Helpful() 
            : base(DefaultName, DefaultDescription, EntityIds.HELPFUL_CONDITION_ID)
        {
        }

        public Helpful(string name, string description)
            :base(name, description, EntityIds.HELPFUL_CONDITION_ID)
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.HELPFUL_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.HELPFUL_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Helpful"; }

        private static string DefaultDescription 
        { 
            get => "This condition reflects a creature’s disposition toward a particular character, and only supernatural effects " +
                   "(like a spell) can impose these conditions on player characters. A creature that is helpful to a character " +
                   "wishes to actively aid that character. It will accept reasonable Requests from that character, as long as such " +
                   "requests aren’t at the expense of the helpful creature’s goals or quality of life. If the character or one of " +
                   "their allies uses a hostile action against the creature, the creature gains a worse attitude condition " +
                   "depending on the severity of the hostile action, as determined by the GM."; 
        }
    }    
}