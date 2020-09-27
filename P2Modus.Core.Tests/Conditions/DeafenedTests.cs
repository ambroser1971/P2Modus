using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class DeafenedTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var deafened = new Deafened();
            deafened.Apply(entity);            
            Assert.IsTrue(true); // If we made it here without exception then the test passed
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var deafened = new Deafened();
            deafened.Apply(character);
            Assert.AreEqual(EntityIds.DEAFENED_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreSame(deafened, character.Conditions[0]);
            Assert.AreEqual(ConditionGroup.Senses, character.Conditions[0].Group);
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingDeafenedCondition_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Deafened());
            var deafened = new Deafened();
            deafened.Apply(character);
        }

        [TestMethod]
        public void Apply_Character_PerceptionInitiativeModifier_Test()
        {
            var character = CreateMockCharacter();
            var deafened = new Deafened();
            deafened.Apply(character);
            Assert.AreEqual(1, character.Conditions.Count);
            Assert.AreEqual(EntityIds.PERCEPTION_BASED_INITIATIVE_ID, character.Conditions[0].AppliesTo[0]);
            Assert.AreEqual(ModifierType.Status, character.Conditions[0].Modifier.Type);
            Assert.AreEqual(-2, character.Conditions[0].Modifier.Value);
        }

        [TestMethod]
        public void Remove_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var deafened = new Deafened();
            deafened.Apply(character);
            deafened.Remove(character);
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