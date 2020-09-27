using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class RestrainedTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var restrained = new Restrained();
            restrained.Apply(entity);            
            Assert.IsTrue(true); // If we made it here without exception then the test passed
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var restrained = new Restrained();
            restrained.Apply(character);
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.RESTRAINED_CONDITION_ID));
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID));
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.IMMOBILIZED_CONDITION_ID));
        }

        [TestMethod]
        public void Apply_Character_GrabConditionRemoval_Test()
        {
            var character = CreateMockCharacter();
            var grabbed = new Grabbed();
            grabbed.Apply(character);
            var restrained = new Restrained();
            restrained.Apply(character);
            Assert.IsFalse(character.Conditions.Any(c => c.Id == EntityIds.GRABBED_CONDITION_ID));
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.RESTRAINED_CONDITION_ID));
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID));
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.IMMOBILIZED_CONDITION_ID));
        }

        [TestMethod]
        public void Apply_Character_FlatFootedToOneExists_Test()
        {
            var character = CreateMockCharacter();
            var flatFooted = new FlatFooted(1);
            flatFooted.Apply(character);
            var restrained = new Restrained();
            restrained.Apply(character);
            var newFlatFooted = (FlatFooted)character.Conditions.First(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID);
            Assert.AreNotSame(flatFooted, newFlatFooted);
            Assert.IsFalse(newFlatFooted.FlatFootedFrom.Contains(1));
            Assert.IsTrue(newFlatFooted.FlatFootedFrom.Contains(FlatFooted.AllCharacters));
        }

        [TestMethod]
        public void Apply_Character_FlatFootedToAllExists_Test()
        {
            var character = CreateMockCharacter();
            var flatFooted = new FlatFooted(FlatFooted.AllCharacters);
            flatFooted.Apply(character);
            var restrained = new Restrained();
            restrained.Apply(character);
            var newFlatFooted = (FlatFooted)character.Conditions.First(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID);
            Assert.AreSame(flatFooted, newFlatFooted);
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingRestrainedCondition_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Restrained());
            var restrained = new Restrained();
            restrained.Apply(character);
        }

        [TestMethod]
        public void Remove_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var restrained = new Restrained();
            restrained.Apply(character);
            restrained.Remove(character);
            Assert.AreEqual(0, character.Conditions.Count);
        }

        [TestMethod]
        public void Remove_FlatFootedToOneExists_Test()
        {
            var character = CreateMockCharacter();
            var flatFooted = new FlatFooted(1);
            flatFooted.Apply(character);
            var restrained = new Restrained();
            restrained.Apply(character);
            restrained.Remove(character);
            Assert.AreEqual(0, character.Conditions.Count);            
        }

        [TestMethod]
        public void Remove_FlatFootedToAllExists_Test()
        {
            var character = CreateMockCharacter();
            var flatFooted = new FlatFooted(FlatFooted.AllCharacters);
            flatFooted.Apply(character);
            var restrained = new Restrained();
            restrained.Apply(character);
            restrained.Remove(character);
            Assert.AreEqual(1, character.Conditions.Count);
            Assert.IsTrue(character.Conditions.Contains(flatFooted));
        }

        [TestMethod]
        public void Remove_ImmobilizedExists_Test()
        {
            var character = CreateMockCharacter();
            var immobilized = new Immobilized();
            immobilized.Apply(character);
            var restrained = new Restrained();
            restrained.Apply(character);
            restrained.Remove(character);
            Assert.AreEqual(1, character.Conditions.Count);
            Assert.IsTrue(character.Conditions.Contains(immobilized));
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