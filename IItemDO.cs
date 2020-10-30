using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDataAccessLayer
{
    public interface IItemDO
    {
         int PKItemID { get; set; }

         int FKSupplierID { get; set; }

         string ItemName { get; set; }

         string Details { get; set; }

         decimal Cost { get; set; }

         int SurvRate { get; set; }
    }
}
