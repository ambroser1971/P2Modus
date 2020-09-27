using System.Collections.Generic;
namespace P2Modus.Core
{
    public interface IAction : IEntity
    {
        ActionEconomy Economy { get; set; }
        
        bool HasTrigger { get; }

        string Trigger { get; }

        bool HasRequirements { get; }

        string Requirements { get; }

        IDictionary<CheckResult, string> DegreesOfSuccess { get; }

        IEnumerable<Conditions.ICondition> AddsConditions { get; }

        IEnumerable<ITrait> Traits { get; }        
    }
}