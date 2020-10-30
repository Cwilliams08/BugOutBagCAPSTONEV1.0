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
    public class SupplierBLOTest
    {
        [TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestReadAllVendors()
        {//public List<BobOwner> ReadAllBobOwners()
            //Arrange
            //Set up the Mapper
            ISupplierMapper Mapper = MockRepository.GenerateMock<ISupplierMapper>();
            //Set up the return from the mapper
            List<ISupplierDO> DataObjects = new List<ISupplierDO>();
            DataObjects.Add(new SupplierDO()
            {
                PKSupplierID = 1,
                VendorID = "TestUser",
                PASSWORD = "TestPassword",
                PHONENUM = 5558887777
            });
            DataObjects.Add(new SupplierDO()
            {
                PKSupplierID = 2,
                VendorID = "TestUser2",
                PASSWORD = "TestPassword2",
                PHONENUM = 7776665555
            });
            DataObjects.Add(new SupplierDO()
            {
                PKSupplierID = 3,
                VendorID = "TestUser3",
                PASSWORD = "TestPassword3",
                PHONENUM = 99988877777
            });
            //Set up expected return FROM the BLo method
            List<Supplier> Expected = new List<Supplier>();
            Expected.Add(new Supplier()
            {
                PKSupplierID = 1,
                VendorID = "TestUser",
                PASSWORD = "TestPassword",
                PHONENUM = 5558887777
            });
            Expected.Add(new Supplier()
            {
                PKSupplierID = 2,
                VendorID = "TestUser2",
                PASSWORD = "TestPassword2",
                PHONENUM = 7776665555
            });
            Expected.Add(new Supplier()
            {
                PKSupplierID = 3,
                VendorID = "TestUser3",
                PASSWORD = "TestPassword3",
                PHONENUM = 99988877777
            });
            //Stub out the Mapper Method with Return
            Mapper.Stub(mapper => mapper.ReadAllVendors()).Return(DataObjects);
            //Set up BLO
            SupplierBLO BLO = new SupplierBLO(Mapper);

            //Act
            //Call method you're testing
            List<Supplier> Actual = BLO.ReadAllVendors();
            //Assert
            //Asserting items "IsNotNull"
            //Asserting items "AreEqual"
            Assert.IsNotNull(Expected);
            Assert.AreEqual(Actual[0].PKSupplierID, Expected[0].PKSupplierID);
            Assert.AreEqual(Actual[0].VendorID, Expected[0].VendorID);
            Assert.AreEqual(Actual[0].PASSWORD, Expected[0].PASSWORD);
            Assert.AreEqual(Actual[0].PHONENUM, Expected[0].PHONENUM);
            Assert.AreEqual(Actual[1].PKSupplierID, Expected[1].PKSupplierID);
            Assert.AreEqual(Actual[1].VendorID, Expected[1].VendorID);
            Assert.AreEqual(Actual[1].PASSWORD, Expected[1].PASSWORD);
            Assert.AreEqual(Actual[1].PHONENUM, Expected[1].PHONENUM);
            Assert.AreEqual(Actual[2].PKSupplierID, Expected[2].PKSupplierID);
            Assert.AreEqual(Actual[2].VendorID, Expected[2].VendorID);
            Assert.AreEqual(Actual[2].PASSWORD, Expected[2].PASSWORD);
            Assert.AreEqual(Actual[2].PHONENUM, Expected[2].PHONENUM);
            Mapper.AssertWasCalled(mapper => mapper.ReadAllVendors());
        }
    }
}
