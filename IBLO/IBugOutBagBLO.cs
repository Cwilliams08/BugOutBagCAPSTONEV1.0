using BusinessLogicLayer.BusinessObjects;
using IDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.IBLO
{
    public interface IBugOutBagBLO
    {

        List<BugOutBag> ReadAllBugOutBags();

        void AddBugOutBag(BugOutBag bugoutbag);

        void DeleteBugOutBag(BugOutBag bugoutbag);

        BugOutBag ReadBugOutBagByID(int PKBugOutBagID);

        void UpdateBugOutBag(BugOutBag bugoutbag);

        void ViewBagList(BugOutBag bugOutBag);

        List<BagContents> ReadBagContentsByBagID(int iBagID);

        BugOutBag ViewBugOutBag(BugOutBag ibugOutBag);
    }
}
