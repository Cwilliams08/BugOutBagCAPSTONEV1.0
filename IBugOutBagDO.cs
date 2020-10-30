using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDataAccessLayer
{
    public interface IBugOutBagDO
    {
         int PKBugOutBagID { get; set; }
        
         decimal TotalCost { get; set; }
        
         int TotalSurvRate { get; set; }
        
         int FKUserID { get; set; }
        
         string BagName { get; set; }
    }
}
