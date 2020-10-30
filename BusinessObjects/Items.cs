using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessObjects
{
    public class Items
    {

        public int PKItemID { get; set; }

        public int FKSupplierID { get; set; }

        public string ItemName { get; set; }

        public string Details { get; set; }

        public decimal Cost { get; set; }

        public int SurvRate { get; set; }

        public string VendorID { get; set; }
    }
}
