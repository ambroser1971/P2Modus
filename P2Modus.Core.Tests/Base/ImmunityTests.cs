using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class ImmunityTests
    {
        [TestMethod]
        public void Apply_NotExisting_Test()
        {
            var character = CreateMockCharacter();
            var immunity = new Immunity("Test", "Description", 1);
            immunity.Apply(character);
            Assert.AreEqual(1, character.Immunities.Count);
            Assert.AreSame(immunity, character.Immunities[0]);
        }

        [TestMethod]
        public void Apply_AlreadyExists_Test()
        {
            var character = CreateMockCharacter();
            var immunity = new Immunity("Test", "Description", 1);
            immunity.Apply(character);
            var immunity2 = new Immunity("Test 2", "Description 2", 1);
            immunity2.Apply(character);
            Assert.AreEqual(1, character.Immunities.Count);
            Assert.AreSame(immunity, character.Immunities[0]);
        }

        [TestMethod]
        public void Remove_Exists_Tests()
        {
            var character = CreateMockCharacter();
            var immunity = new Immunity("Test", "Description", 1);
            immunity.Apply(character);
            immunity.Remove(character);
            Assert.AreEqual(0, character.Immunities.Count);
        }

        [TestMethod]
        public void Remove_NotExists_Tests()
        {
            var character = CreateMockCharacter();
            var immunity = new Immunity("Test", "Description", 1);
            immunity.Remove(character);
            Assert.AreEqual(0, character.Immunities.Count);
            Assert.IsTrue(true); // If we made it here without exception no errors occured
        }

        private ICharacter CreateMockCharacter()
        {
            var character = Mock.Of<ICharacter>();
            Mock.Get(character).Setup(m => m.Immunities).Returns(new List<IImmunity>());
            return character;
        }
    }    
}