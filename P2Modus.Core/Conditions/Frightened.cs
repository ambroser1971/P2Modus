using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Frightened condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=19
    //
    // The following is automated:
    //      * Status penalty to all checks and DCs.    
    //
    // The following is not automated and must be implemented manually by the GM in play:
    //      * Decrement of level by 1 at the end of the character's turn.
    public class Frightened : ConditionBase
    {
        public override bool DoesReduceAtEndOfTurn { get => true; }

        public override int ReductionAmount { get => 1; }
        
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

        public Frightened(int value) 
            : base(DefaultName, DefaultDescription, EntityIds.FRIGHTENED_CONDITION_ID)
        {
            InitializeModifier(value);
        }

        public Frightened(int value, string name, string description)
            :base(name, description, EntityIds.FRIGHTENED_CONDITION_ID)
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
            Modifier = new Modifier() { Type = ModifierType.Status, Value = Level * -1 };            
        }

        protected override bool CustomApply(IEntity entity)
        {
            if(entity is ICharacter character)
            {
                if(character.Conditions.Any(c => c.Id == EntityIds.FRIGHTENED_CONDITION_ID))
                {
                    throw new ConditionExistsException(this, character.Conditions.First(c => c.Id == EntityIds.FRIGHTENED_CONDITION_ID));
                }
                
                character.Conditions.Add(this);
                return true;
            }
            return false;
        }

        protected override bool CustomRemove(IEntity entity)
        {
            
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.FRIGHTENED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.FRIGHTENED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Frightened"; }

        private static string DefaultDescription 
        { 
            get => "You’re gripped by fear and struggle to control your nerves. The frightened condition always includes a " +
                   "value. You take a status penalty equal to this value to all your checks and DCs. Unless specified " +
                   "otherwise, at the end of each of your turns, the value of your frightened condition decreases by 1."; 
        }
    }    
}