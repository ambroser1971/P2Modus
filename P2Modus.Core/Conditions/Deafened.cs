using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Deafened condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=8
    //
    // The following is implemented:
    //      * Status penalty of -2 to Perception based initiative checks.
    //
    // The following must be implemented manually by the GM during gameplay.
    //      * When performing actions with the auditory trait, you must succeed at a DC 5 flat check or the action is lost.
    //      * You are immune to auditory effects.
    public class Deafened : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.Senses; }
        
        public Deafened() 
            : base(DefaultName, DefaultDescription, EntityIds.DEAFENED_CONDITION_ID)
        {
        }

        public Deafened(string name, string description)
            :base(name, description, EntityIds.DEAFENED_CONDITION_ID)
        {
        }

        protected override void SetupAppliesToList()
        {
            _appliesToList.Add(EntityIds.PERCEPTION_BASED_INITIATIVE_ID);
            Modifier = new Modifier() { Type = ModifierType.Status, Value = -2 };
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.DEAFENED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.DEAFENED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Deafened"; }

        private static string DefaultDescription 
        { 
            get => "You can’t hear. You automatically critically fail Perception checks that require you to be able to hear. " +
                   "You take a –2 status penalty to Perception checks for initiative and checks that involve sound but also " +
                   "rely on other senses. If you perform an action with the auditory trait, you must succeed at a DC 5 flat " + 
                   "check or the action is lost; attempt the check after spending the action but before any effects are applied. " +
                   "You are immune to auditory effects. "; 
        }
    }    
}