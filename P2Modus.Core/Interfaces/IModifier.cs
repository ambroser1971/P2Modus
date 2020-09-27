using System;

namespace P2Modus.Core
{
    public interface IModifier
    {
        Guid Id { get; }
        ModifierType Type { get; set; }
        int Value { get; set; }

    }    
}