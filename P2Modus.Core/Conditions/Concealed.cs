using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Concealed condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=4
    //
    // This condition must be manually applied during gameplay.
    public class Concealed : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.Senses; }
        
        public Concealed() 
            : base(DefaultName, DefaultDescription, EntityIds.CONCEALED_CONDITION_ID)
        {
        }

        public Concealed(string name, string description)
            :base(name, description, EntityIds.CONCEALED_CONDITION_ID)
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.CONCEALED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.CONCEALED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Concealed"; }

        private static string DefaultDescription 
        { 
            get => "While you are concealed from a creature, such as in a thick fog, you are difficult for that creature to see. You can " +
                   "still be observed, but you’re tougher to target. A creature that you’re concealed from must succeed at a DC 5 flat " +
                   "check when targeting you with an attack, spell, or other effect. Area effects aren’t subject to this flat check. If " +
                   "the check fails, the attack, spell, or effect doesn’t affect you."; 
        }
    }    
}