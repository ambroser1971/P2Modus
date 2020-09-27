using System;
using System.Collections.Generic;

namespace P2Modus.Core.Conditions
{
    public delegate void ConditionAddedEventHandler(object sender, ConditionEventArgs args);

    public delegate void ConditionRemovedEventHandler(object sender, ConditionEventArgs args);

    public delegate void ConditionLevelAdjustedEventHandler(object sender, ConditionEventArgs args);

    public interface ICondition : IEntity
    {
        int Level { get; set; }

        bool SupportsLevel { get; }

        ConditionGroup Group { get; }

        IModifier Modifier { get; }

        bool DoesReduceAtEndOfTurn { get; }

        int ReductionAmount { get; }

        void EndTurn();

        event ConditionAddedEventHandler ConditionAddedEvent;

        event ConditionRemovedEventHandler ConditionRemovedEvent;

        event ConditionLevelAdjustedEventHandler ConditionLevelAdjustedEvent;
    }
}