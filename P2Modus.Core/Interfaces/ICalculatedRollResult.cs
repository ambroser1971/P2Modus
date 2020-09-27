using System.Collections.Generic;

namespace P2Modus.Core
{
    public interface ICalculatedRollResult
    {
        int TotalResult{ get; }
        
        int TotalModifiers { get; }
        
        IEnumerable<int> DieResults { get; }
    }
}