using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessObjects
{
    public class Supplier
    {
        public int PKSupplierID { get; set; }

        public string VendorID { get; set; }

        public long PHONENUM { get; set; }

        public string PASSWORD { get; set; }

        public string SALT { get; set; }

        public Role UserRole { get; set; }

        public string EncryptedPassword { get; set; }

        public bool IsValid { get; set; }



        public Supplier()
        {
            //represent a not null value prevent null reference exception
            PASSWORD = string.Empty;
            SALT = string.Empty;
            IsValid = false;
            UserRole = new Role();
        }
    }
}
