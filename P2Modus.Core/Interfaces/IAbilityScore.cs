using System;

namespace P2Modus.Core
{
    public interface IAbilityScore : IEntity
    {
        int Value {get; set; }

        IModifier Modifier { get; }
    }
}