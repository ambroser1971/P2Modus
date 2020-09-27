using System;
using System.Collections.Generic;
using System.Linq;

namespace P2Modus.Core
{
    public class Calculator : ICalculator, IRoller, IProficiencyCalculator, IMultipleAttackCalculation
    {
        private IRollResult _rollResult;

        public Calculator() 
        {
            _rollResult = new RollResult();
        }

        public Calculator(IRollResult rollResult)
        {
            _rollResult = rollResult;
        }

        public int CalculateAbilityModifier(int abilityScore)
        {
            if(abilityScore < 1)
                return default;
            
            var mod = (abilityScore - 10.0) / 2.0;
            if(mod < 0)
                mod -= 0.5; // adjust rounding for negative numbers

            return (int)mod;
        }

        public IModifier CalculateProficiencyBonus(ProficiencyRank proficiencyRank, int characterLevel, bool isSkill)
        {
            int bonus = (int)proficiencyRank;
            
            if(!isSkill || proficiencyRank != ProficiencyRank.Untrained)
                bonus += characterLevel;
            
            return new Modifier() {
                Type = ModifierType.Proficiency,
                Value = bonus
            };            
        }

        public IModifier CalculateMultipleAttackPenalty(int attackNumber, bool isAgile)
        {
            int penalty;
            switch (attackNumber)
            {
                case 1:
                    penalty = 0;
                    break;
                case 2:
                    if(isAgile)
                        penalty = -4;
                    else
                        penalty = -5;
                    break;
                default:
                    if(isAgile)
                        penalty = -8;
                    else
                        penalty = -10;
                    break;
            }
            return new Modifier() { Type = ModifierType.UntypedPenalty, Value = penalty };
        }

        public IModifier CalculateRangePenalty(int rangeIncrement, int targetRange)
        {
            var buffer = Math.Floor((decimal)((targetRange - 1) / rangeIncrement));
            if(buffer < 0)
                buffer = 0;

            return new Modifier() { Type = ModifierType.UntypedPenalty, Value = (int)(buffer * -2) };
        }

        public int CalculateArmorClass(ICharacter character)
        {
            var ac =10 + character.Dexterity.Modifier.Value;
            if(character.WornArmor != null)
            {
                ac += character.WornArmor.Modifiers.GetTotalModifiers();
            }
            foreach(var condition in character.Conditions.Where(
                c => c.AppliesTo.Contains(EntityIds.AC_ID) ||
                     c.AppliesTo.Contains(EntityIds.ATTRIBUTE_BASED_CHECK_DEXTERITY)))
            {
                ac += condition.Modifier.Value;
            }
            return ac;
        }

        public int CalculateFullSkillBonus(ISkill skill)
        {
            var character = (ICharacter)skill.EntityAffected;
            var bonus = CalculateProficiencyBonus(skill.Proficiency, character.Level, true);
            bonus.Value += character.GetAbilityScoreById(skill.AbilityScoreId).Modifier.Value;
            return bonus.Value;
        }

        public int Roll(int dieType) => Roll(1, dieType);

        public int Roll(int numberOfDice, int dieType) => Roll(numberOfDice, dieType, new ModifierBag()).TotalResult;

        public ICalculatedRollResult Roll(int numberOfDice, int dieType, ModifierBag modifiers)
        {
            var results = new List<int>();
            var totalModifiers = modifiers.GetTotalModifiers();
            int total = 0;
            var rnd = new Random(DateTime.Now.Millisecond + DateTime.Now.Day);
            for(var i =0; i < numberOfDice; i++) 
            {
                results.Add(rnd.Next(1, dieType));
                total += results.Last();
            }
            total += totalModifiers;
            return new CalculatedRollResult(total, totalModifiers, results);
        }

        public CalculatedRoll20Result Rolld20(ModifierBag modifiers, int dc, bool successOn20 = true)
        {
            var dieResult = Roll(20);
            var modTotal = modifiers.GetTotalModifiers();
            var checkResult = _rollResult.GetResult(dieResult + modTotal, dc);
            if(successOn20 && dieResult == 20 && (checkResult == CheckResult.CriticalFailure || checkResult == CheckResult.Failure))
            {
                checkResult = CheckResult.Success;
            }

            return new CalculatedRoll20Result(checkResult, dieResult + modTotal, modTotal, dieResult);
        }
    }
}