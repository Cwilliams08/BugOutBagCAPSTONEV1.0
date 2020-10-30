using BusinessLogicLayer.BusinessObjects;
using IDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.IBLO
{
    public interface IItemBLO
    {
        List<Items> ReadAllItems();

        void AddItem(Items items);

        void DeleteItem(Items items);

        Items ReadItemByID(int PKItemID);

        IItemDO ReadItemByItemName(int ItemName);

        void UpdateItem(Items items);
    }
}
