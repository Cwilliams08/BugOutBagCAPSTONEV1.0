using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessObjects
{
    public class BagContents
    {
        public int PKBagContentsID { get; set; }

        public int FKBugOutBagID { get; set; }

        public int Quantity { get; set; }

        public int FKItemID { get; set; }

        List<BagContents> _BagContents;

    }
}
