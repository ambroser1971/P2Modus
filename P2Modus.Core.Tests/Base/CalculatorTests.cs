using System.Collections.Generic;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void CalculateAbilityModifier_One_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(1);
            Assert.AreEqual(-5, value);
        }

        [TestMethod]
        public void CalculateAbilityModifier_Two_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(2);
            Assert.AreEqual(-4, value);            
        }

        [TestMethod]
        public void CalculateAbilityModifier_Three_Test()
        {        
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(3);
            Assert.AreEqual(-4, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_Four_Test()
        {        
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(4);
            Assert.AreEqual(-3, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_Five_Test()
        {        
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(5);
            Assert.AreEqual(-3, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_Six_Test()
        {        
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(6);
            Assert.AreEqual(-2, value);    
        }


        [TestMethod]
        public void CalculateAbilityModifier_SevenTest()
        {        
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(7);
            Assert.AreEqual(-2, value);    
        }


        [TestMethod]
        public void CalculateAbilityModifier_Eightest()
        {        
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(8);
            Assert.AreEqual(-1, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_Nine_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(9);
            Assert.AreEqual(-1, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_Ten_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(10);
            Assert.AreEqual(0, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_Eleven_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(11);
            Assert.AreEqual(0, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_Twelve_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(12);
            Assert.AreEqual(1, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_Thirteen_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(13);
            Assert.AreEqual(1, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_Fourteen_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(14);
            Assert.AreEqual(2, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_Fifteen_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(15);
            Assert.AreEqual(2, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_Sixteen_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(16);
            Assert.AreEqual(3, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_Seventeen_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(17);
            Assert.AreEqual(3, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_Eighteen_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(18);
            Assert.AreEqual(4, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_Nineteen_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(19);
            Assert.AreEqual(4, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_Twenty_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(20);
            Assert.AreEqual(5, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_TwentyOne_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(21);
            Assert.AreEqual(5, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_TwentyTwo_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(22);
            Assert.AreEqual(6, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_TwentyThree_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(23);
            Assert.AreEqual(6, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_TwentyFour_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(24);
            Assert.AreEqual(7, value);    
        }

        [TestMethod]
        public void CalculateAbilityModifier_TwentyFive_Test()
        {
            var calc = new Calculator();
            var value = calc.CalculateAbilityModifier(25);
            Assert.AreEqual(7, value);    
        }

        [TestMethod]
        public void CalculatProficiencyBonus_Untrained_NotSkill_Test()
        {
            var calc = new Calculator();
            var mod = calc.CalculateProficiencyBonus(ProficiencyRank.Untrained, 5, false);
            Assert.AreEqual(ModifierType.Proficiency, mod.Type);
            Assert.AreEqual(5, mod.Value);
        }

        [TestMethod]
        public void CalculatProficiencyBonus_Untrained_IsSkill_Test()
        {
            var calc = new Calculator();
            var mod = calc.CalculateProficiencyBonus(ProficiencyRank.Untrained, 5, true);
            Assert.AreEqual(ModifierType.Proficiency, mod.Type);
            Assert.AreEqual(0, mod.Value);
        }

        [TestMethod]
        public void CalculatProficiencyBonus_Trained_Test()
        {
            var calc = new Calculator();
            var mod = calc.CalculateProficiencyBonus(ProficiencyRank.Trained, 5, false);
            Assert.AreEqual(ModifierType.Proficiency, mod.Type);
            Assert.AreEqual(7, mod.Value);
        }

        [TestMethod]
        public void CalculatProficiencyBonus_Expert_Test()
        {
            var calc = new Calculator();
            var mod = calc.CalculateProficiencyBonus(ProficiencyRank.Expert, 5, false);
            Assert.AreEqual(ModifierType.Proficiency, mod.Type);
            Assert.AreEqual(9, mod.Value);
        }

        [TestMethod]
        public void CalculatProficiencyBonus_Master_Test()
        {
            var calc = new Calculator();
            var mod = calc.CalculateProficiencyBonus(ProficiencyRank.Master, 5, false);
            Assert.AreEqual(ModifierType.Proficiency, mod.Type);
            Assert.AreEqual(11, mod.Value);
        }

        [TestMethod]
        public void CalculatProficiencyBonus_Legendary_Test()
        {
            var calc = new Calculator();
            var mod = calc.CalculateProficiencyBonus(ProficiencyRank.Legendary, 5, false);
            Assert.AreEqual(ModifierType.Proficiency, mod.Type);
            Assert.AreEqual(13, mod.Value);
        }

        [TestMethod]
        public void CalculateMultipleAttackPenalty_FirstAttack_NotAgile_Test()
        {
            var calc = new Calculator();
            var mod = calc.CalculateMultipleAttackPenalty(1, false);
            Assert.AreEqual(ModifierType.UntypedPenalty, mod.Type);
            Assert.AreEqual(0, mod.Value);
        }

        [TestMethod]
        public void CalculateMultipleAttackPenalty_FirstAttack_IsAgile_Test()
        {
            var calc = new Calculator();
            var mod = calc.CalculateMultipleAttackPenalty(1, true);
            Assert.AreEqual(ModifierType.UntypedPenalty, mod.Type);
            Assert.AreEqual(0, mod.Value);
        }

        [TestMethod]
        public void CalculateMultipleAttackPenalty_SecondAttack_NotAgile_Test()
        {
            var calc = new Calculator();
            var mod = calc.CalculateMultipleAttackPenalty(2, false);
            Assert.AreEqual(ModifierType.UntypedPenalty, mod.Type);
            Assert.AreEqual(-5, mod.Value);
        }

        [TestMethod]
        public void CalculateMultipleAttackPenalty_SecondAttack_IsAgile_Test()
        {
            var calc = new Calculator();
            var mod = calc.CalculateMultipleAttackPenalty(2, true);
            Assert.AreEqual(ModifierType.UntypedPenalty, mod.Type);
            Assert.AreEqual(-4, mod.Value);
        }

        [TestMethod]
        public void CalculateMultipleAttackPenalty_ThirdAttack_NotAgile_Test()
        {
            var calc = new Calculator();
            var mod = calc.CalculateMultipleAttackPenalty(3, false);
            Assert.AreEqual(ModifierType.UntypedPenalty, mod.Type);
            Assert.AreEqual(-10, mod.Value);
        }

        [TestMethod]
        public void CalculateMultipleAttackPenalty_ThirdAttack_IsAgile_Test()
        {
            var calc = new Calculator();
            var mod = calc.CalculateMultipleAttackPenalty(3, true);
            Assert.AreEqual(ModifierType.UntypedPenalty, mod.Type);
            Assert.AreEqual(-8, mod.Value);
        }

        [TestMethod]
        public void CalculateMultipleAttackPenalty_FourthAttack_NotAgile_Test()
        {
            var calc = new Calculator();
            var mod = calc.CalculateMultipleAttackPenalty(4, false);
            Assert.AreEqual(ModifierType.UntypedPenalty, mod.Type);
            Assert.AreEqual(-10, mod.Value);
        }

        [TestMethod]
        public void CalculateMultipleAttackPenalty_FourthAttack_IsAgile_Test()
        {
            var calc = new Calculator();
            var mod = calc.CalculateMultipleAttackPenalty(4, true);
            Assert.AreEqual(ModifierType.UntypedPenalty, mod.Type);
            Assert.AreEqual(-8, mod.Value);
        }

        [TestMethod]
        public void CalculateRangePenalty_FiveFeet_NoPenalty_Test()
        {
            int rangeIncrement = 30;
            int targetRange = 5;
            var calc = new Calculator();
            var mod = calc.CalculateRangePenalty(rangeIncrement, targetRange);
            Assert.AreEqual(ModifierType.UntypedPenalty, mod.Type);
            Assert.AreEqual(0, mod.Value);
        }

        [TestMethod]
        public void CalculateRangePenalty_TwentyFiveFeet_NoPenalty_Test()
        {
            int rangeIncrement = 30;
            int targetRange = 25;
            var calc = new Calculator();
            var mod = calc.CalculateRangePenalty(rangeIncrement, targetRange);
            Assert.AreEqual(ModifierType.UntypedPenalty, mod.Type);
            Assert.AreEqual(0, mod.Value);
        }

        [TestMethod]
        public void CalculateRangePenalty_ThirtyFeet_NoPenalty_Test()
        {
            int rangeIncrement = 30;
            int targetRange = 30;
            var calc = new Calculator();
            var mod = calc.CalculateRangePenalty(rangeIncrement, targetRange);
            Assert.AreEqual(ModifierType.UntypedPenalty, mod.Type);
            Assert.AreEqual(0, mod.Value);
        }

        [TestMethod]
        public void CalculateRangePenalty_ThirtyFiveFeet_MinusTwoPenalty_Test()
        {
            int rangeIncrement = 30;
            int targetRange = 35;
            var calc = new Calculator();
            var mod = calc.CalculateRangePenalty(rangeIncrement, targetRange);
            Assert.AreEqual(ModifierType.UntypedPenalty, mod.Type);
            Assert.AreEqual(-2, mod.Value);
        }

        [TestMethod]
        public void CalculateRangePenalty_SixtyFeet_MinusTwoPenalty_Test()
        {
            int rangeIncrement = 30;
            int targetRange = 60;
            var calc = new Calculator();
            var mod = calc.CalculateRangePenalty(rangeIncrement, targetRange);
            Assert.AreEqual(ModifierType.UntypedPenalty, mod.Type);
            Assert.AreEqual(-2, mod.Value);
        }

        [TestMethod]
        public void CalculateRangePenalty_SixtyFiveFeet_MinusFourPenalty_Test()
        {
            int rangeIncrement = 30;
            int targetRange = 65;
            var calc = new Calculator();
            var mod = calc.CalculateRangePenalty(rangeIncrement, targetRange);
            Assert.AreEqual(ModifierType.UntypedPenalty, mod.Type);
            Assert.AreEqual(-4, mod.Value);
        }

        [TestMethod]
        public void CalculateRangePenalty_Ninety_MinusFourPenalty_Test()
        {
            int rangeIncrement = 30;
            int targetRange = 90;
            var calc = new Calculator();
            var mod = calc.CalculateRangePenalty(rangeIncrement, targetRange);
            Assert.AreEqual(ModifierType.UntypedPenalty, mod.Type);
            Assert.AreEqual(-4, mod.Value);
        }

        [TestMethod]
        public void CalculateRangePenalty_NinetyFive_MinusSixPenalty_Test()
        {
            int rangeIncrement = 30;
            int targetRange = 95;
            var calc = new Calculator();
            var mod = calc.CalculateRangePenalty(rangeIncrement, targetRange);
            Assert.AreEqual(ModifierType.UntypedPenalty, mod.Type);
            Assert.AreEqual(-6, mod.Value);
        }

        [TestMethod]
        public void CalculateArmorClass_Test()
        {
            var character = BuildMockCharacter();
            var calc = new Calculator();
            var result = calc.CalculateArmorClass(character);
            Assert.AreEqual(14, result);
        }

        [TestMethod]
        public void Roll_OneArg_Test()
        {
            var calc = new Calculator();
            var result = calc.Roll(2);
            Assert.IsTrue(result > 0 && result < 3);
        }

        [TestMethod]
        public void Roll_TwoArgs_Test()
        {
            var calc = new Calculator();
            var result = calc.Roll(2, 2);
            Assert.IsTrue(result > 1 && result < 5);
        }

        [TestMethod]
        public void Roll_ThreeArgs_NoModifiers_Test()
        {
            var calc = new Calculator();
            var result = calc.Roll(2, 2, new ModifierBag());            
            Assert.IsTrue(result.TotalResult > 1 && result.TotalResult < 5);
            Assert.AreEqual(0, result.TotalModifiers);
            int total = 0;
            foreach(var r in result.DieResults)
                total += r;
            Assert.AreEqual(total, result.TotalResult);
        }

        [TestMethod]
        public void Roll_ThreeArgs_WithModifiers_Test()
        {
            var calc = new Calculator();
            var bag = new ModifierBag() {
                new Modifier() { Type = ModifierType.Ability, Value = 2},
                new Modifier() { Type = ModifierType.Item, Value = 4},
                new Modifier() { Type = ModifierType.Status, Value = 1 },
                new Modifier() { Type = ModifierType.UntypedPenalty, Value = -2}                
            };
            var result = calc.Roll(2, 2, bag);            
            Assert.IsTrue(result.TotalResult > 6 && result.TotalResult < 10);
            Assert.AreEqual(5, result.TotalModifiers);
            int total = result.TotalModifiers;
            foreach(var r in result.DieResults)
                total += r;
            Assert.AreEqual(total, result.TotalResult);
        }

        [TestMethod]
        public void Roll20_VerifyTotal_Test()
        {
            var calc = new Calculator();
            var bag = new ModifierBag() {
                new Modifier() { Type = ModifierType.Ability, Value = 2},
                new Modifier() { Type = ModifierType.Item, Value = 4},
                new Modifier() { Type = ModifierType.Status, Value = 1 },
                new Modifier() { Type = ModifierType.UntypedPenalty, Value = -2}                
            };
            var result = calc.Rolld20(bag, 15, false);
            Assert.AreEqual(result.DieResult + result.TotalModifiers, result.TotalResult);
        }
        
        [TestMethod]
        public void CalculateFullSkillBonus_Untrained_Test()
        {
            var character = BuildMockCharacter();
            character.Skills[0].Proficiency = ProficiencyRank.Untrained;
            var calc = new Calculator();
            var result = calc.CalculateFullSkillBonus(character.Skills[0]);
            Assert.AreEqual(2, result);
        }
        
        [TestMethod]
        public void CalculateFullSkillBonus_Trained_Test()
        {
            var character = BuildMockCharacter();
            character.Skills[0].Proficiency = ProficiencyRank.Trained;
            var calc = new Calculator();
            var result = calc.CalculateFullSkillBonus(character.Skills[0]);
            Assert.AreEqual(5, result);
        }
        
        [TestMethod]
        public void CalculateFullSkillBonus_Expert_Test()
        {
            var character = BuildMockCharacter();
            character.Skills[0].Proficiency = ProficiencyRank.Expert;
            var calc = new Calculator();
            var result = calc.CalculateFullSkillBonus(character.Skills[0]);
            Assert.AreEqual(7, result);
        }
        
        [TestMethod]
        public void CalculateFullSkillBonus_Master_Test()
        {
            var character = BuildMockCharacter();
            character.Skills[0].Proficiency = ProficiencyRank.Master;
            var calc = new Calculator();
            var result = calc.CalculateFullSkillBonus(character.Skills[0]);
            Assert.AreEqual(9, result);
        }
        
        [TestMethod]
        public void CalculateFullSkillBonus_Legendary_Test()
        {
            var character = BuildMockCharacter();
            character.Skills[0].Proficiency = ProficiencyRank.Legendary;
            var calc = new Calculator();
            var result = calc.CalculateFullSkillBonus(character.Skills[0]);
            Assert.AreEqual(11, result);
        }
    
        private ICharacter BuildMockCharacter()
        {
            var character = Mock.Of<ICharacter>();
            Mock.Get(character).Setup(m => m.Conditions).Returns(new List<ICondition>());            
            Mock.Get(character).Setup(m => m.Skills).Returns(new List<ISkill>());
            var dex = Mock.Of<IAbilityScore>();
            Mock.Get(dex).Setup(m => m.Value).Returns(14);
            var dexMod = Mock.Of<IModifier>();
            Mock.Get(dexMod).Setup(m => m.Value).Returns(2);
            Mock.Get(dex).Setup(m => m.Modifier).Returns(dexMod);
            Mock.Get(character).Setup(m => m.Dexterity).Returns(dex);
            var armor = Mock.Of<IArmor>();
            Mock.Get(armor).SetupGet(m => m.Category).Returns(ArmorCategory.Medium);
            Mock.Get(armor).SetupGet(m => m.Modifiers).Returns(BuildModifierBag());
            Mock.Get(character).Setup(m => m.WornArmor).Returns(armor);
            character.Skills.Add(BuildMockSkill());
            character.Skills[0].EntityAffected = character;
            Mock.Get(character).Setup(m => m.GetAbilityScoreById(It.IsAny<int>())).Returns(dex);
            Mock.Get(character).Setup(m => m.Level).Returns(1);
            return character;
        }
        
        private IModifierBag BuildModifierBag()
        {
            var itemModifier = Mock.Of<IModifier>();
            Mock.Get(itemModifier).Setup(m => m.Type).Returns(ModifierType.Item);
            Mock.Get(itemModifier).Setup(m => m.Value).Returns(3);

            var statusModifier = Mock.Of<IModifier>();
            Mock.Get(statusModifier).Setup(m => m.Type).Returns(ModifierType.Status);
            Mock.Get(statusModifier).Setup(m => m.Value).Returns(1);

            var untypedModifier = Mock.Of<IModifier>();
            Mock.Get(untypedModifier).Setup(m => m.Type).Returns(ModifierType.UntypedPenalty);
            Mock.Get(untypedModifier).Setup(m => m.Value).Returns(-2);            

            return new ModifierBag()
            {
                itemModifier,
                statusModifier,
                untypedModifier
            };        
        }

        private ISkill BuildMockSkill()
        {
            var skill = Mock.Of<ISkill>();
            var appliesToList = new List<int>();
            appliesToList.Add(EntityIds.CHARACTER_ID);
            Mock.Get(skill).Setup(m => m.AbilityScoreId).Returns(EntityIds.ATTRIBUTE_BASED_CHECK_DEXTERITY);
            Mock.Get(skill).Setup(m => m.AppliesTo).Returns(appliesToList);
            Mock.Get(skill).Setup(m => m.Description).Returns("Description");
            Mock.Get(skill).Setup(m => m.Id).Returns(1);
            Mock.Get(skill).Setup(m => m.Name).Returns("Name");
            Mock.Get(skill).Setup(m => m.Proficiency).Returns(ProficiencyRank.Trained);
            return skill;            
        }
    }
}
