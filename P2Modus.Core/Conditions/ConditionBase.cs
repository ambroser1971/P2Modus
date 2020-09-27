using System;
using System.Collections.Generic;
using System.Linq;

namespace P2Modus.Core.Conditions
{
    public abstract class ConditionBase : BaseEntity, ICondition
    {
        protected int _conditionLevel;

        public virtual int Level
        {
            get { return _conditionLevel; }
            set 
            {
                if(SupportsLevel)
                {
                    _conditionLevel = value;
                    FireConditionLevelAdjustedEvent(this);
                }
            }
        }

        public virtual ConditionGroup Group { get => ConditionGroup.None; }

        public virtual bool SupportsLevel { get => false; }

        public IModifier Modifier { get; protected set; }

        public virtual bool DoesReduceAtEndOfTurn { get => false; }

        public virtual int ReductionAmount { get => 0; }

        public override void Apply(IEntity entity)
        {
            if(CustomApply(entity))
            {
                EntityAffected = entity;
                FireConditionAddedEvent(this);
            }
        }

        protected abstract bool CustomApply(IEntity entity);

        public override void Remove(IEntity entity)
        {
            if(CustomRemove(entity))
            {
                EntityAffected = null;
                FireConditionRemovedEvent(this);
            }
        }

        protected abstract bool CustomRemove(IEntity entity);

        public virtual void EndTurn()
        {}

        public override string ToString() => Name;
        
        public ConditionBase(string name, string description, int conditionId)
            :base(name, description, conditionId)
        { }
        
        public event ConditionAddedEventHandler ConditionAddedEvent;

        public event ConditionRemovedEventHandler ConditionRemovedEvent;

        public event ConditionLevelAdjustedEventHandler ConditionLevelAdjustedEvent;
        
        protected void FireConditionLevelAdjustedEvent(ICondition condition)
        {
            var args = new ConditionEventArgs(condition);
            var raiseEvent = ConditionLevelAdjustedEvent;
            if(raiseEvent != null)
            {
                raiseEvent(this, args);
            }
        }

        protected void FireConditionAddedEvent(ICondition condition)
        {
            var args = new ConditionEventArgs(condition);
            var raiseEvent = ConditionAddedEvent;
            if(raiseEvent != null)
            {
                raiseEvent(this, args);
            }
        }

        protected void FireConditionRemovedEvent(ICondition condition)
        {
            var args = new ConditionEventArgs(condition);
            var raiseEvent = ConditionRemovedEvent;
            if(raiseEvent != null)
            {
                raiseEvent(this, args);
            }
        }        
    }
}