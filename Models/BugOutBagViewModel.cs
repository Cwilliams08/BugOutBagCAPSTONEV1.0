using BusinessLogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugOutBagCAPSTONE.Models
{
    public class BugOutBagViewModel : Controller
    {
        public BugOutBag SingleBugOutBag { get; set; }

        public List<BugOutBag> GroupOfBugOutBags { get; set; }

        public SelectList BugOutBagSelectList { get; set; }

        public BagContents SingleBagContents { get; set; }

        public List<BagContents> GroupOfBagContents { get; set; }

        public SelectList BagContentsSelectList { get; set; }

        public SelectList ItemSelectList { get; set; }

        public Items SingleItem { get; set; }

        public List<Items> GroupOfItems { get; set; }

                public BugOutBagViewModel()
        {
            SingleBugOutBag = new BugOutBag();
            GroupOfBugOutBags = new List<BugOutBag>();
            SingleBagContents = new BagContents();
            GroupOfBagContents = new List<BagContents>();
            SingleItem = new Items();
            GroupOfItems = new List<Items>();

        }

    }
}
