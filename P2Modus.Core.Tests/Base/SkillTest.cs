using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class SkillTests
    {
        [TestMethod]
        public void Apply_NotExisting_Test()
        {
            var character = CreateMockCharacter();
            var skill = new Skill("Test", "Description", 1, ProficiencyRank.Untrained, EntityIds.ATTRIBUTE_BASED_CHECK_DEXTERITY);
            skill.Apply(character);
            Assert.AreEqual(1, character.Skills.Count);
            Assert.AreSame(skill, character.Skills[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void Apply_AlreadyExists_Test()
        {
            var character = CreateMockCharacter();
            var skill = new Skill("Test", "Description", 1, ProficiencyRank.Untrained, EntityIds.ATTRIBUTE_BASED_CHECK_DEXTERITY);
            skill.Apply(character);
            var skill2 = new Skill("Test 2", "Description 2", 1, ProficiencyRank.Untrained, EntityIds.ATTRIBUTE_BASED_CHECK_DEXTERITY);
            skill2.Apply(character);
            Assert.AreEqual(1, character.Skills.Count);
            Assert.AreSame(skill, character.Skills[0]);
            Assert.AreEqual("Test", character.Skills[0].Name);
            Assert.AreEqual("Description", character.Skills[0].Description);
        }

        [TestMethod]
        public void Remove_Exists_Tests()
        {
            var character = CreateMockCharacter();
            var skill = new Skill("Test", "Description", 1, ProficiencyRank.Untrained, EntityIds.ATTRIBUTE_BASED_CHECK_DEXTERITY);
            skill.Apply(character);
            skill.Remove(character);
            Assert.AreEqual(0, character.Skills.Count);
        }

        [TestMethod]
        public void Remove_NotExists_Tests()
        {
            var character = CreateMockCharacter();
            var skill = new Skill("Test", "Description", 1, ProficiencyRank.Untrained, EntityIds.ATTRIBUTE_BASED_CHECK_DEXTERITY);
            skill.Apply(character);
            var skill2 = new Skill("Test 2", "Description 2", 2, ProficiencyRank.Untrained, EntityIds.ATTRIBUTE_BASED_CHECK_DEXTERITY);
            skill2.Remove(character);
            Assert.AreEqual(1, character.Skills.Count);
            // If we made it here without exception no errors occured
        }

        private ICharacter CreateMockCharacter()
        {
            var character = Mock.Of<ICharacter>();
            Mock.Get(character).Setup(m => m.Skills).Returns(new List<ISkill>());
            Mock.Get(character).SetupSequence(m => m.GetAbilityScoreById(It.IsAny<int>()))
                .Returns(new AbilityScore(EntityIds.ATTRIBUTE_BASED_CHECK_DEXTERITY, "Dex", "foo", 12));
            return character;
        }
    }    
}