using BusinessLogicLayer.BusinessObjects;
using BusinessLogicLayer.IBLO;
using DataAccessLayer;
using DataAccessLayer.DataObjects;
using IDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BLO
{
   public class ItemBLO : IItemBLO
    {
       IItemMapper _ItemMapper;
       ISupplierMapper _SupplierMapper;
       IBagContentsMapper _BagContentsMapper;

       private IItemMapper ItemMapperToPassToBLO;
       private IBagContentsMapper BagContentsMapperToPassToBLO;
       private IItemMapper Mapper;

       public ItemBLO(IItemMapper iItemMapper, ISupplierMapper iSupplierMapper)
       {
           _ItemMapper = iItemMapper;
           _SupplierMapper = iSupplierMapper;
       }

       public ItemBLO(IItemMapper ItemMapperToPassToBLO, IBagContentsMapper BagContentsMapperToPassToBLO)
       {

           _BagContentsMapper = BagContentsMapperToPassToBLO;
           _ItemMapper = ItemMapperToPassToBLO;
       }

       public ItemBLO(IItemMapper Mapper)
       {
           _ItemMapper = Mapper;
       }

       public List<Items> ReadAllItems()
       {
           //get list of data objects from mapper
           List<Items> Items = new List<Items>();
           //tranlsate data objects into list of busienss objects
           List<IItemDO> DataObjects = _ItemMapper.ReadAllItems();
           // pass info from data objects to busienss objects
           Items = (from DataObject in DataObjects
                    select new Items()
                    {
                        PKItemID = DataObject.PKItemID,
                        ItemName = DataObject.ItemName,
                        Details = DataObject.Details,
                        Cost = DataObject.Cost,
                        SurvRate = DataObject.SurvRate
                    }).ToList();
                    //return List of business BusinessObjects to presentation layer
               return Items;
       }

       public void AddItem(Items items)
       {



           IItemDO ItemToAdd = new ItemDO();
           ItemToAdd.FKSupplierID = _SupplierMapper.ReadSupplierID(items.VendorID);
           ItemToAdd.ItemName = items.ItemName;
           ItemToAdd.Details = items.Details;
           ItemToAdd.Cost = items.Cost;
           ItemToAdd.SurvRate = items.SurvRate;
           _ItemMapper.AddItem(ItemToAdd);
       }

       public void UpdateItem(Items items)
       {
           IItemDO ItemToUpdate = new ItemDO();
           ItemToUpdate.PKItemID = items.PKItemID;
           ItemToUpdate.ItemName = items.ItemName;
           ItemToUpdate.Details = items.Details;
           ItemToUpdate.Cost = items.Cost;
           ItemToUpdate.SurvRate = items.SurvRate;
           _ItemMapper.UpdateItem(ItemToUpdate);
       }

       public void DeleteItem(Items items)
       {
           IItemDO ItemToDelete = new ItemDO();
           ItemToDelete.PKItemID = items.PKItemID;
           _ItemMapper.DeleteItem(ItemToDelete);

       }



       public Items ReadItemByID(int PKItemID)
       {
           Items items = new Items();
           IItemDO itemDO = _ItemMapper.ReadItemByID(PKItemID);
           items.FKSupplierID = itemDO.FKSupplierID;
           items.PKItemID = itemDO.PKItemID;
           items.ItemName = itemDO.ItemName;
           items.Cost = itemDO.Cost;
           items.SurvRate = itemDO.SurvRate;
           return items;
       }

       public IItemDO ReadItemByItemName(int ItemName)
       {
           throw new NotImplementedException();
       }
    }
}
