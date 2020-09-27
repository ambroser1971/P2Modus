using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class BrokenTests
    {
        [TestMethod]
        public void Apply_NotAnObject_Test()
        {
            var entity = Mock.Of<IEntity>();
            var Broken = new Broken();
            Broken.Apply(entity);
            Assert.IsTrue(true); // If we made it here with exception then the test passed
        }

        [TestMethod]
        public void Apply_IsObject_Test()
        {
            var obj =CreateMockObject();
            var broken = new Broken();
            broken.Apply(obj);
            Assert.AreSame(broken, obj.Conditions[0]);
        }


        [TestMethod]
        [ExpectedException(typeof(ConditionExistsException))]
        public void Apply_Character_WithExistingBrokenCondition_Test()
        {
            var obj = CreateMockObject();
            obj.Conditions.Add(new Broken());
            var broken = new Broken();
            broken.Apply(obj);
        }

        private IObject CreateMockObject()
        {
            var obj = Mock.Of<IObject>();
            Mock.Get(obj).Setup(m => m.Conditions).Returns(new List<ICondition>());
            return obj;
        }
    }
}   