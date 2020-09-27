using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Hidden condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=22
    //
    // This condition must be manually applied during gameplay.
    public class Hidden : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.DegreesOfDetection; }
        
        public Hidden() 
            : base(DefaultName, DefaultDescription, EntityIds.HIDDEN_CONDITION_ID)
        {
        }

        public Hidden(string name, string description)
            :base(name, description, EntityIds.HIDDEN_CONDITION_ID)
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.HIDDEN_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.HIDDEN_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Hidden"; }

        private static string DefaultDescription 
        { 
            get => "While you’re hidden from a creature, that creature knows the space you’re in but can’t tell precisely where " +
                   "you are. You typically become hidden by using Stealth to Hide. When Seeking a creature using only imprecise " +
                   "senses, it remains hidden, rather than observed. A creature you’re hidden from is flat-footed to you, and " +
                   "it must succeed at a DC 11 flat check when targeting you with an attack, spell, or other effect or it fails " +
                   "affect you. Area effects aren’t subject to this flat check.\n\n" +
                   "A creature might be able to use the Seek action to try to observe you.";
        }
    }    
}