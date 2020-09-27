using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Undetected condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=39
    //
    // This condition must be manually applied during gameplay.
    public class Undetected : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.DegreesOfDetection; }
        
        public Undetected() 
            : base(DefaultName, DefaultDescription, EntityIds.UNDETECTED_CONDITION_ID)
        {
        }

        public Undetected(string name, string description)
            :base(name, description, EntityIds.UNDETECTED_CONDITION_ID)
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.UNDETECTED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.UNDETECTED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Undetected"; }

        private static string DefaultDescription 
        { 
            get => "When you are undetected by a creature, that creature cannot see you at all, has no idea what space you " +
                   "occupy, and can’t target you, though you still can be affected by abilities that target an area. When you’re " +
                   "undetected by a creature, that creature is flat-footed to you.\n\n" +
                   "A creature you’re undetected by can guess which square you’re in to try targeting you. It must pick a square " +
                   "and attempt an attack. This works like targeting a hidden creature (requiring a DC 11 flat check), but the " +
                   "flat check and attack roll are rolled in secret by the GM, who doesn’t reveal whether the attack missed due " +
                   "to failing the flat check, failing the attack roll, or choosing the wrong square.\n\n" +
                   "A creature can use the Seek action to try to find you."; 
        }
    }    
}