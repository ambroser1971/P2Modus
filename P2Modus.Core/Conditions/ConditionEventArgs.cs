using System;

namespace P2Modus.Core.Conditions
{
    public class ConditionEventArgs : EventArgs
    {
        public ICondition Condition { get; }

        public ConditionEventArgs(ICondition condition)
        {
            Condition = condition;
        }
    }
}