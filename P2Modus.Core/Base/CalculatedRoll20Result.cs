using System.Collections.Generic;

namespace P2Modus.Core
{
    public class CalculatedRoll20Result : ICalculatedRoll20Result
    {
        public CheckResult DegreeOfSuccess { get; }
        
        public int TotalResult{ get; }
        
        public int TotalModifiers { get; }
        
        public int DieResult { get; }

        public CalculatedRoll20Result(CheckResult checkResult, int totalResult, int totalModifiers, int dieResult)
        {
            DegreeOfSuccess = checkResult;
            TotalResult = totalResult;
            TotalModifiers = totalModifiers;
            DieResult = dieResult;
        }
    }
}