using System;
using System.Collections.Generic;
using System.Linq;
using P2Modus.Core.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace P2Modus.Core.Tests
{
    [TestClass]
    public class EntityIdsTests
    {
        [TestMethod]
        public void ExtensionMethod_IList_IConditions_HasConditionId_True_Test()
        {
            var conditionList = new List<ICondition>();
            conditionList.Add(new Blinded());
            Assert.IsTrue(conditionList.HasConditionId(EntityIds.BLINDED_CONDITION_ID));
        }

        [TestMethod]
        public void ExtensionMethod_IList_IConditions_HasConditionId_False_Test()
        {
            var conditionList = new List<ICondition>();
            Assert.IsFalse(conditionList.HasConditionId(EntityIds.BLINDED_CONDITION_ID));
            conditionList.Add(new Blinded());
            Assert.IsFalse(conditionList.HasConditionId(EntityIds.WOUNDED_CONDITION_ID));
        }

        [TestMethod]
        public void ExtensionMethod_IList_IConditions_GetById_ReturnsNull_Test()
        {
            var conditionList = new List<ICondition>();
            var condition = conditionList.GetById(EntityIds.BLINDED_CONDITION_ID);
            Assert.IsNull(condition);            
        }

        [TestMethod]
        public void ExtensionMethod_IList_IConditions_GetById_ReturnsCondition_Test()
        {
            var conditionList = new List<ICondition>();
            var blinded = new Blinded();
            conditionList.Add(blinded);
            var condition = conditionList.GetById(EntityIds.BLINDED_CONDITION_ID);
            Assert.AreSame(blinded, condition);
        }

        [TestMethod]
        public void ExtensionMethod_IList_IConditions_GetById_ClassConversion_ReturnsCondition_Test()
        {
            var conditionList = new List<ICondition>();
            var blinded = new Blinded();
            conditionList.Add(blinded);
            var condition = conditionList.GetById<Blinded>(EntityIds.BLINDED_CONDITION_ID);
            Assert.AreSame(blinded, condition);            
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void ExtensionMethod_IList_IConditions_GetById_ClassConversion_ThrowsException_Test()
        {
            var conditionList = new List<ICondition>();
            var blinded = new Blinded();
            conditionList.Add(blinded);
            var condition = conditionList.GetById<Wounded>(EntityIds.BLINDED_CONDITION_ID);
            Assert.AreSame(blinded, condition);
        }
    }
}