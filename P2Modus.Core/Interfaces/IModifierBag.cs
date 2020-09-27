using System;
using System.Collections.Generic;

namespace P2Modus.Core
{
    public interface IModifierBag : IList<IModifier>
    {
        Guid Add(ModifierType type, int value);
        bool Remove (Guid Id);
        int Remove(ModifierType type);
        int GetTotalModifiers();
    }

}