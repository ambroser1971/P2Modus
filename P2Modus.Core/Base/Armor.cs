using System.Collections.Generic;
using P2Modus.Core.Conditions;

namespace P2Modus.Core
{
    public class Armor : BaseEntity, IArmor
    {
        private bool _isEquipped;

        public ArmorCategory Category { get; set; }

        public ArmorGroup Group { get; set; }

        public int DexterityCap { get; set; }

        public int CheckPenalty { get; set; }

        public int SpeedPenalty { get; set; }

        public int Strength { get; set; }

        public int Hardness { get; set; }

        public decimal Bulk { get; set; }

        public bool IsEquipped 
        { 
            get { return _isEquipped; } 
            set 
            {
                if(EntityAffected != null)
                {
                    if(value == true)
                    {
                        Apply(EntityAffected);
                    }
                    else
                    {
                        Remove(EntityAffected);
                    }
                    return;
                }
                _isEquipped = value;
            }
        }

        public IEnumerable<ITrait> Traits { get; set; }

        public IModifierBag Modifiers { get; set; }

        public IList<ICondition> Conditions { get; set; }

        public Armor (string name, string description, ArmorCategory category, int acBonus)
            :base(name, description, EntityIds.ARMOR_ID)
        {
            Category = category;
            Conditions = new List<ICondition>();
            Modifiers = new ModifierBag();
            Modifiers.Add(new Modifier(){ Type = ModifierType.Item, Value = acBonus });
        }

        public override void Apply(IEntity entity) 
        {
            if(entity is ICharacter character)
            {
                character.WornArmor = this;
                IsEquipped = true;
            }
        }

        public override void Remove(IEntity entity) 
        {
            if(entity is ICharacter character)
            {
                character.WornArmor = new Armor("None", "No armor worn.", ArmorCategory.Unarmored, 0);
                IsEquipped = false;
            }
        }

        protected override void SetupAppliesToList() 
        {
            _appliesToList.Add(EntityIds.AC_ID);
        }
    }
}