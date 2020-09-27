using System;

namespace P2Modus.Core.Conditions
{
    public class ConditionExistsException : Exception
    {
        public ICondition ExistingCondition { get; }

        public ICondition OffendingConditiong { get; }

        public ConditionExistsException(ICondition offendingCondition, ICondition existingCondition)
            :base()
        {
            OffendingConditiong = offendingCondition;
            ExistingCondition = existingCondition;
        }
    }
}