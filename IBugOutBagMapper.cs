using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDataAccessLayer
{
    public interface IBugOutBagMapper
    {
        List<IBugOutBagDO> ReadAllBugOutBags();
        void AddBugOutBag(IBugOutBagDO BugOutBagToAdd);
        void DeleteBugOutBag(IBugOutBagDO BugOutBagToDelete);
        void UpdateBugOutBag(IBugOutBagDO BugOutBagToUpdate);

        IBugOutBagDO ReadBugOutBagByID(int PKBugOutBagID);
        IBugOutBagDO ViewBugOutBag(string BagName);


        void ViewBagList(IBugOutBagDO BugOutBagViewList);

        List<IBagContentsDO> ReadContentsByBagID(int PKBugOutBagID);
    }
}
