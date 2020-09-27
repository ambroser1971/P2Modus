using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Broken condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=2
    //
    // The following is automated:
    //      * Status penalty to AC for light, medium, and heavy armors.
    public class Broken : ConditionBase
    {
        public Broken() 
            : base(DefaultName, DefaultDescription, EntityIds.BROKEN_CONDITION_ID)
        { }

        public Broken(string name, string description)
            :base(name, description, EntityIds.BROKEN_CONDITION_ID)
        { }

        protected override bool CustomApply(IEntity entity)
        {
            if(entity is IObject item)
            {
                if(!item.Conditions.Any(c => c.Id == EntityIds.BROKEN_CONDITION_ID))
                {
                    if(item is IArmor armor)
                    {
                        switch(armor.Category)
                        {
                            case ArmorCategory.Light:
                                this.Modifier = new Modifier() { Type = ModifierType.Status, Value = -1 };
                                _appliesToList.Add(EntityIds.AC_ID);
                                break;
                            case ArmorCategory.Medium:
                                this.Modifier = new Modifier() { Type = ModifierType.Status, Value = -2 };
                                _appliesToList.Add(EntityIds.AC_ID);
                                break;
                            case ArmorCategory.Heavy:
                                this.Modifier = new Modifier() { Type = ModifierType.Status, Value = -3 };
                                _appliesToList.Add(EntityIds.AC_ID);
                                break;                        
                        }
                    }
                    item.Conditions.Add(this);
                    return true;
                }
                throw new ConditionExistsException(this, item.Conditions.First(c => c.Id == EntityIds.BROKEN_CONDITION_ID));
            }
            return false;
        }

        protected override bool CustomRemove(IEntity entity)
        {
            if(entity is IObject item && item.Conditions.Any(c => c.Id == EntityIds.BROKEN_CONDITION_ID))
            {
                var brokenCondition = item.Conditions.First(c => c.Id == EntityIds.BROKEN_CONDITION_ID);
                item.Conditions.Remove(brokenCondition);
                return true;
            }
            return false;
        }

        protected override void SetupAppliesToList()
        {
            // Done in Apply method.
        }

        private static string DefaultName { get => "Broken"; }

        private static string DefaultDescription 
        { 
            get => "Broken is a condition that affects objects. An object is broken when damage has reduced its Hit Points to equal or less than its Broken " +
                   "Threshold. A broken object can’t be used for its normal function, nor does it grant bonuses-with the exception of armor. Broken armor " +
                   "still grants its item bonus to AC, but it so imparts a status penalty to AC depits category: -1 for broken light armor, -2 for broken " +
                   "medium armor, or -3 for broken heavy armor.\n\nA broken item still imposes penalties and limitations normally incurred by carrying, " +
                   "holding, or wearing it. For example, broken armor would still impose its Dexterity modifier cap, check penalty, and so forth.\n\nIf an " +
                   "effect makes an item broken automatically and the item has more HP than its Broken Threshold, that effect also reduces the item’s " +
                   "current HP to the Broken Threshold.";
        }
    }
}