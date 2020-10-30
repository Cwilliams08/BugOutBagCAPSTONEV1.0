using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDataAccessLayer
{
    public interface IBagContentsMapper
    {
        List<IBagContentsDO> ReadAllBagContents();

        void DeleteContentsFromBugOutBag(IBagContentsDO DeleteContentsFromBugOutBag);

        void AddContentsToBugOutBag(IBagContentsDO AddContentsToBugOutBag);



        List<IBagContentsDO> GetItemPerContents(int PKItemID);


    }
}
