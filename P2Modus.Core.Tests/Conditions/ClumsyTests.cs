using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class ClumsyTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var clumsy = new Clumsy(1);
            clumsy.Apply(entity);
            Assert.IsTrue(true); // If we made it here with exception then the test passed
        }

        [TestMethod]
        public void ApplyTo_HasValue()
        {
            var clumsy = new Clumsy(1);
            Assert.AreEqual(EntityIds.ATTRIBUTE_BASED_CHECK_DEXTERITY, clumsy.AppliesTo.First());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Apply_ApplyWhenValueZero_ThrowsException()
        {
            var character = CreateMockCharacter();
            var clumsy = new Clumsy(0);
            clumsy.Apply(character);
            Assert.AreEqual(0, character.Conditions.Count);
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var clumsy = new Clumsy(1);
            clumsy.Apply(character);
            Assert.AreEqual(EntityIds.CLUMSY_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreEqual(ModifierType.Status, character.Conditions[0].Modifier.Type);
            Assert.AreEqual(-1, character.Conditions[0].Modifier.Value);
            Assert.AreSame(clumsy, character.Conditions[0]);
            Assert.AreEqual(ConditionGroup.LoweredAbilities, character.Conditions[0].Group);
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingClumsyCondition_ThrowsException_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Clumsy(1));
            var clumsy = new Clumsy(2);
            clumsy.Apply(character);            
        }

        [TestMethod]
        public void ChangeLevel_Test()
        {
            var clumsy = new Clumsy(1);
            clumsy.Level = 2;
            Assert.AreEqual(2, clumsy.Level);
            Assert.AreEqual(-2, clumsy.Modifier.Value);
        }

        [TestMethod]
        public void IncrementLevel_Test()
        {
            var clumsy = new Clumsy(1);
            clumsy.Level++;
            Assert.AreEqual(2, clumsy.Level);
            Assert.AreEqual(-2, clumsy.Modifier.Value);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DecrementLevel_BelowOne_Test()
        {
            var clumsy = new Clumsy(1);
            clumsy.Level--;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_InvalidLevel_Test()
        {
            var clumsy = new Clumsy(0);
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