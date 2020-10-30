using BusinessLogicLayer.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.IBLO
{
   public interface ISupplierBLO
    {

       List<Supplier> ReadAllVendors();

       void AddSupplier(Supplier supplier);

       void UpdateSupplier(Supplier supplier);

       void DeleteSupplier(Supplier supplier);

       List<Role> ReadAllUserRoles(int PKRoleID);

       Supplier ValidateSupplier(Supplier supplier);

       List<Role> ReadAllUserRoles();
    }
}
