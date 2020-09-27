using System.Collections.Generic;

namespace P2Modus.Core.Conditions
{
    public interface IConditionFactory
    {
        IEnumerable<int> GetConditionIdsInGroup(ConditionGroup group);

        ICondition CreateCondition(IEntity entityToApply, int conditionId, params object[] args);
    }
}