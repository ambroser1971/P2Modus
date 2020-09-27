using System;
using System.Collections.Generic;

namespace P2Modus.Core
{
    public interface IEntity
    {
        int Id { get; }
        string Name { get; set; }

        string Description { get; set; }

        IReadOnlyList<int> AppliesTo{ get; }

        IEntity EntityAffected { get; set; }

        void Apply(IEntity entity);

        void Apply(IEnumerable<IEntity> entities);

        void Remove(IEntity entity);

        void Remove(IEnumerable<IEntity> entities);
    }
}