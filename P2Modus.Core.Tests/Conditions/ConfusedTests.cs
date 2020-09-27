using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class ConfusedTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var confused = new Confused();
            confused.Apply(entity);            
            Assert.IsTrue(true); // If we made it here without exception then the test passed
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var confused = new Confused();
            confused.Apply(character);
            Assert.AreEqual(EntityIds.CONFUSED_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreSame(confused, character.Conditions[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingConfusedCondition_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Confused());
            var confused = new Confused();
            confused.Apply(character);
        }

        public void Apply_Character_AddsFlatFootedCondition_NotAlreadyPresent_Test()
        {
            var character = CreateMockCharacter();
            var confused = new Confused();
            confused.Apply(character);
            var flatFooted = (FlatFooted)character.Conditions.FirstOrDefault(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID);
            Assert.IsNotNull(flatFooted);
            Assert.AreEqual(EntityIds.FLAT_FOOTED_CONDITION_ID, flatFooted.Id);
            Assert.AreEqual(1, flatFooted.FlatFootedFrom.Count);
            Assert.AreEqual(FlatFooted.AllCharacters, flatFooted.FlatFootedFrom[0]);
        }

        public void Apply_Character_AddsFlatFootedCondition_AlreadyPresentButNotAllCharacters_Test()
        {
            var character = CreateMockCharacter();
            var existingFlatFooted = new FlatFooted(1,2,3);
            var confused = new Confused();
            confused.Apply(character);
            var flatFooted = (FlatFooted)character.Conditions.FirstOrDefault(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID);
            Assert.IsNotNull(flatFooted);
            Assert.AreEqual(EntityIds.FLAT_FOOTED_CONDITION_ID, flatFooted.Id);   
            Assert.AreEqual(4, flatFooted.FlatFootedFrom.Count);
            Assert.IsTrue(flatFooted.FlatFootedFrom.Contains(FlatFooted.AllCharacters));
            Assert.IsTrue(flatFooted.FlatFootedFrom.Contains(1));
            Assert.IsTrue(flatFooted.FlatFootedFrom.Contains(2));
            Assert.IsTrue(flatFooted.FlatFootedFrom.Contains(3));
        }

        public void Remove_Character_RemovesConfusedAndFlatFooted_NotAlreadyPresent_Test()
        {
            var character = CreateMockCharacter();
            var confused = new Confused();
            confused.Apply(character);
            confused.Remove(character);
            Assert.AreEqual(0, character.Conditions.Count);
        }

        public void Remove_Character_FlatFootedAlreadyExists_Test()
        {
            var character = CreateMockCharacter();
            var existingFlatFooted = new FlatFooted(1,2,3);
            var confused = new Confused();
            existingFlatFooted.Apply(character);
            confused.Apply(character);
            confused.Remove(character);
            var flatFooted = (FlatFooted)character.Conditions.FirstOrDefault(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID);
            Assert.IsNotNull(flatFooted);
            Assert.AreEqual(EntityIds.FLAT_FOOTED_CONDITION_ID, flatFooted.Id);   
            Assert.AreEqual(3, flatFooted.FlatFootedFrom.Count);
            Assert.IsTrue(flatFooted.FlatFootedFrom.Contains(1));
            Assert.IsTrue(flatFooted.FlatFootedFrom.Contains(2));
            Assert.IsTrue(flatFooted.FlatFootedFrom.Contains(3));
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