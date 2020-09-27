using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Sickened condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=34
    //
    // The following is automated:
    //      * Status penalty for all DCs and checks.
    //
    // The following is not automated and must be implemented manually by the GM in play:
    //      * Spend one action retching in an attempt to reduce level of sickened.
    //      * Fortitude save reduces sickened by one level (or two on a cticial success).
    public class Sickened : ConditionBase
    {
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

        public Sickened(int value) 
            : base(DefaultName, DefaultDescription, EntityIds.SICKENED_CONDITION_ID)
        {
            InitializeModifier(value);
        }

        public Sickened(int value, string name, string description)
            :base(name, description, EntityIds.SICKENED_CONDITION_ID)
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
            _appliesToList.Add(EntityIds.ALL_CHECKS);
            _appliesToList.Add(EntityIds.ALL_DCS);
        }

        protected override bool CustomApply(IEntity entity)
        {
            if(entity is ICharacter character)
            {
                if(character.Conditions.Any(c => c.Id == EntityIds.SICKENED_CONDITION_ID))
                {
                    throw new ConditionExistsException(this, character.Conditions.First(c => c.Id == EntityIds.SICKENED_CONDITION_ID));
                }
                
                character.Conditions.Add(this);
                return true;
            }
            return false;
        }

        protected override bool CustomRemove(IEntity entity)
        {
            
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.SICKENED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.SICKENED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Sickened"; }

        private static string DefaultDescription 
        { 
            get => "You feel ill. Sickened always includes a value. You take a status penalty equal to this value on all your " +
                   "checks and DCs. You can’t willingly ingest anything—including elixirs and potions—while sickened.\n\n" +
                   "You can spend a single action retching in an attempt to recover, which lets you immediately attempt a " +
                   "Fortitude save against the DC of the effect that made you sickened. On a success, you reduce your sickened " +
                   "value by 1 (or by 2 on a critical success)."; 
        }
    }    
}