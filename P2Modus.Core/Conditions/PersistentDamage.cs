using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P2Modus.Core.Conditions
{
    // PersistentDamage condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=29
    //
    // The following is automated:
    //      * Persistant damage at the end of the character's turn.    
    public class PersistentDamage : ConditionBase
    {
        private List<Die> _damageDice = new List<Die>();

        private string _toStringLabel;

        public IReadOnlyList<Die> Dice { get { return _damageDice; } }

        public string Type { get; }   

        public PersistentDamage(string type, params Die[] damageDice) 
            : base(DefaultName, DefaultDescription, EntityIds.PERSISTENT_DAMAGE_CONDITION_ID)
        {
            Type = type;
            _damageDice.AddRange(damageDice);
            CreaetToStringLabel();
        }

        public PersistentDamage(string name, string description, string type, params Die[] damageDice)
            :base(name, description, EntityIds.PERSISTENT_DAMAGE_CONDITION_ID)
        {
            Type = type;
            _damageDice.AddRange(damageDice);
            CreaetToStringLabel();
        }

        public override void EndTurn()
        {
            if(EntityAffected is ICharacter character)
            {
                var calculator = new Calculator();
                foreach(var die in _damageDice)
                {
                    if(die.Count > 0 && die.Face > 0)
                    {
                        var roll = calculator.Roll(die.Count, die.Face, new ModifierBag() 
                                { new Modifier() { Type = ModifierType.Status, Value = die.Modifier }}
                            );
                        //TODO: implement immunities, resistances, and weaknesses
                        character.HitPoints.Current -= roll.TotalResult;
                        if(character.HitPoints.Current <= 0)
                        {
                            ApplyDeath(character);
                            break;
                        }
                    }
                }                
            }
        }

        public override string ToString()
        {
            return _toStringLabel;
        }

        private void CreaetToStringLabel()
        {
            var buffer = new StringBuilder();
            foreach(var die in _damageDice)
            {
                if(buffer.Length > 0)
                {
                    buffer.Append("+ ");
                }
                switch(CheckDie(die))
                {
                    case HasDiceAndModifier.HasBoth:
                        buffer.AppendFormat("{0}d{1}+{2} ", die.Count, die.Face, die.Modifier);
                        break;
                    case HasDiceAndModifier.HasDice:
                        buffer.AppendFormat("{0}d{1} ", die.Count, die.Face);
                        break;
                    case HasDiceAndModifier.HasModifier:
                        buffer.AppendFormat("{0} ", die.Modifier);
                        break;
                    case HasDiceAndModifier.HasNone:
                        buffer.Length -= 2; // Remove trailing "+ "
                        break;
                }
            }
            buffer.AppendFormat("Persistent {0} Damage", Type);
            _toStringLabel = buffer.ToString();
        }

        private enum HasDiceAndModifier
        {
            HasNone,
            HasDice,
            HasModifier,
            HasBoth
        }

        private HasDiceAndModifier CheckDie(Die die)
        {
            if(die.Count > 0 && die.Modifier != 0)
            {
                return HasDiceAndModifier.HasBoth;
            }
            if(die.Count > 0)
            {
                return HasDiceAndModifier.HasDice;
            }
            if(die.Modifier > 0)
            {
                return HasDiceAndModifier.HasModifier;
            }
            return HasDiceAndModifier.HasNone;
        }

        private void ApplyDeath(ICharacter character)
        {
            character.HitPoints.Current = 0;
            if(!character.Conditions.Any(c => c.Id == EntityIds.DYING_CONDITION_ID))
            {
                character.Conditions.Add(new Dying(1));
            }
        }

        protected override void SetupAppliesToList()
        {            
        }

        protected override bool CustomApply(IEntity entity)
        {
            if(entity is ICharacter character)
            {
                if(!character.Conditions.Any(c => c.Id == Id && ((PersistentDamage)c).Type == Type))
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.PERSISTENT_DAMAGE_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.PERSISTENT_DAMAGE_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "PersistentDamage"; }

        private static string DefaultDescription 
        { 
            get => "Persistent damage comes from effects like acid, being on fire, or many other situations. It appears as “X persistent " +
            "[type] damage,” where “X” is the amount of damage dealt and “[type]” is the damage type. Instead of taking persistent " +
            "damage immediately, you take it at the end of each of your turns as long as you have the condition, rolling any damage dice " +
            "anew each time. After you take persistent damage, roll a DC 15 flat check to see if you recover from the persistent damage. " +
            "If you succeed, the condition ends."; 
        }
    }    
}