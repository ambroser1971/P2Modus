using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class FatiguedTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var fatigued = new Fatigued();
            fatigued.Apply(entity);            
            Assert.IsTrue(true); // If we made it here without exception then the test passed
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var fatigued = new Fatigued();
            fatigued.Apply(character);
            Assert.AreEqual(EntityIds.FATIGUED_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreSame(fatigued, character.Conditions[0]);
            Assert.AreEqual(ModifierType.Status, character.Conditions[0].Modifier.Type);
            Assert.AreEqual(-1, character.Conditions[0].Modifier.Value);
            Assert.IsTrue(character.Conditions[0].AppliesTo.Contains(EntityIds.AC_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Contains(EntityIds.FORT_SAVE_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Contains(EntityIds.REFLEX_SAVE_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Contains(EntityIds.WILL_SAVE_ID));
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingFatiguedCondition_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Fatigued());
            var fatigued = new Fatigued();
            fatigued.Apply(character);
        }

        [TestMethod]
        public void Remove_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var fatigued = new Fatigued();
            fatigued.Apply(character);
            fatigued.Remove(character);
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