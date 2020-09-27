using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class BlindedTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var blinded = new Blinded();
            blinded.Apply(entity);
            Assert.IsTrue(true); // If we made it here with exception then the test passed
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var blinded = new Blinded();
            blinded.Apply(character);
            Assert.AreSame(blinded, character.Conditions[0]);
            Assert.AreEqual(ConditionGroup.Senses, character.Conditions[0].Group);
            Assert.AreEqual(EntityIds.VISUAL_TRAIT_ID, character.Immunities[0].Id);
        }


        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingBlindedCondition_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Blinded());
            var blinded = new Blinded();
            blinded.Apply(character);
        }

        [TestMethod]
        public void Apply_Character_RemovedDazledCondition_Test()
        {
            var character = CreateMockCharacter();
            var dazzled = new Dazzled();
            dazzled.Apply(character);
            var blinded = new Blinded();
            blinded.Apply(character);
            Assert.AreEqual(1, character.Conditions.Count);
            Assert.AreSame(blinded, character.Conditions[0]);            
        }

        [TestMethod]
        public void Remove_NoOtherConditions_Test()
        {
            var character = CreateMockCharacter();
            var blinded = new Blinded();
            blinded.Apply(character);
            blinded.Remove(character);
            Assert.AreEqual(0, character.Conditions.Count);
            Assert.AreEqual(0, character.Immunities.Count);
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