using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDataAccessLayer
{
    public interface IItemMapper
    {
        List<IItemDO> ReadAllItems();
        void AddItem(IItemDO ItemToAdd);
        void DeleteItem(IItemDO ItemToDelete);
        void UpdateItem(IItemDO ItemToUpdate);

        IItemDO ReadItemByID(int PKItemID);
        IItemDO ReadItemByItemName(string ItemName);
    }
}
