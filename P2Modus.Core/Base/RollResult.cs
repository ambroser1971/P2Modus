using System;

namespace P2Modus.Core
{
    public class RollResult : IRollResult
    {
        public CheckResult GetResult(int rollValue, int dc)
        {
            if(rollValue >= dc + 10)
                return CheckResult.CriticalSuccess;
            
            if(rollValue >= dc)
                return CheckResult.Success;

            if(rollValue <= dc - 10)
                return CheckResult.CriticalFailure;

            return CheckResult.Failure;
        }

        public CheckResult GetResult(int rollValue, IModifierBag rollModifiers, int dc)
        {
            
            var total = rollValue + rollModifiers.GetTotalModifiers();
            return GetResult(total, dc);
        }
    }
}