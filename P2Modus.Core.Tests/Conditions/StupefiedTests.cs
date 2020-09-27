using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class StupefiedTests
    {
        [TestMethod]
        public void Apply_NotACharacter_Test()
        {
            var entity = Mock.Of<IEntity>();
            var stupefied = new Stupefied(1);
            stupefied.Apply(entity);            
            Assert.IsTrue(true); // If we made it here without exception then the test passed
        }

        [TestMethod]
        public void Apply_Character_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var stupefied = new Stupefied(1);
            stupefied.Apply(character);
            Assert.AreEqual(EntityIds.STUPEFIED_CONDITION_ID, character.Conditions[0].Id);
            Assert.AreSame(stupefied, character.Conditions[0]);
            Assert.AreEqual(ModifierType.Status, character.Conditions[0].Modifier.Type);
            Assert.AreEqual(-1, character.Conditions[0].Modifier.Value);
            Assert.IsTrue(character.Conditions[0].AppliesTo.Contains(EntityIds.ATTRIBUTE_BASED_CHECK_INTELLIGENCE));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Contains(EntityIds.ATTRIBUTE_BASED_CHECK_WISDOM));
            Assert.IsTrue(character.Conditions[0].AppliesTo.Contains(EntityIds.ATTRIBUTE_BASED_CHECK_CHARISMA));
            Assert.AreEqual(ConditionGroup.LoweredAbilities, character.Conditions[0].Group);
        }

        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingStupefiedCondition_Test()
        {
            var character = CreateMockCharacter();
            character.Conditions.Add(new Stupefied(2));
            var stupefied = new Stupefied(1);
            stupefied.Apply(character);
        }

        [TestMethod]
        public void Remove_NoConditions_Test()
        {
            var character = CreateMockCharacter();
            var stupefied = new Stupefied(1);
            stupefied.Apply(character);
            stupefied.Remove(character);
            Assert.AreEqual(0, character.Conditions.Count);
        }

        [TestMethod]
        public void ChangeLevel_ChangesModifierValue_Test()
        {
            var character = CreateMockCharacter();
            var stupefied = new Stupefied(1);
            stupefied.Apply(character);
            Assert.AreEqual(-1, character.Conditions[0].Modifier.Value);
            character.Conditions[0].Level = 3;
            Assert.AreEqual(-3, character.Conditions[0].Modifier.Value);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ChangeLevel_IncalidValueThroesException_Test()
        {
            var character = CreateMockCharacter();
            var stupefied = new Stupefied(1);
            stupefied.Level--;
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