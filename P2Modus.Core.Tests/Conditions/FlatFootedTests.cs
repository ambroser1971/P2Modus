using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class FlatFootedTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var flatFooted = new FlatFooted();
            flatFooted.Apply(entity);            
            Assert.IsTrue(true); // If we made it here without exception then the test passed
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var flatFooted = new FlatFooted();
            flatFooted.Apply(character);
            Assert.AreEqual(EntityIds.FLAT_FOOTED_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreSame(flatFooted, character.Conditions[0]);
            Assert.AreEqual(ModifierType.Circumstance, character.Conditions[0].Modifier.Type);
            Assert.AreEqual(-2, character.Conditions[0].Modifier.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingFlatFootedCondition_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new FlatFooted());
            var flatFooted = new FlatFooted();
            flatFooted.Apply(character);
        }

        [TestMethod]
        public void Remove_Character_Test()
        {
            var character = CreateMockCharacter();
            var flatFooted = new FlatFooted(1,2,3);
            flatFooted.Apply(character);
            flatFooted.Remove(character);
            Assert.AreEqual(0, character.Conditions.Count);
        }

        [TestMethod]
        public void Construct_AddSingleFlatFootedTo_Test()
        {
            var character = CreateMockCharacter();
            var flatFooted = new FlatFooted(1,2,3);
            flatFooted.Apply(character);
            var flatFootedReplacement = new FlatFooted(flatFooted, false, 4);
            flatFooted.Remove(character);
            flatFootedReplacement.Apply(character);
            Assert.AreEqual(1, character.Conditions.Count);
            Assert.AreEqual(4, ((FlatFooted)character.Conditions[0]).FlatFootedFrom.Count);
            Assert.IsTrue(((FlatFooted)character.Conditions[0]).FlatFootedFrom.Contains(1));
            Assert.IsTrue(((FlatFooted)character.Conditions[0]).FlatFootedFrom.Contains(2));
            Assert.IsTrue(((FlatFooted)character.Conditions[0]).FlatFootedFrom.Contains(3));
            Assert.IsTrue(((FlatFooted)character.Conditions[0]).FlatFootedFrom.Contains(4));
        }

        [TestMethod]
        public void Construct_AddMultipleFlatFootedTo_Test()
        {
            var character = CreateMockCharacter();
            var flatFooted = new FlatFooted(1,2,3);
            flatFooted.Apply(character);
            var flatFootedReplacement = new FlatFooted(flatFooted, false, 4, 5, 6);
            flatFooted.Remove(character);
            flatFootedReplacement.Apply(character);
            Assert.AreEqual(1, character.Conditions.Count);
            Assert.AreEqual(6, ((FlatFooted)character.Conditions[0]).FlatFootedFrom.Count);
            Assert.IsTrue(((FlatFooted)character.Conditions[0]).FlatFootedFrom.Contains(1));
            Assert.IsTrue(((FlatFooted)character.Conditions[0]).FlatFootedFrom.Contains(2));
            Assert.IsTrue(((FlatFooted)character.Conditions[0]).FlatFootedFrom.Contains(3));
            Assert.IsTrue(((FlatFooted)character.Conditions[0]).FlatFootedFrom.Contains(4));
            Assert.IsTrue(((FlatFooted)character.Conditions[0]).FlatFootedFrom.Contains(5));
            Assert.IsTrue(((FlatFooted)character.Conditions[0]).FlatFootedFrom.Contains(6));
        }

        [TestMethod]
        public void Constructor_Remove_SingleFlatFootedTo_Test()
        {
            var character = CreateMockCharacter();
            var flatFooted = new FlatFooted(1,2,3);
            flatFooted.Apply(character);
            var flatFootedReplacement = new FlatFooted(flatFooted, true, 2);
            flatFooted.Remove(character);
            flatFootedReplacement.Apply(character);
            Assert.AreEqual(1, character.Conditions.Count);
            Assert.AreEqual(2, ((FlatFooted)character.Conditions[0]).FlatFootedFrom.Count);
            Assert.IsTrue(((FlatFooted)character.Conditions[0]).FlatFootedFrom.Contains(1));
            Assert.IsTrue(((FlatFooted)character.Conditions[0]).FlatFootedFrom.Contains(3));
        }

        [TestMethod]
        public void Constructor_Remove_MultipleFlatFootedTo_Test()
        {
            var character = CreateMockCharacter();
            var flatFooted = new FlatFooted(1,2,3);
            flatFooted.Apply(character);
            var flatFootedReplacement = new FlatFooted(flatFooted, true, 1,3);
            flatFooted.Remove(character);
            flatFootedReplacement.Apply(character);
            Assert.AreEqual(1, character.Conditions.Count);
            Assert.AreEqual(1, ((FlatFooted)character.Conditions[0]).FlatFootedFrom.Count);
            Assert.IsTrue(((FlatFooted)character.Conditions[0]).FlatFootedFrom.Contains(2));
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