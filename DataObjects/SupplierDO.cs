using IDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DataObjects
{
    public class SupplierDO : ISupplierDO
    {

        public int PKSupplierID { get; set; }

        public string VendorID { get; set; }

        public long PHONENUM { get; set; }

        public string PASSWORD { get; set; }

        public string SALT { get; set; }

        public int FKRoleID { get; set;}

        public string EncryptedPassword { get; set; }

    }
}
