using BusinessLogicLayer.BusinessObjects;
using BusinessLogicLayer.IBLO;
using BusinessLogicLayer.BLO;
using CryptSharp;
using DataAccessLayer.DataObjects;
using IDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BLO
{
    public class SupplierBLO : ISupplierBLO
    {
        ISupplierMapper _SupplierMapper;

        public SupplierBLO(ISupplierMapper iSupplierMapper)
        {
            _SupplierMapper = iSupplierMapper;
        }

        //public SupplierBLO(ISupplierMapper MapperToPassToBLO
        //{
        // _SupplierMapper = MapperToPassToBLO;
        //}

        public List<Supplier> ReadAllVendors()
        {
            //get list odf data objects from mapper
            List<Supplier> Suppliers = new List<Supplier>();
            //translate data objects into list of Business objects
            List<ISupplierDO> DataObjects = _SupplierMapper.ReadAllVendors();
            //pass info from data objects to business objects
            Suppliers = (from DataObject in DataObjects
                         select new Supplier()
                         {
                             PKSupplierID = DataObject.PKSupplierID,
                             VendorID = DataObject.VendorID,
                             PHONENUM = DataObject.PHONENUM,
                             PASSWORD = DataObject.PASSWORD,
                         }).ToList();
            //return list of business objects to presentation layer
            return Suppliers;
        }

        public void AddSupplier(Supplier supplier)
        {
            ISupplierDO SupplierToAdd = new SupplierDO();
            SupplierToAdd.FKRoleID = supplier.UserRole.PKRoleID;
            SupplierToAdd.VendorID = supplier.VendorID;
            SupplierToAdd.PHONENUM = supplier.PHONENUM;
            SupplierToAdd.PASSWORD = supplier.PASSWORD;
            SupplierToAdd.SALT = Crypter.Blowfish.GenerateSalt();
            SupplierToAdd.EncryptedPassword = Crypter.Blowfish.Crypt(supplier.PASSWORD + SupplierToAdd.SALT);
            _SupplierMapper.AddSupplier(SupplierToAdd);
        }

        public void UpdateSupplier(Supplier supplier)
        {
            ISupplierDO SupplierToUpdate = new SupplierDO();
            SupplierToUpdate.PKSupplierID = supplier.PKSupplierID;
            SupplierToUpdate.VendorID = supplier.VendorID;
            SupplierToUpdate.PHONENUM = supplier.PHONENUM;
                _SupplierMapper.UpdateSupplier(SupplierToUpdate);
        }

        public void DeleteSupplier(Supplier supplier)
        {
            ISupplierDO SupplierToDelete = new SupplierDO();
            SupplierToDelete.PKSupplierID = supplier.PKSupplierID;
            _SupplierMapper.DeleteSupplier(SupplierToDelete);
        }

        public List<Role> ReadAllUserRoles(int PKRoleID)
        {
            //get list of data objects from mapper
            List<Role> UserRoles = new List<Role>();
            //translate data objects into list of business objects
            List<IRoleDO> DataObjects = _SupplierMapper.ReadAllUserRoles();
            //pass info from data objects to business objects
            UserRoles = (from DataObject in DataObjects
                         select new Role()
                         {
                             PKRoleID = DataObject.PKRoleID,
                             ROLE = DataObject.ROLE
                         }).ToList();
            //return list of business objects to presentation layer
            return UserRoles;
        }

        public Supplier ValidateSupplier(Supplier supplier)
        {
            //get supplier data object from mapper per SupplierID
            ISupplierDO SupplierFromDataBase = _SupplierMapper.ReadSupplierByVendorID(supplier.VendorID);
            //ensure password is valid according to database
            supplier.IsValid = Crypter.CheckPassword(supplier.PASSWORD + SupplierFromDataBase.SALT, SupplierFromDataBase.PASSWORD);
            //if it's valid pass over the details of the supplier
            if (supplier.IsValid)
            {
                supplier.PKSupplierID = SupplierFromDataBase.PKSupplierID;
                supplier.VendorID = SupplierFromDataBase.VendorID;
                supplier.PHONENUM = SupplierFromDataBase.PHONENUM;
                supplier.UserRole = ReadRoleByID(SupplierFromDataBase.FKRoleID);
            }
            else
            {
                //do nothing
            }
            //pass back business object
            return supplier;
        }

        private Role ReadRoleByID(int PKRoleID)
        {
            //get of data objects from mapper
            Role UserRoles = new Role();
            //translate data objects into of business objects
            IRoleDO DataObjects = _SupplierMapper.ReadRoleByID(PKRoleID);
            Role UserRole = new Role();
            UserRole.PKRoleID = DataObjects.PKRoleID;
            UserRole.ROLE = DataObjects.ROLE;
            //return of business objects to presentation layer
            return UserRoles;
        }

        public List<Role> ReadAllUserRoles()
        {
            //get list of data objects from mapper
            List<Role> UserRoles = new List<Role>();
            //translate data objects into list of busienss objects
            List<IRoleDO> DataObjects = _SupplierMapper.ReadAllUserRoles();
            //pass info from data objects to business objects
            UserRoles = (from DataObject in DataObjects
                         select new Role()
                         {
                             PKRoleID = DataObject.PKRoleID,
                             ROLE = DataObject.ROLE
                         }).ToList();
            //return list of business objects to presemntation layer
            return UserRoles;
        }
    }
}
