using IDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DataObjects
{
    public class BugOutBagDO : IBugOutBagDO
    {
        public int PKBugOutBagID { get; set; }

        public decimal TotalCost { get; set; }

        public int TotalSurvRate { get; set; }

        public int FKUserID { get; set; }

        public string BagName { get; set; }

        }
    }

