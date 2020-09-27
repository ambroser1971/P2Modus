using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class SickenedTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var sickened = new Sickened(1);
            sickened.Apply(entity);
            Assert.IsTrue(true); // If we made it here with exception then the test passed
        }

        [TestMethod]
        public void ApplyTo_HasValue()
        {
            var sickened = new Sickened(1);
            Assert.IsTrue(sickened.AppliesTo.Contains(EntityIds.ALL_CHECKS));
            Assert.IsTrue(sickened.AppliesTo.Contains(EntityIds.ALL_DCS));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Apply_ApplyWhenValueZero_ThrowsException()
        {
            var character = CreateMockCharacter();
            var sickened = new Sickened(0);
            sickened.Apply(character);
            Assert.AreEqual(0, character.Conditions.Count);
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var sickened = new Sickened(1);
            sickened.Apply(character);
            Assert.AreEqual(EntityIds.SICKENED_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreEqual(ModifierType.Status, character.Conditions[0].Modifier.Type);
            Assert.AreEqual(-1, character.Conditions[0].Modifier.Value);
            Assert.AreSame(sickened, character.Conditions[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingSickenedCondition_ThrowsException_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Sickened(1));
            var sickened = new Sickened(2);
            sickened.Apply(character);            
        }

        [TestMethod]
        public void ChangeLevel_Test()
        {
            var sickened = new Sickened(1);
            sickened.Level = 2;
            Assert.AreEqual(2, sickened.Level);
            Assert.AreEqual(-2, sickened.Modifier.Value);
        }

        [TestMethod]
        public void IncrementLevel_Test()
        {
            var sickened = new Sickened(1);
            sickened.Level++;
            Assert.AreEqual(2, sickened.Level);
            Assert.AreEqual(-2, sickened.Modifier.Value);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DecrementLevel_BelowOne_Test()
        {
            var sickened = new Sickened(1);
            sickened.Level--;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_InvalidLevel_Test()
        {
            var sickened = new Sickened(0);
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