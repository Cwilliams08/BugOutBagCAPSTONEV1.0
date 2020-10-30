using BugOutBagCAPSTONE.Models;
using BusinessLogicLayer;
using BusinessLogicLayer.BLO;
using BusinessLogicLayer.BusinessObjects;
using BusinessLogicLayer.IBLO;
using IDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugOutBagCAPSTONE.Controllers
{
    public class ItemsController : Controller
    {
        private string _ConnectionString = ConfigurationManager.ConnectionStrings["BugOutBagCAPSTONE"].ConnectionString;

        IBobOwnerBLO _BobOwnerBLO;
        IBugOutBagBLO _BugOutBagBLO;
        IItemBLO _ItemBLO;
        ISupplierBLO _SupplierBLO;

        private string ConnectionString = "STUFF";

        public ItemsController()
        {
            MapperFactory Factory = new MapperFactory();
            IBobOwnerMapper MapperToPassToBLO = Factory.GenerateBobOwnerMapper(_ConnectionString);
            IRoleMapper RoleMapper = Factory.GenerateRoleMapper(_ConnectionString);
            IBagContentsMapper BagContentsMapperToPassToBLO = Factory.GenerateBagContentsMapper(_ConnectionString);
            IBugOutBagMapper BugOutBagMapperToPassToBLO = Factory.GenerateBugOutBagMapper(_ConnectionString);
            IItemMapper ItemMapperToPassToBLO = Factory.GenerateItemMapper(_ConnectionString);
            ISupplierMapper SupplierMapperToPassToBLO = Factory.GenerateSupplierMapper(_ConnectionString);

            _ItemBLO = new ItemBLO(ItemMapperToPassToBLO, SupplierMapperToPassToBLO);
        }

        public ActionResult AddItem()
        {
            ItemViewModel ViewModel = new ItemViewModel();

            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult AddItem(ItemViewModel iViewModel)
        {
            //Add ID of current logged in Supplier
            iViewModel.SingleItem.VendorID = Session["VendorID"].ToString();
            //call add method on BLO and pass item from view model
            _ItemBLO.AddItem(iViewModel.SingleItem);
            //sends user back to supplier inventory page
            return RedirectToAction("InventoryIndex", "Items");
        }

        public ActionResult DeleteItem()
        {
            //creates viewmodel to select item to delete
            ItemViewModel ViewModel = new ItemViewModel();
            //calls BLO to get all users to populate list
            ViewModel.GroupOfItems = _ItemBLO.ReadAllItems();
            //converts list to selectlist
            ViewModel.ItemSelectList = ConvertToItemSelectList(ViewModel.GroupOfItems);
            //return the view
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult DeleteItem(ItemViewModel iViewModel)
        {
            _ItemBLO.DeleteItem(iViewModel.SingleItem);
            return RedirectToAction("InventoryIndex");
        }

        private SelectList ConvertToItemSelectList(List<Items> ilist)
        {
            SelectList MySelectList = new SelectList(ilist, "PKItemID", "ItemName");
            return MySelectList;
        }

        public ActionResult UpdateItems()
        {
            ItemViewModel ViewModel = new ItemViewModel();
            //retrieve items from BLO
            ViewModel.GroupOfItems = _ItemBLO.ReadAllItems();
            //converts list to select list
            ViewModel.ItemSelectList = ConvertToItemSelectList(ViewModel.GroupOfItems);
            //return the view
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult UpdateItems(ItemViewModel iViewModel)
        {
            _ItemBLO.UpdateItem(iViewModel.SingleItem);
            //return user to supplier inventory
            return RedirectToAction("InventoryIndex", "Items");
        }






        public ActionResult InventoryIndex()
        {
            return View();
        }

    }
}
