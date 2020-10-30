using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessObjects
{
    public class BobOwner
    {
        public int PKUserID { get; set; }

        public int FKRoleID { get; set; }

        public string UserName { get; set; }
        
        public long PHONENUM { get; set; }
        
        public string PASSWORD { get; set; }
       
        public string SALT { get; set; }
        
        public string EncryptedPassword { get; set; }

        public bool IsValid { get; set; }

        public Role UserRole { get; set; }

        //[PKBobOwnerID] INT           IDENTITY (1, 1) NOT NULL,
        //[UserID]       VARCHAR (100) NOT NULL,
        //[RoleID]	     INT		   NOT NULL,
        //[PHONENUM]     BIGINT        NOT NULL,
        //[PASSWORD]     VARCHAR (500) NOT NULL,
        //[SALT]         VARCHAR (500) NOT NULL,

        public BobOwner()
        {
            //represent a not null value to prevent null reference exceptions
            PASSWORD = string.Empty;
            SALT = string.Empty;
            IsValid = false;
            UserRole = new Role();
        }
    }
}
