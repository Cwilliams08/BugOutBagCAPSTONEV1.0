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
    public class BobOwnerController : Controller
    {
        private string _ConnectionString = ConfigurationManager.ConnectionStrings["BugOutBagCAPSTONE"].ConnectionString;

        IBobOwnerBLO _BobOwnerBLO;
        IBugOutBagBLO _BugOutBagBLO;
        
        // GET: /BobOwner/
        private string ConnectionString = "STUFF";

        public BobOwnerController()
        {
            MapperFactory Factory = new MapperFactory();
            IBobOwnerMapper MapperToPassToBLO = Factory.GenerateBobOwnerMapper(_ConnectionString);
            IRoleMapper RoleMapper = Factory.GenerateRoleMapper(_ConnectionString);
            IBagContentsMapper BagContentsMapperToPassToBLO = Factory.GenerateBagContentsMapper(_ConnectionString);
            IBugOutBagMapper BugOutBagMapperToPassToBLO = Factory.GenerateBugOutBagMapper(_ConnectionString);
            IItemMapper ItemMapperToPassToBLO = Factory.GenerateItemMapper(_ConnectionString);
            ISupplierMapper SupplierMapperToPassToBLO = Factory.GenerateSupplierMapper(_ConnectionString);

            _BobOwnerBLO = new BobOwnerBLO(MapperToPassToBLO);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult UniversalHomePage()
        {
            return View();

        }


        [HttpPost]
        public ActionResult Login(BobOwnerViewModel iViewModel)
        {
            ActionResult result;
            iViewModel.SingleBobOwner = _BobOwnerBLO.ValidateBobOwner(iViewModel.SingleBobOwner);
            if (iViewModel.SingleBobOwner.IsValid)
            {
                Session["UserName"] = iViewModel.SingleBobOwner.UserName;
                //RoleName will not work until roles are set up
                Session["Role"] = iViewModel.SingleBobOwner.UserRole.PKRoleID;

                result = RedirectToAction("Index", "BobOwner");
            }
            else
            {
                result = RedirectToAction("InvalidPass", "BobOwner");
            }
            return result;
        }

        public ActionResult RegisterUser()
        {
            BobOwnerViewModel ViewModel = new BobOwnerViewModel();
            //calls BLO to get all roles to populate list
            ViewModel.GroupOfRoles = _BobOwnerBLO.ReadAllUserRoles();
            //converts list to select list
            ViewModel.UserRoleSelectList = ConvertToRoleSelectList(ViewModel.GroupOfRoles);
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult RegisterUser(BobOwnerViewModel iViewModel)
        {
            //call add method on BLO and pass Bobowner from view model
            _BobOwnerBLO.AddBobOwner(iViewModel.SingleBobOwner);
            //sends user back to index page
            return RedirectToAction("Index", "BobOwner");
        }

        public ActionResult AddUser()
        {

            BobOwnerViewModel ViewModel = new BobOwnerViewModel();
            //calls BLO to get all roles to populate list
            ViewModel.GroupOfRoles = _BobOwnerBLO.ReadAllUserRoles();
            //converts list to selectlist
            ViewModel.UserRoleSelectList = ConvertToRoleSelectList(ViewModel.GroupOfRoles);
            //run if check for appropriate role
            //if not valid redirect to index
            if (Session["Role"] != null && ((int)Session["Role"] == 3))
                return View(ViewModel);
            else
            {
                // && prevents null from breaking program
                return View("InvalidPass");
            }
            //return the proper view
        }

        private SelectList ConvertToRoleSelectList(List<Role> ilist)
        {
            List<Role> Omitted = new List<Role>();
            foreach (Role role in ilist)
            {
                if (role.PKRoleID != 2)
                {
                    Omitted.Add(new Role()
                        {
                            PKRoleID = role.PKRoleID,
                            ROLE = role.ROLE
                        });
                }
                
            }
            
            SelectList MyRoleSelectList = new SelectList(Omitted, "PKRoleID", "ROLE");
            return MyRoleSelectList;
        }
                /// <summary>
        /// Method that is called when submit button on add BobOwner view
        /// </summary>
        /// <param name="iViewModel"></param>
        /// <returns></returns>     
        /// 
        [HttpPost]
        public ActionResult AddUser(BobOwnerViewModel iViewModel)
        {
            //call add method on BLO and pass Bobowner from view model
            _BobOwnerBLO.AddBobOwner(iViewModel.SingleBobOwner);
            //sends user back to index page
            return RedirectToAction("Index", "BobOwner");
        }

        public ActionResult DeleteUser()
        {
            //creates viewmodel to select user to delete
            BobOwnerViewModel ViewModel = new BobOwnerViewModel();
            //valls BLO to get all users to populate list
            ViewModel.GroupOfBobOwners = _BobOwnerBLO.ReadAllBobOwners();
            //converts list to selectlist
            ViewModel.BobOwnerSelectList = ConvertToUserSelectList(ViewModel.GroupOfBobOwners);
            //run if check for appropriate role
            //if not valid redirect to index
            if (Session["Role"] != null && (Session["Role"] == "3"))
                return View(ViewModel);
            else
            {
                // && prevents null from breaking program
                return RedirectToAction("InvalidPass", "BobOwner");
            }
            //return the proper view
        }

        

        private SelectList ConvertToUserSelectList(List<BobOwner> ilist)
        {
            SelectList MySelectList = new SelectList(ilist, "PKUserID", "UserName");
            return MySelectList;
        }

        [HttpPost]
        public ActionResult DeleteUser(BobOwnerViewModel iViewModel)
        {
            _BobOwnerBLO.DeleteBobOwner(iViewModel.SingleBobOwner);
            return RedirectToAction("Index");
        }

        public ActionResult UpdateUser()
        {
            BobOwnerViewModel ViewModel = new BobOwnerViewModel();
            //retrieve users from BLO
            ViewModel.GroupOfBobOwners = _BobOwnerBLO.ReadAllBobOwners();
            //converts list to select list
            ViewModel.BobOwnerSelectList = ConvertToUserSelectList(ViewModel.GroupOfBobOwners);
            //retrieve roles from BLO
            ViewModel.GroupOfRoles = _BobOwnerBLO.ReadAllUserRoles();
            //converts list to select list
            ViewModel.UserRoleSelectList = ConvertToRoleSelectList(ViewModel.GroupOfRoles);
            //run if check for appropriate role
            //if not valid redirect to index
            if (Session["Role"] != null && (Session["Role"] == "3"))
                return View(ViewModel);
            else
            {
                // && prevents null from breaking program
                return RedirectToAction("InvalidPass", "BobOwner");
            }
            //return the proper view
        
        }

        [HttpPost]
        public ActionResult UpdateUser(BobOwnerViewModel iViewModel)
        {
            _BobOwnerBLO.UpdateBobOwner(iViewModel.SingleBobOwner);
            //run if check for appropriate role
            //if not valid redirect to index
            if (Session["Role"] != null && (Session["Role"] == "3"))
                return View(iViewModel);
            else
            {
                // && prevents null from breaking program
                return RedirectToAction("InvalidPass", "BobOwner");
            }
            //return the proper view
        
        }
            
        

        public ActionResult Index()
        {
            return View();
        }

    }
}
