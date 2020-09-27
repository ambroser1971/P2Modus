namespace P2Modus.Core
{
    public interface IRoller
    {
        int Roll(int dieType);

        int Roll(int numberOfDice, int dieType);

        ICalculatedRollResult Roll(int numberOfDice, int dieType, ModifierBag modifiers);

        CalculatedRoll20Result Rolld20(ModifierBag modifiers, int dc, bool successOn20);
    }
}