using BusinessLogicLayer.BusinessObjects;
using BusinessLogicLayer.IBLO;
using CryptSharp;
using DataAccessLayer.DataObjects;
using IDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BLO
{
    public class BobOwnerBLO : IBobOwnerBLO
    {
        IBobOwnerMapper _BobOwnerMapper;

        public BobOwnerBLO(IBobOwnerMapper iBobOwnerMapper)
        {
            _BobOwnerMapper = iBobOwnerMapper;
        }

        //public BobOwnerBLO(IBobOwnerMapper MapperToPassToBLO)
        //{
        //    _BobOwnerMapper = MapperToPassToBLO;
        //}

        public BobOwner ValidateBobOwner(BobOwner bobOwner)
        {
            //get Bobowner Data Object from mapper per UserID
            IBobOwnerDO BobOwnerFromDataBase = _BobOwnerMapper.ReadBobOwnerByUserName(bobOwner.UserName);
            //Ensure password is valid according to database
            bobOwner.IsValid = Crypter.CheckPassword(bobOwner.PASSWORD + BobOwnerFromDataBase.SALT, BobOwnerFromDataBase.PASSWORD);
            //if it's valid pass over the details of the Bobowner
            if (bobOwner.IsValid)
            {
                bobOwner.PKUserID = BobOwnerFromDataBase.PKUserID;
                bobOwner.UserName = BobOwnerFromDataBase.UserName;
                bobOwner.PHONENUM = BobOwnerFromDataBase.PHONENUM;
                bobOwner.UserRole = ReadRoleByID(BobOwnerFromDataBase.FKRoleID);
            }
            else
            {
                //do nothing
            }
            //pass back business object
            return bobOwner;
            }

        public Role ReadRoleByID(int PKRoleID)
        {
            //get  of data objects from mapper
            //translate data objects into  of BO
            IRoleDO DataObjects = _BobOwnerMapper.ReadRoleByID(PKRoleID);
            Role UserRole = new Role();
            UserRole.PKRoleID = DataObjects.PKRoleID;
            UserRole.ROLE = DataObjects.ROLE;
            //return  of business objects to presentation layer
            return UserRole;
        }

        public List<BobOwner> ReadAllBobOwners()
        {
            //get list of data opb jects from mapper
            List<BobOwner> BobOwners = new List<BobOwner>();
            //translate data objects into list of BO
            List<IBobOwnerDO> DataObjects = _BobOwnerMapper.ReadAllBobOwners();
            //pass info from data objects to business objects
            BobOwners = (from DataObject in DataObjects
                         select new BobOwner()
                         {
                             PKUserID = DataObject.PKUserID,
                             UserName = DataObject.UserName,
                             PASSWORD = DataObject.PASSWORD,
                             PHONENUM = DataObject.PHONENUM
                         }).ToList();
            //return list of business objects to presentation layer
            return BobOwners;
        }

        public void AddBobOwner(BobOwner bobOwner)
        {
            IBobOwnerDO BobOwnerToAdd = new BobOwnerDO();
            BobOwnerToAdd.FKRoleID = bobOwner.UserRole.PKRoleID;
            BobOwnerToAdd.UserName = bobOwner.UserName;
            BobOwnerToAdd.PHONENUM = bobOwner.PHONENUM;
            BobOwnerToAdd.PASSWORD = bobOwner.PASSWORD;
            BobOwnerToAdd.SALT = Crypter.Blowfish.GenerateSalt();
            BobOwnerToAdd.EncryptedPassword = Crypter.Blowfish.Crypt(bobOwner.PASSWORD + BobOwnerToAdd.SALT);
            _BobOwnerMapper.AddBobOwner(BobOwnerToAdd);
        }

        public void RegisterBobOwner(BobOwner bobOwner)
        {
            IBobOwnerDO BobOwnerToAdd = new BobOwnerDO();
            BobOwnerToAdd.FKRoleID = bobOwner.UserRole.PKRoleID;
            BobOwnerToAdd.UserName = bobOwner.UserName;
            BobOwnerToAdd.PHONENUM = bobOwner.PHONENUM;
            BobOwnerToAdd.PASSWORD = bobOwner.PASSWORD;
            BobOwnerToAdd.SALT = Crypter.Blowfish.GenerateSalt();
            BobOwnerToAdd.EncryptedPassword = Crypter.Blowfish.Crypt(bobOwner.PASSWORD + BobOwnerToAdd.SALT);
            _BobOwnerMapper.AddBobOwner(BobOwnerToAdd);
        }

        public void UpdateBobOwner(BobOwner bobOwner)
        {
            IBobOwnerDO BobOwnerToUpdate = new BobOwnerDO();
            BobOwnerToUpdate.PKUserID = bobOwner.PKUserID;
            BobOwnerToUpdate.FKRoleID = bobOwner.UserRole.PKRoleID;
            BobOwnerToUpdate.UserName = bobOwner.UserName;
            BobOwnerToUpdate.PHONENUM = bobOwner.PHONENUM;
            _BobOwnerMapper.UpdateBobOwner(BobOwnerToUpdate);
        }

        public void DeleteBobOwner(BobOwner bobOwner)
        {
            IBobOwnerDO BobOwnerToDelete = new BobOwnerDO();
            BobOwnerToDelete.PKUserID = bobOwner.PKUserID;
            _BobOwnerMapper.DeleteBobOwner(BobOwnerToDelete);
        }




        public void SearchBobOwner(BobOwner bobowner)
        {
            throw new NotImplementedException();
        }


        public List<Role> ReadAllUserRoles()
        {
            //get list of data objects from mapper
            List<Role> UserRoles = new List<Role>();
            //translate data objects into list of BO
            List<IRoleDO> DataObjects = _BobOwnerMapper.ReadAllUserRoles();
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




        public IBobOwnerDO ReadBobOwnerByUserID(int PKUserID)
        {
            throw new NotImplementedException();
        }
    }
    }

