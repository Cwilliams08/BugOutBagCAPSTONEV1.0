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
    public class BagContentsController : Controller
    {

        private string _ConnectionString = ConfigurationManager.ConnectionStrings["BugOutBagCAPSTONE"].ConnectionString;

        IBugOutBagBLO _BugOutBagBLO;
        IBobOwnerBLO _BobOwnerBLO;
        IItemBLO _ItemBLO;
        ISupplierBLO _SupplierBLO;
        IBagContentsBLO _BagContentsBLO;

        private string ConnectionString = "STUFF";

        public BagContentsController()
        {
            MapperFactory Factory = new MapperFactory();
            IBobOwnerMapper MapperToPassToBLO = Factory.GenerateBobOwnerMapper(_ConnectionString);
            IRoleMapper RoleMapper = Factory.GenerateRoleMapper(_ConnectionString);
            IBagContentsMapper BagContentsMapperToPassToBLO = Factory.GenerateBagContentsMapper(_ConnectionString);
            IBugOutBagMapper BugOutBagMapperToPassToBLO = Factory.GenerateBugOutBagMapper(_ConnectionString);
            IItemMapper ItemMapperToPassToBLO = Factory.GenerateItemMapper(_ConnectionString);
            ISupplierMapper SupplierMapperToPassToBLO = Factory.GenerateSupplierMapper(_ConnectionString);

            _BagContentsBLO = new BagContentsBLO(BagContentsMapperToPassToBLO, MapperToPassToBLO);
            _ItemBLO = new ItemBLO(ItemMapperToPassToBLO, BagContentsMapperToPassToBLO);
        }

        public ActionResult AddContentsToBugOutBag()
        {
            BugOutBagViewModel ViewModel = new BugOutBagViewModel();
            //calls BLo to get all items to populate list
            ViewModel.GroupOfItems = _ItemBLO.ReadAllItems();
            //converts list to select list
            ViewModel.ItemSelectList = ConvertToItemSelectList(ViewModel.GroupOfItems);
            //return view
            return View(ViewModel);
        }
        
        [HttpPost]
        public ActionResult AddContentsToBugOutBag(BugOutBagViewModel iBugOutBagViewModel)
        {
            //get bag id from session
            iBugOutBagViewModel.SingleBugOutBag.PKBugOutBagID = (int)Session["BugOutBag"];
            //call add method on BLO and pass items from view model
            _BagContentsBLO.AddContentsToBugOutBag(iBugOutBagViewModel.SingleItem
                                    , iBugOutBagViewModel.SingleBugOutBag
                                    , iBugOutBagViewModel.SingleBagContents);
            //sends user back to Bag contents listing page
            return RedirectToAction("ViewBugOutBag", "BugOutBag");
        }

       public ActionResult DeleteContentsFromBugOutBag()
       {
           //creates viewmodel to select item to remove from bag
           BugOutBagViewModel ViewModel = new BugOutBagViewModel();
           //retrieve session
           ViewModel.SingleBugOutBag.UserName = Session["UserName"].ToString();
           //calls BLO to get items to populate list
           ViewModel.GroupOfBagContents = _BagContentsBLO.ReadAllBagContents();
           //converts list to select list
           ViewModel.BagContentsSelectList = ConvertToBagContentsSelectList(ViewModel.GroupOfBagContents);
           //return the view
           return View(ViewModel);
       
       }
       
       [HttpPost]
       public ActionResult DeleteContentsFromBugOutBag(BugOutBagViewModel BugOutBagViewModel)
       {
           //call delete method on BLO and pass items from view model
           _BagContentsBLO.DeleteContentsFromBugOutBag(BugOutBagViewModel.SingleBagContents);
           //sends user back to bag contents listing
           return RedirectToAction("ViewBugOutBag", "BugOutBag");
       }

        private SelectList ConvertToBagContentsSelectList(List<BagContents> ilist)
        {
            SelectList MyContentsSelectList = new SelectList(ilist, "PKBagContentsID", "FKItemID");
            return MyContentsSelectList;
        }

        private SelectList ConvertToItemSelectList(List<Items> ilist)
        {
            SelectList MySelectList = new SelectList(ilist, "PKItemID", "ItemName");
            return MySelectList;
        }
    }
}
