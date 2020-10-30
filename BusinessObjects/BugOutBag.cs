using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessObjects
{
    public class BugOutBag
    {
        public int PKBugOutBagID { get; set; }

        public int FKUserID { get; set; }

        public string BagName { get; set; }

        public decimal TotalCost { get; set; }

        public int TotalSurvRate { get; set; }

        public List <BagContents> ContentsOfBag  { get; set; }

        public BugOutBag()
        {
            ContentsOfBag = new List<BagContents>();
        }

        public List<Items> BagItems { get; set; }

        public string UserName { get; set; }
    }
}
