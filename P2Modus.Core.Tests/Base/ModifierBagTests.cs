using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class ModifierBagTests
    {
        [TestMethod]
        public void Add_TypeAndValue_ReturnsId_Test()
        {
            var bag = new ModifierBag();
            var id = bag.Add(ModifierType.Ability, 1);
            Assert.AreEqual(id, bag[0].Id);
        }

        [TestMethod]
        public void Add_Modifier_IsAdded_Test()
        {
            var mod = new Modifier() { Type = ModifierType.Ability, Value = 1};
            var bag = new ModifierBag();
            bag.Add(mod);
            Assert.AreEqual(mod, bag[0]);
        }

        [TestMethod]
        public void Remove_ByTypeAndValue_Test()
        {
            var type = ModifierType.Ability;
            short value = 1;
            var bag = new ModifierBag();
            var id = bag.Add(type, value);
            Assert.IsTrue(1 == bag.Count);
            Assert.AreEqual(id, bag[0].Id);
        }

        [TestMethod]
        public void Remove_ById_Test()
        {
            var type = ModifierType.Ability;
            short value = 1;
            var bag = new ModifierBag();
            var id = bag.Add(type, value);
            Assert.IsTrue(1 == bag.Count);
            var isRemoved = bag.Remove(id);
            Assert.IsTrue(isRemoved);
            Assert.AreEqual(0, bag.Count);
        }

        [TestMethod]
        public void Remove_ByModifierType_Test()
        {
            var bag = new ModifierBag();
            bag.Add(ModifierType.Ability, 0);
            bag.Add(ModifierType.Proficiency, 4);
            bag.Add(ModifierType.Circumstance, 1);
            bag.Add(ModifierType.Circumstance, -2);
            bag.Add(ModifierType.Status, -1);
            Assert.AreEqual(5, bag.Count);
            var numRemoved = bag.Remove(ModifierType.Circumstance);
            Assert.AreEqual(2, numRemoved);
            Assert.AreEqual(3, bag.Count);
            Assert.AreEqual(0, bag.Count(m => m.Type == ModifierType.Circumstance));
        }

        [TestMethod]
        public void GetTotalModifiers_Test()
        {
            var bag = new ModifierBag();
            bag.Add(ModifierType.Ability, 0);
            bag.Add(ModifierType.Proficiency, 4);
            bag.Add(ModifierType.Circumstance, 1);
            bag.Add(ModifierType.Circumstance, -2);
            bag.Add(ModifierType.Status, -1);
            Assert.AreEqual(5, bag.Count);
            Assert.AreEqual(2, bag.GetTotalModifiers());
        }

        [TestMethod]
        public void GetTotalModifiers_UntypedPenaltiesStack_Test()
        {
            var bag = new ModifierBag();
            bag.Add(ModifierType.Ability, 0);
            bag.Add(ModifierType.Proficiency, 4);
            bag.Add(ModifierType.Circumstance, 1);
            bag.Add(ModifierType.UntypedPenalty, -2);
            bag.Add(ModifierType.UntypedPenalty, -1);
            Assert.AreEqual(5, bag.Count);
            Assert.AreEqual(2, bag.GetTotalModifiers());
        }
    }
}   