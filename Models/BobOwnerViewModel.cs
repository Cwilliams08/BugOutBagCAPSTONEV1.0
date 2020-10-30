using BusinessLogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugOutBagCAPSTONE.Models
{
    public class BobOwnerViewModel
    {
        public List<BobOwner> GroupOfBobOwners { get; set; }

        public BobOwner SingleBobOwner { get; set; }

        public SelectList BobOwnerSelectList { get; set; }

        public SelectList UserRoleSelectList { get; set; }

        public BobOwnerViewModel()
        {
            SingleBobOwner = new BobOwner();
            GroupOfBobOwners = new List<BobOwner>();
            SingleRole = new Role();
            GroupOfRoles = new List<Role>();
        }




        public Role SingleRole { get; set; }

        public List<Role> GroupOfRoles { get; set; }
    }
}
