using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IDataAccessLayer;
using Rhino.Mocks;
using DataAccessLayer;
using System.Collections.Generic;
using BusinessLogicLayer.BusinessObjects;
using BusinessLogicLayer;
using DataAccessLayer.DataObjects;
using BusinessLogicLayer.BLO;

namespace BugOutBagCAPSTONE.Tests
{
    [TestClass]
    public class BugOutBagBLOTest
    {
        [TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestReadAllBugOutBags()
        {//public List<BugOutBag> ReadAllBugOutBags()
            //Arrange
            //Set up the Mapper
            IBugOutBagMapper Mapper = MockRepository.GenerateMock<IBugOutBagMapper>();
            //Set up the return from the mapper
            List<IBugOutBagDO> DataObjects = new List<IBugOutBagDO>();
            DataObjects.Add(new BugOutBagDO()
            {
                PKBugOutBagID = 1,
                BagName = "TestBag",
                TotalCost = 400,
                TotalSurvRate = 55
            });
            DataObjects.Add(new BugOutBagDO()
            {
                PKBugOutBagID = 2,
                BagName = "TestBag",
                TotalCost = 400,
                TotalSurvRate = 55
            });
            DataObjects.Add(new BugOutBagDO()
            {
                PKBugOutBagID = 3,
                BagName = "TestBag",
                TotalCost = 400,
                TotalSurvRate = 55
            });
            //Set up expected return FROM the BLo method
            List<BugOutBag> Expected = new List<BugOutBag>();
            Expected.Add(new BugOutBag()
            {
                PKBugOutBagID = 1,
                BagName = "TestBag",
                TotalCost = 400,
                TotalSurvRate = 55
            });
            Expected.Add(new BugOutBag()
            {
                PKBugOutBagID = 2,
                BagName = "TestBag",
                TotalCost = 400,
                TotalSurvRate = 55
            });
            Expected.Add(new BugOutBag()
            {
                PKBugOutBagID = 3,
                BagName = "TestBag",
                TotalCost = 400,
                TotalSurvRate = 55
            });
            //Stub out the Mapper Method with Return
            Mapper.Stub(mapper => mapper.ReadAllBugOutBags()).Return(DataObjects);
            //Set up BLO
            BugOutBagBLO BLO = new BugOutBagBLO(Mapper);

            //Act
            //Call method you're testing
            List<BugOutBag> Actual = BLO.ReadAllBugOutBags();
            //Assert
            //Asserting items "IsNotNull"
            //Asserting items "AreEqual"
            Assert.IsNotNull(Expected);
            Assert.AreEqual(Actual[0].PKBugOutBagID, Expected[0].PKBugOutBagID);
            Assert.AreEqual(Actual[0].BagName, Expected[0].BagName);
            Assert.AreEqual(Actual[0].TotalCost, Expected[0].TotalCost);
            Assert.AreEqual(Actual[0].TotalSurvRate, Expected[0].TotalSurvRate);
            Assert.AreEqual(Actual[1].PKBugOutBagID, Expected[1].PKBugOutBagID);
            Assert.AreEqual(Actual[1].BagName, Expected[1].BagName);
            Assert.AreEqual(Actual[1].TotalCost, Expected[1].TotalCost);
            Assert.AreEqual(Actual[1].TotalSurvRate, Expected[1].TotalSurvRate);
            Assert.AreEqual(Actual[2].PKBugOutBagID, Expected[2].PKBugOutBagID);
            Assert.AreEqual(Actual[2].BagName, Expected[2].BagName);
            Assert.AreEqual(Actual[2].TotalCost, Expected[2].TotalCost);
            Assert.AreEqual(Actual[2].TotalSurvRate, Expected[2].TotalSurvRate);
            Mapper.AssertWasCalled(mapper => mapper.ReadAllBugOutBags());
        }
    }
}

