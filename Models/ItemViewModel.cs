using BusinessLogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugOutBagCAPSTONE.Models
{
    public class ItemViewModel
    {

        public List<Items> GroupOfItems { get; set; }

        public Items SingleItem { get; set; }

        public SelectList ItemSelectList { get; set; }

        public ItemViewModel()
        {
            SingleItem = new Items();
            GroupOfItems = new List<Items>();
        }



    }
}
