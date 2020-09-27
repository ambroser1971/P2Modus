using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class ResistanceTests
    {
        [TestMethod]
        public void Apply_NotExisting_Test()
        {
            var character = CreateMockCharacter();
            var resistance = new Resistance("Test", "Description", 1, 2);
            resistance.Apply(character);
            Assert.AreEqual(1, character.Resistances.Count);
            Assert.AreSame(resistance, character.Resistances[0]);
        }

        [TestMethod]
        public void Apply_AlreadyExists_SecondHigherValue_Test()
        {
            var character = CreateMockCharacter();
            var resistance = new Resistance("Test", "Description", 1, 2);
            resistance.Apply(character);
            var resistance2 = new Resistance("Test 2", "Description 2", 1, 3);
            resistance2.Apply(character);
            Assert.AreEqual(1, character.Resistances.Count);
            Assert.AreEqual(3, character.Resistances[0].Value);
            Assert.AreEqual("Test", character.Resistances[0].Name);
            Assert.AreEqual("Description", character.Resistances[0].Description);
        }

        [TestMethod]
        public void Apply_AlreadyExists_FirstHigherValue_Test()
        {
            var character = CreateMockCharacter();
            var resistance = new Resistance("Test", "Description", 1, 3);
            resistance.Apply(character);
            var resistance2 = new Resistance("Test 2", "Description 2", 1, 2);
            resistance2.Apply(character);
            Assert.AreEqual(1, character.Resistances.Count);
            Assert.AreEqual(3, character.Resistances[0].Value);
            Assert.AreEqual("Test", character.Resistances[0].Name);
            Assert.AreEqual("Description", character.Resistances[0].Description);
        }

        [TestMethod]
        public void Remove_Exists_Tests()
        {
            var character = CreateMockCharacter();
            var resistance = new Resistance("Test", "Description", 1, 2);
            resistance.Apply(character);
            resistance.Remove(character);
            Assert.AreEqual(0, character.Resistances.Count);
        }

        [TestMethod]
        public void Remove_NotExists_Tests()
        {
            var character = CreateMockCharacter();
            var resistance = new Resistance("Test", "Description", 1, 2);
            resistance.Apply(character);
            var resistance2 = new Resistance("Test 2", "Description 2", 2, 1);
            resistance2.Remove(character);
            Assert.AreEqual(1, character.Resistances.Count);
            // If we made it here without exception no errors occured
        }

        private ICharacter CreateMockCharacter()
        {
            var character = Mock.Of<ICharacter>();
            Mock.Get(character).Setup(m => m.Resistances).Returns(new List<IResistance>());
            return character;
        }
    }    
}