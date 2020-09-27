namespace P2Modus.Core
{
    public interface IMultipleAttackCalculation
    {
        IModifier CalculateMultipleAttackPenalty(int attackNumber, bool isAgile);        
    }
}