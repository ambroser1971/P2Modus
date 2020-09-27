using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class RollResultTests
    {
        [TestMethod]
        public void CheckResult_NoAdjustments_IsCriticalSuccess_Test()
        {
            short rollValue = 15;
            short dc = 5;
            Assert.AreEqual(CheckResult.CriticalSuccess, new RollResult().GetResult(rollValue, dc));
        }

        [TestMethod]
        public void CheckResult_NoAdjustments_IsSuccess_Test()
        {
            short rollValue = 14;
            short dc = 5;
            Assert.AreEqual(CheckResult.Success, new RollResult().GetResult(rollValue, dc));
        }

        [TestMethod]
        public void CheckResult_NoAdjustments_IsFailure_Test()
        {
            short rollValue = 5;
            short dc = 14;
            Assert.AreEqual(CheckResult.Failure, new RollResult().GetResult(rollValue, dc));
        }

        [TestMethod]
        public void CheckResult_NoAdjustments_IsCriticalFailure_Test()
        {
            short rollValue = 5;
            short dc = 15;
            Assert.AreEqual(CheckResult.CriticalFailure, new RollResult().GetResult(rollValue, dc));
        }
        
        [TestMethod]
        public void CheckResult_WithAdjustments_IsCriticalSuccess_Test()
        {
            short rollValue = 10;
            short dc = 5;
            var modifiers = new ModifierBag() {
                new Modifier() { Type = ModifierType.Ability, Value = 4 },
                new Modifier() { Type = ModifierType.Status, Value = 1}
            };
            Assert.AreEqual(CheckResult.CriticalSuccess, new RollResult().GetResult(rollValue, modifiers, dc));
        }
        
        [TestMethod]
        public void CheckResult_WithAdjustments_IsSuccess_Test()
        {
            short rollValue = 9;
            short dc = 5;
            var modifiers = new ModifierBag() {
                new Modifier() { Type = ModifierType.Ability, Value = 4 },
                new Modifier() { Type = ModifierType.Status, Value = 1}
            };
            Assert.AreEqual(CheckResult.Success, new RollResult().GetResult(rollValue, modifiers, dc));
        }
        
        [TestMethod]
        public void CheckResult_WithAdjustments_IsCriticalFailure_Test()
        {
            short rollValue = 10;
            short dc = 25;
            var modifiers = new ModifierBag() {
                new Modifier() { Type = ModifierType.Ability, Value = 4 },
                new Modifier() { Type = ModifierType.Status, Value = 1}
            };
            Assert.AreEqual(CheckResult.CriticalFailure, new RollResult().GetResult(rollValue, modifiers, dc));
        }
        
        [TestMethod]
        public void CheckResult_WithAdjustments_IsFailure_Test()
        {
            short rollValue = 11;
            short dc = 25;
            var modifiers = new ModifierBag() {
                new Modifier() { Type = ModifierType.Ability, Value = 4 },
                new Modifier() { Type = ModifierType.Status, Value = 1}
            };
            Assert.AreEqual(CheckResult.Failure, new RollResult().GetResult(rollValue, modifiers, dc));
        }
    }
}
