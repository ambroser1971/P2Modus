using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class StunnedTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var stunned = new Stunned(1);
            stunned.Apply(entity);            
            Assert.IsTrue(true); // If we made it here without exception then the test passed
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var stunned = new Stunned(2);
            stunned.Apply(character);
            Assert.AreEqual(EntityIds.STUNNED_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreSame(stunned, character.Conditions[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingStunnedCondition_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Stunned(2));
            var stunned = new Stunned(1);
            stunned.Apply(character);
        }

        [TestMethod]
        public void Remove_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var stunned = new Stunned(1);
            stunned.Apply(character);
            stunned.Remove(character);
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