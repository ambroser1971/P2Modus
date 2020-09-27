using System.Collections.Generic;

namespace P2Modus.Core
{
    public class CalculatedRollResult : ICalculatedRollResult
    {
        public int TotalResult { get; }

        public int TotalModifiers { get; }

        public IEnumerable<int> DieResults { get; }

        public CalculatedRollResult(int totalResult, int totalModifiers, IEnumerable<int> dieResults)
        {
            TotalResult = totalResult;
            TotalModifiers = totalModifiers;
            DieResults = dieResults;
        }
    }
}