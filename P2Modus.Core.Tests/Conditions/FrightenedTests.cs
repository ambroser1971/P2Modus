using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class FrightenedTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var frightened = new Frightened(1);
            frightened.Apply(entity);
            Assert.IsTrue(true); // If we made it here with exception then the test passed
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Apply_ApplyWhenValueZero_ThrowsException()
        {
            var character = CreateMockCharacter();
            var frightened = new Frightened(0);
            frightened.Apply(character);
            Assert.AreEqual(0, character.Conditions.Count);
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var frightened = new Frightened(1);
            frightened.Apply(character);
            Assert.AreEqual(EntityIds.FRIGHTENED_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreEqual(ModifierType.Status, character.Conditions[0].Modifier.Type);
            Assert.AreEqual(-1, character.Conditions[0].Modifier.Value);
            Assert.AreSame(frightened, character.Conditions[0]);
            Assert.IsTrue(character.Conditions[0].DoesReduceAtEndOfTurn);
            Assert.AreEqual(1, character.Conditions[0].ReductionAmount);
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingFrightenedCondition_ThrowsException_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Frightened(1));
            var frightened = new Frightened(2);
            frightened.Apply(character);            
        }

        [TestMethod]
        public void ChangeLevel_Test()
        {
            var frightened = new Frightened(1);
            frightened.Level = 2;
            Assert.AreEqual(2, frightened.Level);
            Assert.AreEqual(-2, frightened.Modifier.Value);
        }

        [TestMethod]
        public void IncrementLevel_Test()
        {
            var frightened = new Frightened(1);
            frightened.Level++;
            Assert.AreEqual(2, frightened.Level);
            Assert.AreEqual(-2, frightened.Modifier.Value);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DecrementLevel_BelowOne_Test()
        {
            var frightened = new Frightened(1);
            frightened.Level--;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_InvalidLevel_Test()
        {
            var frightened = new Frightened(0);
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