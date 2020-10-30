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
    public class SupplierController : Controller
    {
        private string _ConnectionString = ConfigurationManager.ConnectionStrings["BugOutBagCAPSTONE"].ConnectionString;

        ISupplierBLO _SupplierBLO;
        IBobOwnerBLO _BobOwnerBLO;
        IItemBLO _ItemBLO;

        private string ConnectionString = "STUFF";

        public SupplierController()
        {
            MapperFactory Factory = new MapperFactory();
            IBobOwnerMapper MapperToPassToBLO = Factory.GenerateBobOwnerMapper(_ConnectionString);
            IRoleMapper RoleMapper = Factory.GenerateRoleMapper(_ConnectionString);
            IBagContentsMapper BagContentsMapperToPassToBLO = Factory.GenerateBagContentsMapper(_ConnectionString);
            IBugOutBagMapper BugOutBagMapperToPassToBLO = Factory.GenerateBugOutBagMapper(_ConnectionString);
            IItemMapper ItemMapperToPassToBLO = Factory.GenerateItemMapper(_ConnectionString);
            ISupplierMapper SupplierMapperToPassToBLO = Factory.GenerateSupplierMapper(_ConnectionString);

            _SupplierBLO = new SupplierBLO(SupplierMapperToPassToBLO);
        }

        public ActionResult SupplierLogin()
        {
            return View();
        }

        public ActionResult SupplierRegistration()
        {
            SupplierViewModel ViewModel = new SupplierViewModel();
            //calls BLo to get all roles to populate list
            ViewModel.GroupOfRoles = _SupplierBLO.ReadAllUserRoles();
            //converts list to selectlist
            ViewModel.UserRoleSelectList = ConvertToRoleSelectList(ViewModel.GroupOfRoles);
            //run if check for appropraite role
            //if not valid redirect to index
            //if (Session["Role"] != null && (Session["Role"] == "2"))
            return View(ViewModel);
            //else
            {
                // && prevents null from breaking the program
              //   return RedirectToAction("SupplierRegistration");
            }
            //return the proper view
        }

        [HttpPost]
        public ActionResult SupplierRegistration(SupplierViewModel iViewModel)
        {
            {
                //call add method on BLO and pass Supplier from view model
                _SupplierBLO.AddSupplier(iViewModel.SingleSupplier);
                //sends user back to index page
                return RedirectToAction("UniversalHomePage", "BobOwner");
            }
        }

        [HttpPost]
        public ActionResult SupplierLogin(SupplierViewModel iViewModel)
        {
            ActionResult result;
            iViewModel.SingleSupplier = _SupplierBLO.ValidateSupplier(iViewModel.SingleSupplier);
            if (iViewModel.SingleSupplier.IsValid)
            {
                Session["VendorID"] = iViewModel.SingleSupplier.VendorID;
                Session["Role"] = iViewModel.SingleSupplier.UserRole;

                result = RedirectToAction("InventoryIndex", "Items");
            }
            else
            {
                result = RedirectToAction("InvalidPass", "BobOwner");
            }
            return result;
        }

        public ActionResult AddSupplier()
        {
            SupplierViewModel ViewModel = new SupplierViewModel();
            //calls BLo to get all roles to populate list
            ViewModel.GroupOfRoles = _SupplierBLO.ReadAllUserRoles();
            //converts list to selectlist
            ViewModel.UserRoleSelectList = ConvertToRoleSelectList(ViewModel.GroupOfRoles);
            //run if check for appropraite role
            //if not valid redirect to index
            if (Session["Role"] != null && (Session["Role"] == "2"))
            return View(ViewModel);
            else
            {
                // && prevents null from breaking the program
                 return RedirectToAction("Index");
            }
            //return the proper view
        }

        private SelectList ConvertToRoleSelectList(List<Role> ilist)
        {
                        List<Role> Omitted = new List<Role>();
                        foreach (Role role in ilist)
                        {
                            if (role.PKRoleID == 2)
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
        public ActionResult AddSupplier(SupplierViewModel iViewModel)
        {
            //call add method on BLO and pass Supplier from view model
            _SupplierBLO.AddSupplier(iViewModel.SingleSupplier);
            //sends user back to index page
            return RedirectToAction("SupplierIndex", "Supplier");
        }

        private SelectList ConvertToSupplierSelectList(List<Supplier> ilist)
        {
            SelectList MySelectList = new SelectList(ilist, "PKSupplierID", "VendorID");
            return MySelectList;
        }

        public ActionResult DeleteSupplier()
        {
            //creates viewmodel to select user to delete
            SupplierViewModel ViewModel = new SupplierViewModel();
            //calls blo to get all users to popuyalte list
            ViewModel.GroupOfSuppliers = _SupplierBLO.ReadAllVendors();
            //conerts list to selectlist
            ViewModel.SupplierSelectList = ConvertToSupplierSelectList(ViewModel.GroupOfSuppliers);
            //run if check for appropraite role
            //if not valid redirect to index
            if (Session["Role"] != null && (Session["Role"] == "2"))
                return View(ViewModel);
            else
            {
                // && prevents null from breaking the program
                return RedirectToAction("Index");
            }
            //return the proper view
        }




        [HttpPost]
        public ActionResult DeleteSupplier(SupplierViewModel iViewModel)
        {
            _SupplierBLO.DeleteSupplier(iViewModel.SingleSupplier);
            return RedirectToAction("SupplierIndex", "Supplier");
        }

        public ActionResult UpdateSupplier()
        {
            SupplierViewModel ViewModel = new SupplierViewModel();
            //retrieve users from BLO
            ViewModel.GroupOfSuppliers = _SupplierBLO.ReadAllVendors();
            //converts list to select list
            ViewModel.SupplierSelectList = ConvertToSupplierSelectList(ViewModel.GroupOfSuppliers);
            //retrieve users from BLO
            ViewModel.GroupOfRoles = _SupplierBLO.ReadAllUserRoles();
            //converts list to select list
            ViewModel.UserRoleSelectList = ConvertToRoleSelectList(ViewModel.GroupOfRoles);
            //run if check for appropraite role
            //if not valid redirect to index
            if (Session["Role"] != null && (Session["Role"] == "2"))
                return View(ViewModel);
            else
            {
                // && prevents null from breaking the program
                return RedirectToAction("Index");
            }
            //return the proper view
        }

        [HttpPost]
        public ActionResult UpdateSupplier(SupplierViewModel iViewModel)
        {
            _SupplierBLO.UpdateSupplier(iViewModel.SingleSupplier);
            //return user to index
            return RedirectToAction("SupplierIndex", "Supplier");
        }



        public ActionResult SupplierIndex()
        {
            return View();
        }

    }
}

