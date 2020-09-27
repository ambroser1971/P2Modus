using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class FascinatedTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var fascinated = new Fascinated();
            fascinated.Apply(entity);            
            Assert.IsTrue(true); // If we made it here without exception then the test passed
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var fascinated = new Fascinated();
            fascinated.Apply(character);
            Assert.AreEqual(EntityIds.FASCINATED_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreSame(fascinated, character.Conditions[0]);
            Assert.AreEqual(ModifierType.Status, character.Conditions[0].Modifier.Type);
            Assert.AreEqual(-2, character.Conditions[0].Modifier.Value);
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.PERCEPTION_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_ACROBATICS_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_ARCANA_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_ATHLETICS_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_CRAFTING_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_DECEPTION_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_DIPLOMACY_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_INTIMIDATION_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_LORE_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_MEDICINE_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_NATURE_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_OCCULTIMS_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_PERFORMANCE_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_RELIGION_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_SOCIETY_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_STEALTH_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_SURVIVAL_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.SKILL_THIEVERY_ID));
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingFascinatedCondition_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Fascinated());
            var fascinated = new Fascinated();
            fascinated.Apply(character);
        }

        [TestMethod]
        public void Remove_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var fascinated = new Fascinated();
            fascinated.Apply(character);
            fascinated.Remove(character);
            Assert.AreEqual(0, character.Conditions.Count);
        }

        private ICharacter CreateMockCharacter()
        {
            var character = Mock.Of<ICharacter>();
            Mock.Get(character).Setup(m => m.Conditions).Returns(new List<ICondition>());
            var vision = Mock.Of<ISense>();
            Mock.Get(vision).Setup(m => m.Id).Returns(EntityIds.VISION_SENSE_ID);
            Mock.Get(vision).Setup(m => m.IsPrecise).Returns(true);
            Mock.Get(character).Setup(m => m.Senses).Returns(new List<ISense>() { vision });
            Mock.Get(character).Setup(m => m.Immunities).Returns(new List<IImmunity>());
            return character;
        }
    }
}   