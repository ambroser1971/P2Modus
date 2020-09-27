namespace P2Modus.Core
{
    public interface IProficiencyCalculator
    {
        IModifier CalculateProficiencyBonus(ProficiencyRank proficiencyRank, int characterLevel, bool isSkill);
    }
}