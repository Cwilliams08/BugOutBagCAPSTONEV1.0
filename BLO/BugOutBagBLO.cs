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
    public class BugOutBagBLO : IBugOutBagBLO
    {
        IBugOutBagMapper _BugOutBagMapper;
        IBobOwnerMapper _BobOwnerMapper;
        IBagContentsMapper _BagContentsMapper;
        private IBugOutBagMapper BugOutBagMapperToPassToBLO;


        public BugOutBagBLO(IBugOutBagMapper iBugOutBagMapper, IBobOwnerMapper iBobOwnerMapper, IBagContentsMapper iBagContentsMapper)
        {
            _BugOutBagMapper = iBugOutBagMapper;
            _BobOwnerMapper = iBobOwnerMapper;
            _BagContentsMapper = iBagContentsMapper;
        }

        public BugOutBagBLO(IBugOutBagMapper BugOutBagMapperToPassToBLO)
        {
            // TODO: Complete member initialization
            this.BugOutBagMapperToPassToBLO = BugOutBagMapperToPassToBLO;
        }




        public void AddBugOutBag(BugOutBag bugoutbag)
        {
            IBugOutBagDO BugOutBagToAdd = new BugOutBagDO();
            BugOutBagToAdd.FKUserID = _BobOwnerMapper.ReadUserID(bugoutbag.UserName);
            BugOutBagToAdd.BagName = bugoutbag.BagName;
            BugOutBagToAdd.TotalCost = bugoutbag.TotalCost;
            BugOutBagToAdd.TotalSurvRate = bugoutbag.TotalSurvRate;
            _BugOutBagMapper.AddBugOutBag(BugOutBagToAdd);

        }


        public void DeleteBugOutBag(BugOutBag bugoutbag)
        {
            IBugOutBagDO BugOutBagToDelete = new BugOutBagDO();
            BugOutBagToDelete.PKBugOutBagID = bugoutbag.PKBugOutBagID;
            _BugOutBagMapper.DeleteBugOutBag(BugOutBagToDelete);
        }


        public void UpdateBugOutBag(BugOutBag bugoutbag)
        {
            IBugOutBagDO BugOutBagToUpdate = new BugOutBagDO();
            BugOutBagToUpdate.PKBugOutBagID = bugoutbag.PKBugOutBagID;
            BugOutBagToUpdate.FKUserID = bugoutbag.FKUserID;
            BugOutBagToUpdate.BagName = bugoutbag.BagName;
            BugOutBagToUpdate.TotalCost = bugoutbag.TotalCost;
            BugOutBagToUpdate.TotalSurvRate = bugoutbag.TotalSurvRate;
            _BugOutBagMapper.UpdateBugOutBag(BugOutBagToUpdate);
        }



        public List<BugOutBag> ReadAllBugOutBags()
        {
            //get list of data objects from mapper
            List<BugOutBag> BugOutBags = new List<BugOutBag>();
            //translate data objects into list of business objects
            List<IBugOutBagDO> DataObjects = _BugOutBagMapper.ReadAllBugOutBags();
            //pass info from data objects to business objects
            BugOutBags = (from DataObject in DataObjects
                          select new BugOutBag()
                          {
                              PKBugOutBagID = DataObject.PKBugOutBagID,
                              BagName = DataObject.BagName,
                              TotalCost = DataObject.TotalCost,
                              TotalSurvRate = DataObject.TotalSurvRate
                          }).ToList();
            //return list of business objects to presentation layer
            return BugOutBags;
        }



        public BugOutBag ReadBugOutBagByID(int PKBugOutBagID)
        {
            //create bug out bag business object
            BugOutBag Bag = new BugOutBag();
            //get bug out bag data objects
            IBugOutBagDO DataObjects = _BugOutBagMapper.ReadBugOutBagByID(PKBugOutBagID);
            //fill bugout bag contents from bag contents mapper
            Bag.ContentsOfBag = ReadBagContentsByBagID(DataObjects.PKBugOutBagID);
                //fill bag items from bag contents
                Bag.BagItems = GetItemPerContents(Bag.ContentsOfBag);
            //calculate survival rate increase
            //calculate total bag cost
                return Bag;
        }

        private List<Items> GetItemPerContents(List<BagContents> iContentsOfBag)
        {
            //get list of data objects from mapper
            List<Items> ContentsOfBag = new List<Items>();
            //translate data objects into list of business objects
            List<IBagContentsDO> DataObjects = _BagContentsMapper.GetItemPerContents(iContentsOfBag.First().FKItemID);
            //pass info from data objects to business objects
            //ContentsOfBag = (from DataObject in DataObjects
            //                 select new BagContents()
            //                 {
            //                     PKBagContentsID = DataObject.PKBagContentsID,
            //                     FKBugOutBagID = DataObject.FKBugOutBagID,
            //                     FKItemID = DataObject.FKItemID
            //                 }).Cast<IBagContentsDO>().ToList();
            //return list of business objects to presentation layer
            return ContentsOfBag;
        }

        public List<BagContents> ReadBagContentsByBagID(int iBagID)
        {
            //create list of contents data objects
            List<IBagContentsDO> DataObjects = _BugOutBagMapper.ReadContentsByBagID(iBagID);
            //create list of contents business objects
            List<BagContents> BagContents = new List<BagContents>();
            //go through data object to fill contents
            foreach (BagContentsDO x in DataObjects)
            {
                BagContents.Add(new BagContents()
                {
                    PKBagContentsID = x.PKBagContentsID,
                    FKItemID = x.FKItemID,
                    FKBugOutBagID = x.FKBugOutBagID
                });
            }
            return BagContents;
        }

        public BugOutBag ViewBugOutBag(BugOutBag ibugOutBag)
        {
            //create bug out bag business object
            IBugOutBagDO ViewBugOutBagContents = new BugOutBagDO();
            //get bag data object
            ibugOutBag.ContentsOfBag = ReadBagContentsByBagID(ibugOutBag.PKBugOutBagID);
            //fill bag contents from bag contents mapper
            //ibugOutBag.BagItems = ReadBagContentsByBagID(ibugOutBag.PKBugOutBagID);
            //fill bag items from bag contents
            ViewBugOutBagContents= _BugOutBagMapper.ReadBugOutBagByID(ibugOutBag.PKBugOutBagID);
            ibugOutBag.FKUserID = ViewBugOutBagContents.FKUserID;
            ibugOutBag.BagName = ViewBugOutBagContents.BagName;
            ibugOutBag.TotalCost = ViewBugOutBagContents.TotalCost;
            ibugOutBag.TotalSurvRate = ViewBugOutBagContents.TotalSurvRate;
            //calculate cost
            //calculate survival rate increase
            return ibugOutBag;
        }



        //public void AddTotal (decimal Cost, int PKBagID)
        //{
        //    //ask database for bag info
        //    (x= Items.Cost)
        //    BugOutBag.TotalCost (x += Cost);
        //    //update to DAL
        //
        //}

        public void ViewBagList(BugOutBag bugOutBag)
        {
            IBugOutBagDO BugOutBagViewList = new BugOutBagDO();
            BugOutBagViewList.PKBugOutBagID = bugOutBag.PKBugOutBagID;
            BugOutBagViewList.BagName = bugOutBag.BagName;
            _BugOutBagMapper.ViewBagList(BugOutBagViewList);
        }




















    }
}


