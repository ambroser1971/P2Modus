using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class ProneTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var prone = new Prone();
            prone.Apply(entity);            
            Assert.IsTrue(true); // If we made it here without exception then the test passed
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var prone = new Prone();
            prone.Apply(character);
            Assert.AreEqual(EntityIds.PRONE_CONDITION_ID, character.Conditions[0].Id);
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.PRONE_CONDITION_ID));
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID));
        }

        public void Apply_Character_FlatFootedToOneExists_Test()
        {
            var character = CreateMockCharacter();
            var flatFooted = new FlatFooted(1);
            var prone = new Prone();
            prone.Apply(character);
            var savedFlatFooted = (FlatFooted)character.Conditions.FirstOrDefault(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID);
            Assert.IsTrue(savedFlatFooted.FlatFootedFrom.Contains(FlatFooted.AllCharacters));
            Assert.IsFalse(savedFlatFooted.FlatFootedFrom.Contains(1));
        }

        public void Apply_Character_FlatFootedToAllExists_Test()
        {
            var character = CreateMockCharacter();
            var flatFooted = new FlatFooted(FlatFooted.AllCharacters);
            var prone = new Prone();
            prone.Apply(character);
            var savedFlatFooted = (FlatFooted)character.Conditions.FirstOrDefault(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID);
            Assert.AreSame(flatFooted, savedFlatFooted);
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingProneCondition_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Prone());
            var prone = new Prone();
            prone.Apply(character);
        }

        [TestMethod]
        public void Remove_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var prone = new Prone();
            prone.Apply(character);
            prone.Remove(character);
            Assert.AreEqual(0, character.Conditions.Count);
        }

        [TestMethod]
        public void Remove_WithExistingFlatFootedToOne_Test()
        {
            var character = CreateMockCharacter();
            var flatFooted = new FlatFooted(1);
            flatFooted.Apply(character);
            var prone = new Prone();
            prone.Apply(character);
            prone.Remove(character);
            Assert.AreEqual(0, character.Conditions.Count);
        }

        [TestMethod]
        public void Remove_WithExistingFlatFootedToAll_Test()
        {
            var character = CreateMockCharacter();
            var flatFooted = new FlatFooted(FlatFooted.AllCharacters);
            flatFooted.Apply(character);
            var prone = new Prone();
            prone.Apply(character);
            prone.Remove(character);
            Assert.AreEqual(1, character.Conditions.Count);
            Assert.AreSame(flatFooted, character.Conditions[0]);
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