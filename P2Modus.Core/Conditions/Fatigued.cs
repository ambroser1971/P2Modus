using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Fatigued condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=15
    //
    // The following is automated:
    //      * -1 status penalty to AC and saving throws.
    //
    // The following must be implemented by GM during gameplay:
    //      * Can't choose the exploration activity while exploring.
    //      * Remove fatigue after a full night's rest.
    public class Fatigued : ConditionBase
    {
        public Fatigued() 
            : base(DefaultName, DefaultDescription, EntityIds.FATIGUED_CONDITION_ID)
        {
        }

        public Fatigued(string name, string description)
            :base(name, description, EntityIds.FATIGUED_CONDITION_ID)
        {
        }

        protected override void SetupAppliesToList()
        {
            Modifier = new Modifier() { Type = ModifierType.Status, Value = -1 };
            _appliesToList.Add(EntityIds.AC_ID);
            _appliesToList.Add(EntityIds.FORT_SAVE_ID);
            _appliesToList.Add(EntityIds.REFLEX_SAVE_ID);
            _appliesToList.Add(EntityIds.WILL_SAVE_ID);
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.FATIGUED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.FATIGUED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Fatigued"; }

        private static string DefaultDescription 
        { 
            get => "You’re tired and can’t summon much energy. You take a –1 status penalty to AC and saving throws. " +
                   "While exploring, you can’t choose an exploration activity.\n\n" +
                   "You recover from fatigue after a full night’s rest."; 
        }
    }    
}