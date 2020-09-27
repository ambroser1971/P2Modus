using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class PersistentDamageTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var die = new Die("Acid", 1, 4, 1);
            var persistentDamage = new PersistentDamage(die.Type, die);
            persistentDamage.Apply(entity);
            Assert.IsTrue(true); // If we made it here without exception then the test passed
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var die = new Die("Acid", 1, 4, 1);
            var persistentDamage = new PersistentDamage(die.Type, die);
            persistentDamage.Apply(character);
            Assert.AreEqual(EntityIds.PERSISTENT_DAMAGE_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreSame(persistentDamage, character.Conditions[0]);
        }

        [TestMethod]
        public void Apply_Character_WithExistingPersistentDamageCondition_Test()
        {
            var character = CreateMockCharacter();
            var die = new Die("Acid", 1, 4, 1);
            character.Conditions.Add(new PersistentDamage(die.Type, die));
            var die2 = new Die("Fire", 1, 4, 1);
            var persistentDamage = new PersistentDamage(die2.Type, die2);
            persistentDamage.Apply(character);
            Assert.AreEqual(2, character.Conditions.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithSameExistingPersistentDamageCondition_Test()
        {
            var character = CreateMockCharacter();
            var die = new Die("Acid", 1, 4, 1);
            character.Conditions.Add(new PersistentDamage(die.Type, die));
            var die2 = new Die("Acid", 1, 6, 1);
            var persistentDamage = new PersistentDamage(die2.Type, die2);
            persistentDamage.Apply(character);;
        }

        [TestMethod]
        public void Remove_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var die = new Die("Acid", 1, 4, 1);
            var persistentDamage = new PersistentDamage(die.Type, die);
            persistentDamage.Apply(character);
            persistentDamage.Remove(character);
            Assert.AreEqual(0, character.Conditions.Count);
        }

        [TestMethod]
        public void EndTurn_OneDamageType_OneDie_Test()
        {
            var character = CreateMockCharacter();
            var die = new Die("Acid", 1, 1, 1);
            var persistentDamage = new PersistentDamage(die.Type, die);
            persistentDamage.Apply(character);
            persistentDamage.EndTurn();
            Assert.AreEqual(18, character.HitPoints.Current);
        }

        [TestMethod]
        public void EndTurn_OneDamageType_ThreeDice_Test()
        {
            var character = CreateMockCharacter();
            var die = new Die("Acid", 3, 1, 1);
            var persistentDamage = new PersistentDamage(die.Type, die);
            persistentDamage.Apply(character);
            persistentDamage.EndTurn();
            Assert.AreEqual(16, character.HitPoints.Current);
        }

        [TestMethod]
        public void EndTurn_TwoDamageTypes_OneDieEach_Test()
        {
            var character = CreateMockCharacter();
            var die = new Die("Acid", 1, 1, 1);
            var acidDamage = new PersistentDamage(die.Type, die);
            acidDamage.Apply(character);
            acidDamage.EndTurn();
            var die2 = new Die("Fire", 1, 1, 1);
            var fireDamage = new PersistentDamage(die2.Type, die2);
            fireDamage.Apply(character);
            fireDamage.EndTurn();
            Assert.AreEqual(16, character.HitPoints.Current);
        }

        [TestMethod]
        public void ToString_SingleDieWithModifier_Test()
        {
            var character = CreateMockCharacter();
            var die = new Die("Acid", 1, 2, 3);
            var acidDamage = new PersistentDamage(die.Type, die);
            Assert.AreEqual("1d2+3 Persistent Acid Damage", acidDamage.ToString());
        }

        [TestMethod]
        public void ToString_SingleDieWithoutModifier_Test()
        {
            var character = CreateMockCharacter();
            var die = new Die("Acid", 1, 2, 0);
            var acidDamage = new PersistentDamage(die.Type, die);
            Assert.AreEqual("1d2 Persistent Acid Damage", acidDamage.ToString());
        }

        [TestMethod]
        public void ToString_OnlyModifier_Test()
        {
            var character = CreateMockCharacter();
            var die = new Die("Acid", 0, 0, 3);
            var acidDamage = new PersistentDamage(die.Type, die);
            Assert.AreEqual("3 Persistent Acid Damage", acidDamage.ToString());
        }

        [TestMethod]
        public void ToString_MultipleDice_Test()
        {
            var character = CreateMockCharacter();
            var die = new Die("Acid", 1, 2, 3);
            var die2 = new Die("Acid", 4, 5, 6);
            var die3 = new Die("Acid", 0, 0, 7);
            var acidDamage = new PersistentDamage(die.Type, die, die2, die3);
            Assert.AreEqual("1d2+3 + 4d5+6 + 7 Persistent Acid Damage", acidDamage.ToString());
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
            return character;
        }

        private IHitPoint CreateMockHitPoints()
        {
            var hp = Mock.Of<IHitPoint>();
            Mock.Get(hp).Setup(m => m.Current).Returns(20);
            return hp;
        }
    }
}   