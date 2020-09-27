using System.Collections.Generic;

namespace P2Modus.Core
{
    public interface ICalculatedRoll20Result
    {
        CheckResult DegreeOfSuccess { get; }

        int TotalResult{ get; }
        
        int TotalModifiers { get; }
        
        int DieResult { get; }
    }
}