using System.Collections.Generic;

namespace P2Modus.Core
{
    public interface ISpeed : IEntity
    {
        int MaxMovement { get; set; }

        int CurrentMaxMovement { get; set; }
    }
}