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
    public class BugOutBagController : Controller
    {

        private string _ConnectionString = ConfigurationManager.ConnectionStrings["BugOutBagCAPSTONE"].ConnectionString;

        IBugOutBagBLO _BugOutBagBLO;
        IBobOwnerBLO _BobOwnerBLO;
        IItemBLO _ItemBLO;
        ISupplierBLO _SupplierBLO;


        private string ConnectionString = "STUFF";

        public BugOutBagController()
        {
            MapperFactory Factory = new MapperFactory();
            IBobOwnerMapper MapperToPassToBLO = Factory.GenerateBobOwnerMapper(_ConnectionString);
            IRoleMapper RoleMapper = Factory.GenerateRoleMapper(_ConnectionString);
            IBagContentsMapper BagContentsMapperToPassToBLO = Factory.GenerateBagContentsMapper(_ConnectionString);
            IBugOutBagMapper BugOutBagMapperToPassToBLO = Factory.GenerateBugOutBagMapper(_ConnectionString);
            _ItemBLO = new ItemBLO(Factory.GenerateItemMapper(_ConnectionString));
            ISupplierMapper SupplierMapperToPassToBLO = Factory.GenerateSupplierMapper(_ConnectionString);

            _BugOutBagBLO = new BugOutBagBLO(BugOutBagMapperToPassToBLO, MapperToPassToBLO, BagContentsMapperToPassToBLO);
            
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddBugOutBag()
        {
            BugOutBagViewModel ViewModel = new BugOutBagViewModel();
            //calls BLO to get list of bag options
            ViewModel.GroupOfBugOutBags = _BugOutBagBLO.ReadAllBugOutBags();
            //converts list to select list
            ViewModel.BugOutBagSelectList = ConvertToBugOutBagSelectList(ViewModel.GroupOfBugOutBags);

            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult AddBugOutBag(BugOutBagViewModel iViewModel)
        {
            iViewModel.SingleBugOutBag.UserName = Session["UserName"].ToString();
            //calls add method on BLO and pass bugoutbag from view model
            _BugOutBagBLO.AddBugOutBag(iViewModel.SingleBugOutBag);
            //sends user back to index page
            return RedirectToAction("Index", "BobOwner");
        }


        private SelectList ConvertToBugOutBagSelectList(List<BugOutBag> ilist)
        {
            SelectList MyBugOutBagSelectList = new SelectList(ilist, "PKBugOutBagID", "BagName");
            return MyBugOutBagSelectList;
        }

        public ActionResult DeleteBugOutBag()
        {
            //creates viewmodel to select user to delete
            BugOutBagViewModel ViewModel = new BugOutBagViewModel();
            ViewModel.SingleBugOutBag.UserName = Session["UserName"].ToString();
            //calls blo to get all bags to popualte list
            ViewModel.GroupOfBugOutBags = _BugOutBagBLO.ReadAllBugOutBags();
            //converts list to selectlist
            ViewModel.BugOutBagSelectList = ConvertToBugOutBagSelectList(ViewModel.GroupOfBugOutBags);
            //return the view
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult DeleteBugOutBag(BugOutBagViewModel iViewModel)
        {
            _BugOutBagBLO.DeleteBugOutBag(iViewModel.SingleBugOutBag);
            return RedirectToAction("ViewBagList", "BugOutBag");
        }

        public ActionResult UpdateBugOutBag()
        {
            BugOutBagViewModel ViewModel = new BugOutBagViewModel();
            //retrieve bugoutbags from BLO
            ViewModel.SingleBugOutBag.UserName = Session["UserName"].ToString();
            ViewModel.GroupOfBugOutBags = _BugOutBagBLO.ReadAllBugOutBags();
            //converts list to select list
            ViewModel.BugOutBagSelectList = ConvertToBugOutBagSelectList(ViewModel.GroupOfBugOutBags);
            //return the view
            return View(ViewModel);

        }

        [HttpPost]
        public ActionResult UpdateBugOutBag(BugOutBagViewModel iViewModel)
        {
            _BugOutBagBLO.UpdateBugOutBag(iViewModel.SingleBugOutBag);
            //return user to index
            return RedirectToAction("ViewBagList", "BugOutBag");
        }

        public ActionResult ViewBugOutBag()
        {
            BugOutBagViewModel ViewModel = new BugOutBagViewModel();
            ViewModel.SingleBugOutBag.UserName = Session["UserName"].ToString();
            //ViewModel.SingleBugOutBag.FKUserID = (int)Session["FKUserID"];
            ViewModel.SingleBugOutBag.PKBugOutBagID = (int)Session["BugOutBag"];
            //ViewModel.SingleBugOutBag = _BugOutBagBLO.ReadBugOutBagByID((int)Session["BugOutBag"]);
            //ViewModel.SingleBagContents.PKBagContentsID = (int)Session["PKBagContentsID"];
            ViewModel.GroupOfBagContents = _BugOutBagBLO.ReadBagContentsByBagID(ViewModel.SingleBugOutBag.PKBugOutBagID);
           
            foreach (BagContents x in ViewModel.GroupOfBagContents)
            {
                Items temp = _ItemBLO.ReadItemByID(x.FKItemID);
                ViewModel.GroupOfItems.Add(temp);
            }
            //converts list to selectlist
            //ViewModel.BugOutBagSelectList = ConvertToBugOutBagSelectList(ViewModel.GroupOfBugOutBags);
            //return the view
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult ViewBugOutBag(BugOutBagViewModel iViewModel)
        {
            _BugOutBagBLO.ViewBugOutBag(iViewModel.SingleBugOutBag);
            //return user to list of bags
            return RedirectToAction("ViewBagList", "BugOutBag");
        }

        public ActionResult ViewBagList()
        {
            BugOutBagViewModel ViewModel = new BugOutBagViewModel();
            ViewModel.SingleBugOutBag.UserName = Session["UserName"].ToString();
            ViewModel.GroupOfBugOutBags = _BugOutBagBLO.ReadAllBugOutBags();
            //converts list to selectlist
            ViewModel.BugOutBagSelectList = ConvertToBugOutBagSelectList(ViewModel.GroupOfBugOutBags);
            //return the view
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult ViewBagList(BugOutBagViewModel iViewModel)
        {
            //Set Bugout Bag selected in Session
            Session["BugOutBag"] = iViewModel.SingleBugOutBag.PKBugOutBagID;
            _BugOutBagBLO.ViewBagList(iViewModel.SingleBugOutBag);
            //return user to view bug out bag view
            return RedirectToAction("ViewBugOutBag", "BugOutBag");
        }

    }
}
