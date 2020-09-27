using System;
using System.Collections.Generic;

namespace P2Modus.Core
{
    public interface ICalculator
    {
        int CalculateAbilityModifier(int abilityScore);

        IModifier CalculateProficiencyBonus(ProficiencyRank proficiencyRank, int characterLevel, bool isSkill);

        IModifier CalculateMultipleAttackPenalty(int attackNumber, bool isAgile); 
        
        IModifier CalculateRangePenalty(int rangeIncrement, int targetRange);

        int CalculateArmorClass(ICharacter character);
    }
}