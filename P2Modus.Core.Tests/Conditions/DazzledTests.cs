using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class DazzledTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var dazzled = new Dazzled();
            dazzled.Apply(entity);            
            Assert.IsTrue(true); // If we made it here without exception then the test passed
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var dazzled = new Dazzled();
            dazzled.Apply(character);
            Assert.AreEqual(EntityIds.DAZZLED_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreSame(dazzled, character.Conditions[0]);
            Assert.AreEqual(ConditionGroup.Senses, character.Conditions[0].Group);
        }

        [TestMethod]
        public void Apply_Character_WithExistingDazzledCondition_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Dazzled());
            var dazzled = new Dazzled();
            dazzled.Apply(character);
            Assert.AreEqual(1, character.Conditions.Count);
            Assert.AreNotSame(dazzled, character.Conditions[0]);
            Assert.AreEqual(EntityIds.DAZZLED_CONDITION_ID, character.Conditions[0].Id);
        }

        [TestMethod]
        public void Apply_Character_DoesNotApplyWithBlindedCondition_Test()
        {
            var character = CreateMockCharacter();
            var blinded = new Blinded();
            blinded.Apply(character);
            var dazzled = new Dazzled();
            dazzled.Apply(character);
            Assert.AreEqual(1, character.Conditions.Count);
            Assert.AreSame(blinded, character.Conditions[0]);
        }

        [TestMethod]
        public void Remove_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var dazzled = new Dazzled();
            dazzled.Apply(character);
            dazzled.Remove(character);
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