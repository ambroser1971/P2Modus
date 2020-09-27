using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class UnconsciousTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var unconscious = new Unconscious();
            unconscious.Apply(entity);            
            Assert.IsTrue(true); // If we made it here without exception then the test passed
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var unconscious = new Unconscious();
            unconscious.Apply(character);
            Assert.AreEqual(EntityIds.UNCONSCIOUS_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreSame(unconscious, character.Conditions[0]);
            Assert.AreEqual(ModifierType.Status, character.Conditions[0].Modifier.Type);
            Assert.AreEqual(-4, character.Conditions[0].Modifier.Value);
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.AC_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.PERCEPTION_ID));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Any(a => a == EntityIds.REFLEX_SAVE_ID));
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.BLINDED_CONDITION_ID));
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID));
            Assert.AreEqual(ConditionGroup.DeathAndDying, character.Conditions[0].Group);
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingUnconsciousCondition_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Unconscious());
            var unconscious = new Unconscious();
            unconscious.Apply(character);
        }

        [TestMethod]
        public void Remove_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var unconscious = new Unconscious();
            unconscious.Apply(character);
            unconscious.Remove(character);
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