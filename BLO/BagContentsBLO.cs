using BusinessLogicLayer.BusinessObjects;
using BusinessLogicLayer.IBLO;
using DataAccessLayer.DataObjects;
using IDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BLO
{
    public class BagContentsBLO : IBagContentsBLO
    {
       IBagContentsMapper _BagContentsMapper;
       IBobOwnerMapper _BobOwnerMapper;
       IItemMapper _ItemMapper;
       IBugOutBagMapper _BugOutBagMapper;

       private IBagContentsMapper BagContentsToPassToBLO;

       public BagContentsBLO(IBagContentsMapper iBagContentsMapper, IBobOwnerMapper iBobOwnerMapper)
       {
           _BagContentsMapper = iBagContentsMapper;
           _BobOwnerMapper = iBobOwnerMapper;
       }


        public List<BagContents> ReadAllBagContents()
        {
            //get list of data objects from mapper
            List<BagContents> BagContents = new List<BagContents>();
            //translate data objects into list of business objects
            List<IBagContentsDO> DataObjects = _BagContentsMapper.ReadAllBagContents();
            //pass info from data objects to business objects
            BagContents = (from DataObject in DataObjects
                           select new BagContents()
                           {
                               PKBagContentsID = DataObject.PKBagContentsID,
                               FKBugOutBagID = DataObject.FKBugOutBagID,
                               FKItemID = DataObject.FKItemID
                           }).ToList();
            //return list of business objects to presentation layer
            return BagContents;
        }

        public List<BagContents> ReadContentsByBagID(int PKBugOutBagID)
        {
            //get list of data objects from mapper
            List<BagContents> BagContents = new List<BagContents>();
            //translate data objects into list of business objects
            List<IBagContentsDO> DataObjects = _BugOutBagMapper.ReadContentsByBagID(PKBugOutBagID);
            //pass info from data objects to business objects
            BagContents = (from DataObject in DataObjects
                           select new BagContents()
                           {
                               PKBagContentsID = DataObject.PKBagContentsID,
                               FKBugOutBagID = DataObject.FKBugOutBagID,
                               FKItemID = DataObject.FKItemID
                           }).ToList();
            //return lsit of business objects to presentation layer
            return BagContents;
        

                           
        }




        public void AddContentsToBugOutBag(Items items, BugOutBag bugOutBag, BagContents bagContents)
        {
            IBagContentsDO AddContentsToBugOutBag = new BagContentsDO();
            AddContentsToBugOutBag.FKItemID = items.PKItemID;
            AddContentsToBugOutBag.FKBugOutBagID = bugOutBag.PKBugOutBagID;
            _BagContentsMapper.AddContentsToBugOutBag(AddContentsToBugOutBag);
        }

        public void DeleteContentsFromBugOutBag(BagContents bagContents)
        {
            IBagContentsDO DeleteContentsFromBugOutBag = new BagContentsDO();
            DeleteContentsFromBugOutBag.PKBagContentsID = bagContents.PKBagContentsID;
            _BagContentsMapper.DeleteContentsFromBugOutBag(DeleteContentsFromBugOutBag);
        }
    }
    }

