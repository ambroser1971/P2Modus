using System.Collections.Generic;
using P2Modus.Core.Conditions;

namespace P2Modus.Core
{
    public interface IObject : IEntity
    {
        IEnumerable<ITrait> Traits { get; set; }

        IModifierBag Modifiers { get; set; }

        IList<ICondition> Conditions { get; set; }

        decimal Bulk { get; set; }

        int Hardness { get; set; }
    }
}