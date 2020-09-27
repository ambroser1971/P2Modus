using System;

namespace P2Modus.Core
{
    public class Modifier : IModifier
    {
        public Modifier()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }        
        public ModifierType Type { get; set; }
        public int Value { get; set; }
    }
}