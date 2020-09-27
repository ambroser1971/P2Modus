using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class DyingTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var dying = new Dying(1);
            dying.Apply(entity);            
            Assert.IsTrue(true); // If we made it here without exception then the test passed
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var dying = new Dying(1);
            dying.Apply(character);
            Assert.AreEqual(EntityIds.DYING_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreEqual(1, character.Conditions[0].Level);
            Assert.AreSame(dying, character.Conditions[0]);
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.UNCONSCIOUS_CONDITION_ID));
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingDyingCondition_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Dying(1));
            var dying = new Dying(2);
            dying.Apply(character);
        }

        [TestMethod]
        public void Apply_Character_WithExistingWoundedCondition_Test()
        {
            var character = CreateMockCharacter();
            var wounded = new Wounded(1);
            wounded.Apply(character);            
            var dying = new Dying(1);
            dying.Apply(character);
            Assert.AreEqual(2, character.Conditions.First(c => c.Id == dying.Id).Level);
            Assert.AreEqual(1, character.Conditions.First(c => c.Id == wounded.Id).Level);
            Assert.AreEqual(ConditionGroup.DeathAndDying, character.Conditions[0].Group);
        }

        [TestMethod]
        public void Remove_WithExistingWoundedCondition_Test()
        {
            var character = CreateMockCharacter();
            var wounded = new Wounded(1);
            wounded.Apply(character);
            var dying = new Dying(1);
            dying.Apply(character);
            dying.Remove(character);
            Assert.AreEqual(1, character.Conditions.Count);
            Assert.AreEqual(2, character.Conditions.First(c => c.Id == wounded.Id).Level);
        }

        [TestMethod]
        public void Remove_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var dying = new Dying(1);
            dying.Apply(character);
            dying.Remove(character);
            Assert.AreEqual(1, character.Conditions.Count);
            var wounded = character.Conditions.FirstOrDefault(c => c.Id == EntityIds.WOUNDED_CONDITION_ID);
            Assert.IsNotNull(wounded);
            Assert.AreEqual(1, wounded.Level);
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