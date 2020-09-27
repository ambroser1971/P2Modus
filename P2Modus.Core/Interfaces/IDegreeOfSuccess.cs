using System.Collections.Generic;

namespace P2Modus.Core
{
    public interface IDegreeOfSuccess : IEntity
    {
        CheckResult Result { get; set; }

        IEnumerable<IModifier> AddedModifier { get; set; }

    }
}