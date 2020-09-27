using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Enfeebled condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=13
    //
    // The following is automated:
    //      * Condition penalty for Strength-based DCs and Skill Checks.
    //
    // The following is not automated and must be implemented manually by the GM in play:
    public class Enfeebled : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.LoweredAbilities; }
        
        public override int Level {
            get
            {
                return base.Level;
            }
            set
            {
                if(value < 1)
                {
                    throw new System.ArgumentOutOfRangeException("Level must be 1 or higher.");
                }
                Modifier.Value = value * -1;
                _conditionLevel = value;
            }
        }

        public override bool SupportsLevel { get => true; }

        public Enfeebled(int value) 
            : base(DefaultName, DefaultDescription, EntityIds.ENFEEBLED_CONDITION_ID)
        {
            InitializeModifier(value);
        }

        public Enfeebled(int value, string name, string description)
            :base(name, description, EntityIds.ENFEEBLED_CONDITION_ID)
        {
            InitializeModifier(value);
        }

        private void InitializeModifier(int value)
        {
            if(value < 1)
            {
                throw new System.ArgumentOutOfRangeException("Level must be 1 or higher.");
            }
            Modifier = new Modifier() { Type = ModifierType.Status, Value = value * -1};
            _conditionLevel = value;
        }

        protected override void SetupAppliesToList()
        {
            _appliesToList.Add(EntityIds.ATTRIBUTE_BASED_CHECK_STRENGTH);
        }

        protected override bool CustomApply(IEntity entity)
        {
            if(entity is ICharacter character)
            {
                if(character.Conditions.Any(c => c.Id == EntityIds.ENFEEBLED_CONDITION_ID))
                {
                    throw new ConditionExistsException(this, character.Conditions.First(c => c.Id == EntityIds.ENFEEBLED_CONDITION_ID));
                }
                
                character.Conditions.Add(this);
                return true;
            }
            return false;
        }

        protected override bool CustomRemove(IEntity entity)
        {
            
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.ENFEEBLED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.ENFEEBLED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Enfeebled"; }

        private static string DefaultDescription 
        { 
            get => "You’re physically weakened. Enfeebled always includes a value. When you are enfeebled, you take a status " +
                   "penalty equal to the condition value to Strength-based rolls and DCs, including Strength-based melee attack " +
                   "rolls, Strength-based damage rolls, and Athletics checks."; 
        }
    }    
}