using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Fleeing condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=17
    //
    // This condition must be manually applied during gameplay.
    public class Fleeing : ConditionBase
    {
        public Fleeing() 
            : base(DefaultName, DefaultDescription, EntityIds.FLEEING_CONDITION_ID)
        {
        }

        public Fleeing(string name, string description)
            :base(name, description, EntityIds.FLEEING_CONDITION_ID)
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.FLEEING_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.FLEEING_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Fleeing"; }

        private static string DefaultDescription 
        { 
            get => "You’re forced to run away due to fear or some other compulsion. On your turn, you must spend each of your " +
                   "actions trying to escape the source of the fleeing condition as expediently as possible (such as by using " +
                   "move actions to flee, or opening doors barring your escape). The source is usually the effect or caster " +
                   "that gave you the condition, though some effects might define something else as the source. You can’t " +
                   "Delay or Ready while fleeing."; 
        }
    }    
}