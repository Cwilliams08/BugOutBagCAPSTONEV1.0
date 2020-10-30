using BusinessLogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.IBLO
{
    public interface IBagContentsBLO
    {
        List<BagContents> ReadAllBagContents();


        void AddContentsToBugOutBag(Items items, BugOutBag bugOutBag, BagContents bagContents);

        void DeleteContentsFromBugOutBag(BagContents bagContents);
    }
}
