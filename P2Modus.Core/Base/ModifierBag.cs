using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace P2Modus.Core
{
    public class ModifierBag : IModifierBag
    {
        public IModifier this[int index] { get => _bag[index]; set => _bag[index] = value; }

        private List<IModifier> _bag = new List<IModifier>();

        public void Add(IModifier modifier) => _bag.Add(modifier);

        public Guid Add(ModifierType type, int value)
        {
            var mod = new Modifier() { Type = type, Value = value };
            Add(mod);
            return mod.Id;
        }

        public bool Contains(IModifier modifier) => _bag.Contains(modifier);

        public void CopyTo(IModifier[] array, int index) => _bag.CopyTo(array, index);

        public int Count { get => _bag.Count; }

        public IEnumerator GetEnumerator()  => _bag.GetEnumerator();

        IEnumerator<IModifier> IEnumerable<IModifier>.GetEnumerator() => _bag.GetEnumerator();

        public int IndexOf(IModifier modifier) => _bag.IndexOf(modifier);

        public void Insert(int index, IModifier modifier) => _bag.Insert(index, modifier);

        public bool IsReadOnly => false; 

        public bool Remove(IModifier modifier) => _bag.Remove(modifier);

        public bool Remove(Guid id)
        {
            var modifier = _bag.SingleOrDefault(m => m.Id == id);
            if(modifier != null)
                return Remove(modifier);
            return false;
        }

        public int Remove(ModifierType type) 
        {
            var buffer = new List<IModifier>();
            foreach(var mod in _bag.FindAll(m => m.Type == type))
                buffer.Add(mod);
            
            foreach(var mod in buffer)
                _bag.Remove(mod);

            return buffer.Count;
        }

        public void RemoveAt(int index) => _bag.RemoveAt(index);

        public void Clear() => _bag.Clear();

        public int GetTotalModifiers()
        {
            var abilityBonus = GetMaxValue(_bag.Where(m => m.Type == ModifierType.Ability && m.Value >= 0));
            var circumstanceBonus = GetMaxValue(_bag.Where(m => m.Type == ModifierType.Circumstance && m.Value >= 0));
            var itemBonus = GetMaxValue(_bag.Where(m => m.Type == ModifierType.Item && m.Value >= 0));
            var profBonus = GetMaxValue(_bag.Where(m => m.Type == ModifierType.Proficiency && m.Value >= 0));
            var statusBonus = GetMaxValue(_bag.Where(m => m.Type == ModifierType.Status && m.Value >= 0));
            
            var abilityPenalty = GetMinValue(_bag.Where(m => m.Type == ModifierType.Ability && m.Value <= 0));
            var circumstancePenalty =GetMinValue(_bag.Where(m => m.Type == ModifierType.Circumstance && m.Value <= 0));
            var itemPenalty = GetMinValue(_bag.Where(m => m.Type == ModifierType.Item && m.Value <= 0));
            var statusPenalty = GetMinValue(_bag.Where(m => m.Type == ModifierType.Status && m.Value <= 0));
            var untypedPenalty = GetUntypedPenaltyValue();
            
            return abilityBonus + circumstanceBonus + itemBonus + profBonus + statusBonus +
                abilityPenalty + circumstancePenalty + itemPenalty + statusPenalty + untypedPenalty;
        }

        private int GetMaxValue(IEnumerable<IModifier> modifiers)
        {
            if(modifiers.Count() == 0)
                return 0;
            return modifiers.Max(m => m.Value);
        }

        private int GetMinValue(IEnumerable<IModifier> modifiers)
        {
            if(modifiers.Count() == 0)
                return 0;
            return modifiers.Min(m => m.Value);
        }

        private int GetUntypedPenaltyValue()
        {
            int value = 0;
            foreach(var mod in _bag.Where(m => m.Type == ModifierType.UntypedPenalty && m.Value <= 0))
            {
                value += mod.Value;
            }
            return value;
        }
    }
}