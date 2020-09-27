using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Unnoticed condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=41
    //
    // This condition must be manually applied during gameplay.
    public class Unnoticed : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.DegreesOfDetection; }
        
        public Unnoticed() 
            : base(DefaultName, DefaultDescription, EntityIds.UNNOTICED_CONDITION_ID)
        {
        }

        public Unnoticed(string name, string description)
            :base(name, description, EntityIds.UNNOTICED_CONDITION_ID)
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.UNNOTICED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.UNNOTICED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Unnoticed"; }

        private static string DefaultDescription 
        { 
            get => "If you are unnoticed by a creature, that creature has no idea you are present at all. When you’re unnoticed, " +
            "you’re also undetected by the creature. This condition matters for abilities that can be used only against targets " +
            "totally unaware of your presence."; 
        }
    }    
}