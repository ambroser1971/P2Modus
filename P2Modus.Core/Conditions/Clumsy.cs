using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Clumsy condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=3
    //
    // The following is automated:
    //      * Condition penalty for Dexterity-based DCs and Skill Checks.
    //
    // The following is not automated and must be implemented manually by the GM in play:
    public class Clumsy : ConditionBase
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

        public Clumsy(int value) 
            : base(DefaultName, DefaultDescription, EntityIds.CLUMSY_CONDITION_ID)
        {
            InitializeModifier(value);
        }

        public Clumsy(int value, string name, string description)
            :base(name, description, EntityIds.CLUMSY_CONDITION_ID)
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
            _appliesToList.Add(EntityIds.ATTRIBUTE_BASED_CHECK_DEXTERITY);
        }

        protected override bool CustomApply(IEntity entity)
        {
            if(entity is ICharacter character)
            {
                if(character.Conditions.Any(c => c.Id == EntityIds.CLUMSY_CONDITION_ID))
                {
                    throw new ConditionExistsException(this, character.Conditions.First(c => c.Id == EntityIds.CLUMSY_CONDITION_ID));
                }
                
                character.Conditions.Add(this);
                return true;
            }
            return false;
        }

        protected override bool CustomRemove(IEntity entity)
        {
            
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.CLUMSY_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.CLUMSY_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Clumsy"; }

        private static string DefaultDescription 
        { 
            get => "Your movements become clumsy and inexact. Clumsy always includes a value. You take a status penalty equal " +
                   "to the condition value to Dexterity-based checks and DCs, including AC, Reflex saves, ranged attack rolls, " +
                   "and skill checks using Acrobatics, Stealth, and Thievery."; 
        }
    }    
}