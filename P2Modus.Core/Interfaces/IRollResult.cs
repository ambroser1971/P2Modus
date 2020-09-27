using System;

namespace P2Modus.Core
{
    public interface IRollResult
    {
        CheckResult GetResult(int rollValue, int dc);
        CheckResult GetResult(int rollValue, IModifierBag rollModifiers, int dc);
    }
}