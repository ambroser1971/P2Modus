using System;
using System.Collections.Generic;

namespace P2Modus.Core
{
    public abstract class BaseEntity : IEntity
    {
        protected List<int> _appliesToList = new List<int>();

        public virtual IEntity EntityAffected { get; set; }

        public int Id { get; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public IReadOnlyList<int> AppliesTo { get  => _appliesToList; }

        public BaseEntity(string name, string description, int id)
        {
            Id = id;
            Name = name;
            Description = description;
            SetupAppliesToList();
        }

        public abstract void Apply(IEntity entity);
        
        public virtual void Apply(IEnumerable<IEntity> entities)
        {
            foreach(var entity in entities)
            {                
                Apply(entity);
            }
        }

        public abstract void Remove(IEntity entity);

        public virtual void Remove(IEnumerable<IEntity> entities)
        {
            foreach(var entity in entities)
            {                
                Remove(entity);
            }
        }

        protected abstract void SetupAppliesToList();
    }
}