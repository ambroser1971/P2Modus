using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class DrainedTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var drained = new Drained(1);
            drained.Apply(entity);            
            Assert.IsTrue(true); // If we made it here without exception then the test passed
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var drained = new Drained(1);
            drained.Apply(character);
            Assert.AreEqual(EntityIds.DRAINED_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreSame(drained, character.Conditions[0]);
            Assert.AreEqual(17, character.HitPoints.ModifiedMax);
            Assert.AreEqual(15, character.HitPoints.Current);
            Assert.IsFalse(character.Conditions.Any(c => c.Id == EntityIds.DYING_CONDITION_ID));
            Assert.AreEqual(ConditionGroup.LoweredAbilities, character.Conditions[0].Group);
        }

        [TestMethod]
        public void Apply_Character_Dying_Test()
        {
            var character = CreateMockCharacter();
            var drained = new Drained(6);
            drained.Apply(character);
            Assert.AreEqual(EntityIds.DRAINED_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreSame(drained, character.Conditions[0]);
            Assert.AreEqual(2, character.HitPoints.ModifiedMax);
            Assert.AreEqual(0, character.HitPoints.Current);
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.DYING_CONDITION_ID));
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingDrainedCondition_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Drained(1));
            var drained = new Drained(2);
            drained.Apply(character);
        }

        [TestMethod]
        public void Remove_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var drained = new Drained(1);
            drained.Apply(character);
            drained.Remove(character);
            Assert.AreEqual(0, character.Conditions.Count);            
            Assert.AreEqual(20, character.HitPoints.ModifiedMax);
            Assert.AreEqual(15, character.HitPoints.Current);
        }

        [TestMethod]
        public void IncreaseLevel_Test()
        {
            var character = CreateMockCharacter();
            var drained = new Drained(1);
            drained.Apply(character);
            drained.Level += 2;
            Assert.AreEqual(3, drained.Level);
            Assert.AreEqual(11, character.HitPoints.ModifiedMax);
            Assert.AreEqual(9, character.HitPoints.Current);
        }

        [TestMethod]
        public void IncreaseLevel_DyingAtZero_Test()
        {
            var character = CreateMockCharacter();
            var drained = new Drained(1);
            drained.Apply(character);
            drained.Level += 5;
            Assert.AreEqual(6, drained.Level);
            Assert.AreEqual(2, character.HitPoints.ModifiedMax);
            Assert.AreEqual(0, character.HitPoints.Current);
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.DYING_CONDITION_ID));
        }

        [TestMethod]
        public void IncreaseLevel_DyingBelowZero_Test()
        {
            var character = CreateMockCharacter();
            var drained = new Drained(1);
            drained.Apply(character);
            drained.Level += 6;
            Assert.AreEqual(7, drained.Level);
            Assert.AreEqual(-1, character.HitPoints.ModifiedMax);
            Assert.AreEqual(0, character.HitPoints.Current);
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.DYING_CONDITION_ID));
        }

        [TestMethod]
        public void DecreaseLevel_Test()
        {
            var character = CreateMockCharacter();
            var drained = new Drained(3);
            drained.Apply(character);
            drained.Level -= 2;
            Assert.AreEqual(1, drained.Level);
            Assert.AreEqual(17, character.HitPoints.ModifiedMax);
            Assert.AreEqual(9, character.HitPoints.Current);
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
            Mock.Get(character).Setup(m => m.HitPoints).Returns(CreateMockHitPoints());
            Mock.Get(character).Setup(m => m.Level).Returns(3);
            return character;
        }

        private IHitPoint CreateMockHitPoints()
        {
            var hp = Mock.Of<IHitPoint>();
            Mock.Get(hp).Setup(m => m.Current).Returns(18);
            Mock.Get(hp).Setup(m => m.Max).Returns(20);
            Mock.Get(hp).Setup(m => m.ModifiedMax).Returns(20);
            return hp;
        }
    }
}   