using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDataAccessLayer
{
    public interface ISupplierDO
    {
        string VendorID { get; set; }

        long PHONENUM { get; set; }

        string PASSWORD { get; set; }

        string SALT { get; set; }

        int PKSupplierID { get; set; }

        int FKRoleID { get; set; }

        string EncryptedPassword { get; set; }
    }
}
