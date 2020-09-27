using System;
using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Stupefied condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=37
    //
    // The following is autmated:
    //      * Status penaly equal to condition level to all Intelligence-, Wisdom-, and Charisma-based checks.
    //
    // The follwing must be enforced by GM:
    //      * Any time you attempt to Cast a Spell while stupefied, the spell is disrupted unless you succeed at 
    //        a flat check with a DC equal to 5 + your stupefied value.
    public class Stupefied : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.LoweredAbilities; }
        
        public override bool SupportsLevel { get => true; }

        public override int Level
        {
            get => base.Level;
            set
            {
                if(value < 1)
                {
                    throw new ArgumentOutOfRangeException("Value must be 1 or higher.");
                }

                Modifier.Value = value * -1;
                base.Level = value;
            }
        }
        public Stupefied(int value) 
            : base(DefaultName, DefaultDescription, EntityIds.STUPEFIED_CONDITION_ID)
        {
            _conditionLevel = value;
            Modifier = new Modifier() { Type = ModifierType.Status, Value = value * -1 };
        }

        public Stupefied(int value, string name, string description)
            :base(name, description, EntityIds.STUPEFIED_CONDITION_ID)
        {
            _conditionLevel = value;
            Modifier = new Modifier() { Type = ModifierType.Status, Value = value * -1 };
        }

        protected override void SetupAppliesToList()
        {
            _appliesToList.Add(EntityIds.ATTRIBUTE_BASED_CHECK_INTELLIGENCE);
            _appliesToList.Add(EntityIds.ATTRIBUTE_BASED_CHECK_WISDOM);
            _appliesToList.Add(EntityIds.ATTRIBUTE_BASED_CHECK_CHARISMA);
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.STUPEFIED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.STUPEFIED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Stupefied"; }

        private static string DefaultDescription 
        { 
            get => "Your thoughts and instincts are clouded. Stupefied always includes a value. You take a status penalty equal " +
                   "to this value on Intelligence-, Wisdom-, and Charisma-based checks and DCs, including Will saving throws, " +
                   "spell attack rolls, spell DCs, and skill checks that use these ability scores. Any time you attempt to Cast a " +
                   "Spell while stupefied, the spell is disrupted unless you succeed at a flat check with a DC equal to 5 + your " +
                   "stupefied value.";
        }
    }    
}