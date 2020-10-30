using IDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DataObjects
{
    public class BagContentsDO : IBagContentsDO
    {
        public int PKBagContentsID { get; set; }

        public int FKBugOutBagID { get; set; }

        public int FKItemID { get; set; }
    }
}
