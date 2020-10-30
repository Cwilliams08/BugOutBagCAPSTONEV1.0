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
    public class BobOwnerBLOTest
    {
        [TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestReadAllBobOwners()
        {//public List<BobOwner> ReadAllBobOwners()
            //Arrange
            //Set up the Mapper
            IBobOwnerMapper Mapper = MockRepository.GenerateMock<IBobOwnerMapper>();
            //Set up the return from the mapper
            List<IBobOwnerDO> DataObjects = new List<IBobOwnerDO>();
            DataObjects.Add(new BobOwnerDO()
            {
                PKUserID = 1,
                UserName = "TestUser",
                PASSWORD = "TestPassword",
                PHONENUM = 5558887777
            });
            DataObjects.Add(new BobOwnerDO()
            {
                PKUserID = 2,
                UserName = "TestUser2",
                PASSWORD = "TestPassword2",
                PHONENUM = 7776665555
            });
            DataObjects.Add(new BobOwnerDO()
            {
                PKUserID = 3,
                UserName = "TestUser3",
                PASSWORD = "TestPassword3",
                PHONENUM = 99988877777
            });
            //Set up expected return FROM the BLo method
            List<BobOwner> Expected = new List<BobOwner>();
            Expected.Add(new BobOwner()
            {
                PKUserID = 1,
                UserName = "TestUser",
                PASSWORD = "TestPassword",
                PHONENUM = 5558887777
            });
            Expected.Add(new BobOwner()
            {
                PKUserID = 2,
                UserName = "TestUser2",
                PASSWORD = "TestPassword2",
                PHONENUM = 7776665555
            });
            Expected.Add(new BobOwner()
            {
                PKUserID = 3,
                UserName = "TestUser3",
                PASSWORD = "TestPassword3",
                PHONENUM = 99988877777
            });
            //Stub out the Mapper Method with Return
            Mapper.Stub(mapper => mapper.ReadAllBobOwners()).Return(DataObjects);
            //Set up BLO
            BobOwnerBLO BLO = new BobOwnerBLO(Mapper);

            //Act
            //Call method you're testing
            List<BobOwner> Actual = BLO.ReadAllBobOwners();
            //Assert
            //Asserting items "IsNotNull"
            //Asserting items "AreEqual"
            Assert.IsNotNull(Expected);
            Assert.AreEqual(Actual[0].PKUserID, Expected[0].PKUserID);
            Assert.AreEqual(Actual[0].UserName, Expected[0].UserName);
            Assert.AreEqual(Actual[0].PASSWORD, Expected[0].PASSWORD);
            Assert.AreEqual(Actual[0].PHONENUM, Expected[0].PHONENUM);
            Assert.AreEqual(Actual[1].PKUserID, Expected[1].PKUserID);
            Assert.AreEqual(Actual[1].UserName, Expected[1].UserName);
            Assert.AreEqual(Actual[1].PASSWORD, Expected[1].PASSWORD);
            Assert.AreEqual(Actual[1].PHONENUM, Expected[1].PHONENUM);
            Assert.AreEqual(Actual[2].PKUserID, Expected[2].PKUserID);
            Assert.AreEqual(Actual[2].UserName, Expected[2].UserName);
            Assert.AreEqual(Actual[2].PASSWORD, Expected[2].PASSWORD);
            Assert.AreEqual(Actual[2].PHONENUM, Expected[2].PHONENUM);
            Mapper.AssertWasCalled(mapper => mapper.ReadAllBobOwners());
        }
    }
}
