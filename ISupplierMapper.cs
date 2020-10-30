using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDataAccessLayer
{
    public interface ISupplierMapper
    {

        List<ISupplierDO> ReadAllVendors();

        void AddSupplier(ISupplierDO SupplierToAdd);
        void DeleteSupplier(ISupplierDO SupplierToDelete);
        void UpdateSupplier(ISupplierDO SupplierToUpdate);

       ISupplierDO ReadSupplierByVendorID(string VendorIDString);
       
       List<IRoleDO> ReadAllUserRoles();
       
       IRoleDO ReadRoleByID(int PKRoleID);

       int ReadSupplierID(string VendorID);
    }
}