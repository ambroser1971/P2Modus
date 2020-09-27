using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Dazzled condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=7
    //
    // This condition must be manually applied during gameplay.
    public class Dazzled : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.Senses; }
        
        public Dazzled() 
            : base(DefaultName, DefaultDescription, EntityIds.DAZZLED_CONDITION_ID)
        {
        }

        public Dazzled(string name, string description)
            :base(name, description, EntityIds.DAZZLED_CONDITION_ID)
        {
        }

        protected override void SetupAppliesToList()
        {
        }

        protected override bool CustomApply(IEntity entity)
        {
            if(entity is ICharacter character)
            {
                if(!character.Conditions.Any(c => c.Id == Id) && !character.Conditions.Any(c => c.Id == EntityIds.BLINDED_CONDITION_ID))
                {                    
                    character.Conditions.Add(this);
                    return true;
                }
            }
            return false;
        }

        protected override bool CustomRemove(IEntity entity)
        {            
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.DAZZLED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.DAZZLED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Dazzled"; }

        private static string DefaultDescription 
        { 
            get => "Your eyes are overstimulated. If vision is your only precise sense, all creatures and objects are concealed " +
                   "from you."; 
        }
    }    
}