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
    public class ItemBLOTest
    {
        [TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestReadAllItems()
        {//public List<Items> ReadAllItems()
            //Arrange
            //Set up the Mapper
            IItemMapper Mapper = MockRepository.GenerateMock<IItemMapper>();
            //Set up the return from the mapper
            List<IItemDO> DataObjects = new List<IItemDO>();
            DataObjects.Add(new ItemDO()
            {
                PKItemID = 1,
                ItemName = "TestItem",
                Details = "TestDescription",
                Cost = 5558887777,
                SurvRate = 10
            });
            DataObjects.Add(new ItemDO()
            {
                PKItemID = 2,
                ItemName = "TestItem",
                Details = "TestDescription",
                Cost = 5558887777,
                SurvRate = 10
            });
            DataObjects.Add(new ItemDO()
            {
                PKItemID = 3,
                ItemName = "TestItem",
                Details = "TestDescription",
                Cost = 5558887777,
                SurvRate = 10
            });
            //Set up expected return FROM the BLo method
            List<Items> Expected = new List<Items>();
            Expected.Add(new Items()
            {
                PKItemID = 1,
                ItemName = "TestItem",
                Details = "TestDescription",
                Cost = 5558887777,
                SurvRate = 10
            });
            Expected.Add(new Items()
            {
                PKItemID = 2,
                ItemName = "TestItem",
                Details = "TestDescription",
                Cost = 5558887777,
                SurvRate = 10
            });
            Expected.Add(new Items()
            {
                PKItemID = 3,
                ItemName = "TestItem",
                Details = "TestDescription",
                Cost = 5558887777,
                SurvRate = 10
            });
            //Stub out the Mapper Method with Return
            Mapper.Stub(mapper => mapper.ReadAllItems()).Return(DataObjects);
            //Set up BLO
            ItemBLO BLO = new ItemBLO(Mapper);

            //Act
            //Call method you're testing
            List<Items> Actual = BLO.ReadAllItems();
            //Assert
            //Asserting items "IsNotNull"
            //Asserting items "AreEqual"
            Assert.IsNotNull(Expected);
            Assert.AreEqual(Actual[0].PKItemID, Expected[0].PKItemID);
            Assert.AreEqual(Actual[0].ItemName, Expected[0].ItemName);
            Assert.AreEqual(Actual[0].Details, Expected[0].Details);
            Assert.AreEqual(Actual[0].Cost, Expected[0].Cost);
            Assert.AreEqual(Actual[0].SurvRate, Expected[0].SurvRate);
            Assert.AreEqual(Actual[1].PKItemID, Expected[1].PKItemID);
            Assert.AreEqual(Actual[1].ItemName, Expected[1].ItemName);
            Assert.AreEqual(Actual[1].Details, Expected[1].Details);
            Assert.AreEqual(Actual[1].Cost, Expected[1].Cost);
            Assert.AreEqual(Actual[1].SurvRate, Expected[1].SurvRate);
            Assert.AreEqual(Actual[2].PKItemID, Expected[2].PKItemID);
            Assert.AreEqual(Actual[2].ItemName, Expected[2].ItemName);
            Assert.AreEqual(Actual[2].Details, Expected[2].Details);
            Assert.AreEqual(Actual[2].Cost, Expected[2].Cost);
            Assert.AreEqual(Actual[2].SurvRate, Expected[2].SurvRate);
            Mapper.AssertWasCalled(mapper => mapper.ReadAllItems());
        }
    }
}
