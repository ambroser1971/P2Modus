using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Friendly condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=18
    //
    // This condition must be manually applied during gameplay.
    public class Friendly : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.Attitude; }

        public Friendly() 
            : base(DefaultName, DefaultDescription, EntityIds.FRIENDLY_CONDITION_ID)
        {
        }

        public Friendly(string name, string description)
            :base(name, description, EntityIds.FRIENDLY_CONDITION_ID)
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.FRIENDLY_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.FRIENDLY_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Friendly"; }

        private static string DefaultDescription 
        { 
            get => "This condition reflects a creature’s disposition toward a particular character, and only supernatural effects " +
                   "(like a spell) can impose these conditions on player characters. A creature that is friendly to a character " +
                   "likes that character. The character can attempt to make a Request of a friendly creature, and the friendly " +
                   "creature is likely to agree to a simple and safe request that doesn’t cost it much to fulfill. If the character " +
                   "or one of their allies uses hostile actions against the creature, the creature gains a worse attitude condition " +
                   "depending on the severity of the hostile action, as determined by the GM."; 
        }
    }    
}