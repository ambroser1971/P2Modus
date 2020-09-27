using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Observed condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=27
    //
    // This condition must be manually applied during gameplay.
    public class Observed : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.DegreesOfDetection; }
        
        public Observed() 
            : base(DefaultName, DefaultDescription, EntityIds.OBSERVED_CONDITION_ID)
        {
        }

        public Observed(string name, string description)
            :base(name, description, EntityIds.OBSERVED_CONDITION_ID)
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.OBSERVED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.OBSERVED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Observed"; }

        private static string DefaultDescription 
        { 
            get => "Anything in plain view is observed by you. If a creature takes measures to avoid detection, such as by " +
            "using Stealth to Hide, it can become hidden or undetected instead of observed. If you have another precise " +
            "sense instead of or in addition to sight, you might be able to observe a creature or object using that sense " +
            "instead. You can observe a creature only with precise senses. When Seeking a creature using only imprecise " +
            "senses, it remains hidden, rather than observed."; 
        }
    }    
}