using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class EnfeebledTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var enfeebled = new Enfeebled(1);
            enfeebled.Apply(entity);
            Assert.IsTrue(true); // If we made it here with exception then the test passed
        }

        [TestMethod]
        public void ApplyTo_HasValue()
        {
            var enfeebled = new Enfeebled(1);
            Assert.AreEqual(EntityIds.ATTRIBUTE_BASED_CHECK_STRENGTH, enfeebled.AppliesTo.First());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Apply_ApplyWhenValueZero_ThrowsException()
        {
            var character = CreateMockCharacter();
            var enfeebled = new Enfeebled(0);
            enfeebled.Apply(character);
            Assert.AreEqual(0, character.Conditions.Count);
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var enfeebled = new Enfeebled(1);
            enfeebled.Apply(character);
            Assert.AreEqual(EntityIds.ENFEEBLED_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreEqual(ModifierType.Status, character.Conditions[0].Modifier.Type);
            Assert.AreEqual(-1, character.Conditions[0].Modifier.Value);
            Assert.AreSame(enfeebled, character.Conditions[0]);
            Assert.AreEqual(ConditionGroup.LoweredAbilities, character.Conditions[0].Group);
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingEnfeebledCondition_ThrowsException_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Enfeebled(1));
            var enfeebled = new Enfeebled(2);
            enfeebled.Apply(character);            
        }

        [TestMethod]
        public void ChangeLevel_Test()
        {
            var enfeebled = new Enfeebled(1);
            enfeebled.Level = 2;
            Assert.AreEqual(2, enfeebled.Level);
            Assert.AreEqual(-2, enfeebled.Modifier.Value);
        }

        [TestMethod]
        public void IncrementLevel_Test()
        {
            var enfeebled = new Enfeebled(1);
            enfeebled.Level++;
            Assert.AreEqual(2, enfeebled.Level);
            Assert.AreEqual(-2, enfeebled.Modifier.Value);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DecrementLevel_BelowOne_Test()
        {
            var enfeebled = new Enfeebled(1);
            enfeebled.Level--;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_InvalidLevel_Test()
        {
            var enfeebled = new Enfeebled(0);
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