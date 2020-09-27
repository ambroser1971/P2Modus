using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Invisible condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=26
    //
    // This condition must be manually applied during gameplay.
    public class Invisible : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.Senses; }
        
        public Invisible() 
            : base(DefaultName, DefaultDescription, EntityIds.INVISIBLE_CONDITION_ID)
        {
        }

        public Invisible(string name, string description)
            :base(name, description, EntityIds.INVISIBLE_CONDITION_ID)
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.INVISIBLE_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.INVISIBLE_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Invisible"; }

        private static string DefaultDescription 
        { 
            get => "While invisible, you can’t be seen. You’re undetected to everyone. Creatures can Seek to attempt to detect " +
                   "you; if a creature succeeds at its Perception check against your Stealth DC, you become hidden to that " +
                   "creature until you Sneak to become undetected again. If you become invisible while someone can already see " +
                   "you, you start out hidden to the observer (instead of undetected) until you successfully Sneak. You can’t " +
                   "become observed while invisible except via special abilities or magic."; 
        }
    }    
}