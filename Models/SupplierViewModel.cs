using BusinessLogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugOutBagCAPSTONE.Models
{
    public class SupplierViewModel
    {
        public List<Supplier> GroupOfSuppliers { get; set; }

        public Supplier SingleSupplier { get; set; }

        public SelectList SupplierSelectList { get; set; }

        public SelectList UserRoleSelectList { get; set; }

        public SupplierViewModel()
        {
            SingleSupplier = new Supplier();
            GroupOfSuppliers = new List<Supplier>();
            SingleRole = new Role();
            GroupOfRoles = new List<Role>();
        }

        public Role SingleRole { get; set; }

        public List<Role> GroupOfRoles { get; set; }

    }
}
