using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class ConditionBaseTests
    {
        [TestMethod]
        public void Constructor_Test()
        {
            var first = new ReturnsTrueCondition();
            Assert.AreEqual("returns true", first.Name);
            Assert.AreEqual("description true", first.Description);
            Assert.AreEqual(1, first.Id);
            Assert.AreEqual(1, first.AppliesTo[0]);

            var second = new ReturnsFalseCondition();
            Assert.AreEqual("returns false", second.Name);
            Assert.AreEqual("description false", second.Description);
            Assert.AreEqual(-1, second.Id);
            Assert.AreEqual(-1, second.AppliesTo[0]);
        }

        [TestMethod]
        public void ToString_Test()
        {
            var target = new ReturnsFalseCondition();
            Assert.AreEqual("returns false", target.ToString());
        }
        
        [TestMethod]
        public void LevelChanged_WhenLevelSupport_Test()
        {
            var target = new ReturnsTrueCondition();
            var oldLevel = target.Level;
            target.Level = 2;
            Assert.AreNotEqual(oldLevel, target.Level);
            Assert.AreEqual(2, target.Level);
        }
        
        [TestMethod]
        public void LevelNotChanged_WhenLevelNotSupport_Test()
        {
            var target = new ReturnsFalseCondition();
            var oldLevel = target.Level;
            target.Level = 2;
            Assert.AreEqual(oldLevel, target.Level);
            Assert.AreNotEqual(2, target.Level);
        }

        [TestMethod]
        public void ConditionAddedEvent_RaisedWhenCustomApplyReturnsTrue_Test()
        {
            var receivedEvents = new List<string>();
            var entity = Mock.Of<IEntity>();
            var target = new ReturnsTrueCondition();
            target.ConditionAddedEvent += delegate(object sender, ConditionEventArgs args)
            {
                receivedEvents.Add(args.Condition.ToString());
            };
            target.Apply(entity);
            Assert.IsTrue(receivedEvents.Contains("returns true"));
        }

        [TestMethod]
        public void ConditionAddedEvent_NotRaisedWhenCustomApplyReturnsFalse_Test()
        {
            var receivedEvents = new List<string>();
            var entity = Mock.Of<IEntity>();
            var target = new ReturnsFalseCondition();
            target.ConditionAddedEvent += delegate(object sender, ConditionEventArgs args)
            {
                receivedEvents.Add(args.Condition.ToString());
            };
            target.Apply(entity);
            Assert.AreEqual(0, receivedEvents.Count);
        }

        [TestMethod]
        public void ConditionRemovedEvent_RaisedWhenCustomRemoveReturnsTrue_Test()
        {
            var receivedEvents = new List<string>();
            var entity = Mock.Of<IEntity>();
            var target = new ReturnsTrueCondition();
            target.ConditionRemovedEvent += delegate(object sender, ConditionEventArgs args)
            {
                receivedEvents.Add(args.Condition.ToString());
            };
            target.Apply(entity);
            target.Remove(entity);
            Assert.IsTrue(receivedEvents.Contains("returns true"));
        }

        [TestMethod]
        public void ConditionRemovedEvent_NotRaisedWhenCustomRemoveReturnsFalse_Test()
        {
            var receivedEvents = new List<string>();
            var entity = Mock.Of<IEntity>();
            var target = new ReturnsFalseCondition();
            target.ConditionRemovedEvent += delegate(object sender, ConditionEventArgs args)
            {
                receivedEvents.Add(args.Condition.ToString());
            };
            target.Apply(entity);
            target.Remove(entity);
            Assert.AreEqual(0, receivedEvents.Count);
        }

        [TestMethod]
        public void ConditionLevelAdjustedEvent_RaisedWhenLevelSupported_Test()
        {
            var receivedEvents = new List<string>();
            var entity = Mock.Of<IEntity>();
            var target = new ReturnsTrueCondition();
            target.ConditionLevelAdjustedEvent += delegate(object sender, ConditionEventArgs args)
            {
                receivedEvents.Add(args.Condition.ToString());
            };
            target.Level = 7;
            Assert.IsTrue(receivedEvents.Contains("returns true"));
        }

        [TestMethod]
        public void ConditionLevelAdjustedEvent_NotRaisedWhenLevelNotSupported_Test()
        {
            var receivedEvents = new List<string>();
            var entity = Mock.Of<IEntity>();
            var target = new ReturnsFalseCondition();
            target.ConditionLevelAdjustedEvent += delegate(object sender, ConditionEventArgs args)
            {
                receivedEvents.Add(args.Condition.ToString());
            };
            target.Level = 7;
            Assert.AreEqual(0, receivedEvents.Count);
        }
        
        public class ReturnsTrueCondition : ConditionBase
        {
            public ReturnsTrueCondition()
                :base("returns true", "description true", 1)
            { }

            public override bool SupportsLevel { get => true; }

            protected override bool CustomApply(IEntity entity)
            {
                return true;
            }

            protected override bool CustomRemove(IEntity entity)
            {
                return true;
            }

            protected override void SetupAppliesToList()
            {
                _appliesToList.Add(1);
            }
        }

        public class ReturnsFalseCondition : ConditionBase
        {
            public ReturnsFalseCondition()
                :base("returns false", "description false", -1)
            { }
            protected override bool CustomApply(IEntity entity)
            {
                return false;
            }

            protected override bool CustomRemove(IEntity entity)
            {
                return false;
            }

            protected override void SetupAppliesToList()
            {
                _appliesToList.Add(-1);
            }
        }
    }
}
