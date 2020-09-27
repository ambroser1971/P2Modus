using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class ConditionFactoryTests
    {
        [TestMethod]
        public void GetConditionIdsInGroup_Attitudes_Test()
        {
            var factory = new ConditionFactory();
            var ids = factory.GetConditionIdsInGroup(ConditionGroup.Attitude);
            Assert.AreEqual(5, ids.Count());
            Assert.IsTrue(ids.Any(i => i == EntityIds.FRIENDLY_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.HELPFUL_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.HOSTILE_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.INDIFFERENT_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.UNFRIENDLY_CONDITION_ID));
        }

        [TestMethod]
        public void GetConditionIdsInGroup_DeathAndDying_Test()
        {
            var factory = new ConditionFactory();
            var ids = factory.GetConditionIdsInGroup(ConditionGroup.DeathAndDying);
            Assert.AreEqual(4, ids.Count());
            Assert.IsTrue(ids.Any(i => i == EntityIds.DOOMED_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.DYING_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.UNCONSCIOUS_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.WOUNDED_CONDITION_ID));
        }

        [TestMethod]
        public void GetConditionIdsInGroup_DegressOfDetection_Test()
        {
            var factory = new ConditionFactory();
            var ids = factory.GetConditionIdsInGroup(ConditionGroup.DegreesOfDetection);
            Assert.AreEqual(4, ids.Count());
            Assert.IsTrue(ids.Any(i => i == EntityIds.HIDDEN_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.OBSERVED_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.UNDETECTED_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.UNNOTICED_CONDITION_ID));
        }

        [TestMethod]
        public void GetConditionIdsInGroup_LoweredAbilities_Test()
        {
            var factory = new ConditionFactory();
            var ids = factory.GetConditionIdsInGroup(ConditionGroup.LoweredAbilities);
            Assert.AreEqual(4, ids.Count());
            Assert.IsTrue(ids.Any(i => i == EntityIds.CLUMSY_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.DRAINED_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.ENFEEBLED_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.STUPEFIED_CONDITION_ID));
        }
        
        [TestMethod]
        public void GetConditionIdsInGroup_Senses_Test()
        {
            var factory = new ConditionFactory();
            var ids = factory.GetConditionIdsInGroup(ConditionGroup.Senses);
            Assert.AreEqual(5, ids.Count());
            Assert.IsTrue(ids.Any(i => i == EntityIds.BLINDED_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.CONCEALED_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.DAZZLED_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.DEAFENED_CONDITION_ID));
            Assert.IsTrue(ids.Any(i => i == EntityIds.INVISIBLE_CONDITION_ID));
        }

        [TestMethod]
        public void GetConditionIdsInGroup_None_Test()
        {
            var factory = new ConditionFactory();
            var ids = factory.GetConditionIdsInGroup(ConditionGroup.None);
            Assert.AreEqual(18, ids.Count());
            Assert.IsFalse(ids.Any(i => i == EntityIds.FRIENDLY_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.HELPFUL_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.HOSTILE_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.INDIFFERENT_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.UNFRIENDLY_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.DOOMED_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.DYING_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.UNCONSCIOUS_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.WOUNDED_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.HIDDEN_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.OBSERVED_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.UNDETECTED_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.UNNOTICED_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.CLUMSY_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.DRAINED_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.ENFEEBLED_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.STUPEFIED_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.BLINDED_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.CONCEALED_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.DAZZLED_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.DEAFENED_CONDITION_ID));
            Assert.IsFalse(ids.Any(i => i == EntityIds.INVISIBLE_CONDITION_ID));
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateCondition_BadArgument_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, -1);
        }
        
        [TestMethod]
        public void CreateCondition_Blinded_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.BLINDED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.BLINDED_CONDITION_ID));
            Assert.IsTrue(condition is Blinded);
            Assert.AreSame(condition, character.Conditions[0]);
        }
        
        [TestMethod]
        public void CreateCondition_Broken_Test()
        {
            var obj = CreateMockObject();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(obj, EntityIds.BROKEN_CONDITION_ID);
            Assert.IsTrue(obj.Conditions.Any(c => c.Id == EntityIds.BROKEN_CONDITION_ID));
            Assert.IsTrue(condition is Broken);
            Assert.AreSame(condition, obj.Conditions[0]);
        }
        
        [TestMethod]
        public void CreateCondition_Clumsy_WithLevel_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.CLUMSY_CONDITION_ID, 3);
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.CLUMSY_CONDITION_ID));
            Assert.IsTrue(condition is Clumsy);
            Assert.AreSame(condition, character.Conditions[0]);
            Assert.AreEqual(3, character.Conditions[0].Level);
        }

        [TestMethod]
        public void CreateCondition_Drained_WithLevel_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.DRAINED_CONDITION_ID, 2);
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.DRAINED_CONDITION_ID));
            Assert.IsTrue(condition is Drained);
            Assert.AreSame(condition, character.Conditions[0]);
            Assert.AreEqual(2, character.Conditions[0].Level);
        }

        [TestMethod]
        public void CreateCondition_Drained_WithoutLevel_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.DRAINED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.DRAINED_CONDITION_ID));
            Assert.IsTrue(condition is Drained);
            Assert.AreSame(condition, character.Conditions[0]);
            Assert.AreEqual(1, character.Conditions[0].Level);
        }

        [TestMethod]
        public void CreateCondition_Dying_WithLevel_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.DYING_CONDITION_ID, 2);
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.DYING_CONDITION_ID));
            Assert.IsTrue(condition is Dying);
            Assert.AreSame(condition, character.Conditions[0]);
            Assert.AreEqual(2, character.Conditions[0].Level);
        }

        [TestMethod]
        public void CreateCondition_Dying_WithoutLevel_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.DYING_CONDITION_ID);
            Assert.IsTrue(character.Conditions.Any(c => c.Id == EntityIds.DYING_CONDITION_ID));
            Assert.IsTrue(condition is Dying);
            Assert.AreSame(condition, character.Conditions[0]);
            Assert.AreEqual(1, character.Conditions[0].Level);
        }
        
        [TestMethod]
        public void CreateCondition_Encumbered_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.ENCUMBERED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.ENCUMBERED_CONDITION_ID));
            Assert.IsTrue(condition is Encumbered);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.ENCUMBERED_CONDITION_ID));
        }

        [TestMethod]
        public void CreateCondition_Enfeebled_WithoutLevel_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.ENFEEBLED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.ENFEEBLED_CONDITION_ID));
            Assert.IsTrue(condition is Enfeebled);
            Assert.AreSame(condition, character.Conditions[0]);
            Assert.AreEqual(1, character.Conditions[0].Level);
        }

        [TestMethod]
        public void CreateCondition_Enfeebled_WithLevel_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.ENFEEBLED_CONDITION_ID, 2);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.ENFEEBLED_CONDITION_ID));
            Assert.IsTrue(condition is Enfeebled);
            Assert.AreSame(condition, character.Conditions[0]);
            Assert.AreEqual(2, character.Conditions[0].Level);
        }
        
        [TestMethod]
        public void CreateCondition_Fascinated_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.FASCINATED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.FASCINATED_CONDITION_ID));
            Assert.IsTrue(condition is Fascinated);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.FASCINATED_CONDITION_ID));
        }
        
        [TestMethod]
        public void CreateCondition_Fleeing_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.FLEEING_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.FLEEING_CONDITION_ID));
            Assert.IsTrue(condition is Fleeing);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.FLEEING_CONDITION_ID));
        }
        
        [TestMethod]
        public void CreateCondition_Friendly_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.FRIENDLY_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.FRIENDLY_CONDITION_ID));
            Assert.IsTrue(condition is Friendly);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.FRIENDLY_CONDITION_ID));
        }

        [TestMethod]
        public void CreateCondition_Frightened_WithoutLevel_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.FRIGHTENED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.FRIGHTENED_CONDITION_ID));
            Assert.IsTrue(condition is Frightened);
            Assert.AreSame(condition, character.Conditions[0]);
            Assert.AreEqual(1, character.Conditions[0].Level);
        }

        [TestMethod]
        public void CreateCondition_Frightened_WithLevel_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.FRIGHTENED_CONDITION_ID, 2);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.FRIGHTENED_CONDITION_ID));
            Assert.IsTrue(condition is Frightened);
            Assert.AreSame(condition, character.Conditions[0]);
            Assert.AreEqual(2, character.Conditions[0].Level);
        }
        
        [TestMethod]
        public void CreateCondition_Grabbed_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.GRABBED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.GRABBED_CONDITION_ID));
            Assert.IsTrue(condition is Grabbed);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.GRABBED_CONDITION_ID));
        }
        
        [TestMethod]
        public void CreateCondition_Helpful_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.HELPFUL_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.HELPFUL_CONDITION_ID));
            Assert.IsTrue(condition is Helpful);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.HELPFUL_CONDITION_ID));
        }
        
        [TestMethod]
        public void CreateCondition_Hidden_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.HIDDEN_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.HIDDEN_CONDITION_ID));
            Assert.IsTrue(condition is Hidden);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.HIDDEN_CONDITION_ID));
        }
        
        [TestMethod]
        public void CreateCondition_Hostile_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.HOSTILE_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.HOSTILE_CONDITION_ID));
            Assert.IsTrue(condition is Hostile);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.HOSTILE_CONDITION_ID));
        }
        
        [TestMethod]
        public void CreateCondition_Immobilized_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.IMMOBILIZED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.IMMOBILIZED_CONDITION_ID));
            Assert.IsTrue(condition is Immobilized);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.IMMOBILIZED_CONDITION_ID));
        }
        
        [TestMethod]
        public void CreateCondition_Indifferent_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.INDIFFERENT_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.INDIFFERENT_CONDITION_ID));
            Assert.IsTrue(condition is Indifferent);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.INDIFFERENT_CONDITION_ID));
        }
        
        [TestMethod]
        public void CreateCondition_Invisible_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.INVISIBLE_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.INVISIBLE_CONDITION_ID));
            Assert.IsTrue(condition is Invisible);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.INVISIBLE_CONDITION_ID));
        }
        
        [TestMethod]
        public void CreateCondition_Observed_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.OBSERVED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.OBSERVED_CONDITION_ID));
            Assert.IsTrue(condition is Observed);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.OBSERVED_CONDITION_ID));
        }

        [TestMethod]
        public void CreateCondition_PersistentDamage_Test()
        {
            var character = CreateMockCharacter();
            var die = new Die("Acid", 1, 4, 1);
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.PERSISTENT_DAMAGE_CONDITION_ID, "Acid", die);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.PERSISTENT_DAMAGE_CONDITION_ID));
            Assert.IsTrue(condition is PersistentDamage);
            Assert.AreSame(condition, character.Conditions[0]);
        }
        
        [TestMethod]
        public void CreateCondition_Petrified_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.PETRIFIED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.PETRIFIED_CONDITION_ID));
            Assert.IsTrue(condition is Petrified);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.PETRIFIED_CONDITION_ID));
        }
        
        [TestMethod]
        public void CreateCondition_Prone_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.PRONE_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.PRONE_CONDITION_ID));
            Assert.IsTrue(condition is Prone);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.PRONE_CONDITION_ID));
        }
        
        [TestMethod]
        public void CreateCondition_Quickened_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.QUICKENED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.QUICKENED_CONDITION_ID));
            Assert.IsTrue(condition is Quickened);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.QUICKENED_CONDITION_ID));
        }
        
        [TestMethod]
        public void CreateCondition_Restrained_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.RESTRAINED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.RESTRAINED_CONDITION_ID));
            Assert.IsTrue(condition is Restrained);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.RESTRAINED_CONDITION_ID));
        }

        [TestMethod]
        public void CreateCondition_Sickened_WithoutLevel_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.SICKENED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.SICKENED_CONDITION_ID));
            Assert.IsTrue(condition is Sickened);
            Assert.AreSame(condition, character.Conditions[0]);
            Assert.AreEqual(1, character.Conditions[0].Level);
        }

        [TestMethod]
        public void CreateCondition_Sickened_WithLevel_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.SICKENED_CONDITION_ID, 2);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.SICKENED_CONDITION_ID));
            Assert.IsTrue(condition is Sickened);
            Assert.AreSame(condition, character.Conditions[0]);
            Assert.AreEqual(2, character.Conditions[0].Level);
        }
        
        [TestMethod]
        public void CreateCondition_Slowed_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.SLOWED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.SLOWED_CONDITION_ID));
            Assert.IsTrue(condition is Slowed);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.SLOWED_CONDITION_ID));
        }

        [TestMethod]
        public void CreateCondition_Stunned_WithoutLevel_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.STUNNED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.STUNNED_CONDITION_ID));
            Assert.IsTrue(condition is Stunned);
            Assert.AreSame(condition, character.Conditions[0]);
            Assert.AreEqual(1, character.Conditions[0].Level);
        }

        [TestMethod]
        public void CreateCondition_Stunned_WithLevel_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.STUNNED_CONDITION_ID, 2);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.STUNNED_CONDITION_ID));
            Assert.IsTrue(condition is Stunned);
            Assert.AreSame(condition, character.Conditions[0]);
            Assert.AreEqual(2, character.Conditions[0].Level);
        }
        
        [TestMethod]
        public void CreateCondition_Stupefied_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.STUPEFIED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.STUPEFIED_CONDITION_ID));
            Assert.IsTrue(condition is Stupefied);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.STUPEFIED_CONDITION_ID));
        }
        
        [TestMethod]
        public void CreateCondition_Unconscious_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.UNCONSCIOUS_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.UNCONSCIOUS_CONDITION_ID));
            Assert.IsTrue(condition is Unconscious);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.UNCONSCIOUS_CONDITION_ID));
        }
        
        [TestMethod]
        public void CreateCondition_Undetected_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.UNDETECTED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.UNDETECTED_CONDITION_ID));
            Assert.IsTrue(condition is Undetected);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.UNDETECTED_CONDITION_ID));
        }
        
        [TestMethod]
        public void CreateCondition_Unfriendly_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.UNFRIENDLY_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.UNFRIENDLY_CONDITION_ID));
            Assert.IsTrue(condition is Unfriendly);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.UNFRIENDLY_CONDITION_ID));
        }
        
        [TestMethod]
        public void CreateCondition_Unnoticed_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.UNNOTICED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.UNNOTICED_CONDITION_ID));
            Assert.IsTrue(condition is Unnoticed);
            Assert.AreSame(condition, character.Conditions.GetById(EntityIds.UNNOTICED_CONDITION_ID));
        }

        [TestMethod]
        public void CreateCondition_Wounded_WithoutLevel_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.WOUNDED_CONDITION_ID);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.WOUNDED_CONDITION_ID));
            Assert.IsTrue(condition is Wounded);
            Assert.AreSame(condition, character.Conditions[0]);
            Assert.AreEqual(1, character.Conditions[0].Level);
        }

        [TestMethod]
        public void CreateCondition_Wounded_WithLevel_Test()
        {
            var character = CreateMockCharacter();
            var factory = new ConditionFactory();
            var condition = factory.CreateCondition(character, EntityIds.WOUNDED_CONDITION_ID, 2);
            Assert.IsTrue(character.Conditions.HasConditionId(EntityIds.WOUNDED_CONDITION_ID));
            Assert.IsTrue(condition is Wounded);
            Assert.AreSame(condition, character.Conditions[0]);
            Assert.AreEqual(2, character.Conditions[0].Level);
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

        private IObject CreateMockObject()
        {
            var obj = Mock.Of<IObject>();
            Mock.Get(obj).Setup(m => m.Conditions).Returns(new List<ICondition>());
            return obj;
        }
    }
}