using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDataAccessLayer
{
    public interface IBobOwnerDO
    {
        int PKUserID { get; set; }
        
        int FKRoleID { get; set; }
        
        string UserName { get; set; }
        
        long PHONENUM { get; set; }
        
        string PASSWORD { get; set; }
        
        string SALT { get; set; }
        
        string EncryptedPassword { get; set; }



    //[PKBobOwnerID] INT           IDENTITY (1, 1) NOT NULL,
    //[UserID]       VARCHAR (100) NOT NULL,
	//[RoleID]	     INT		   NOT NULL,
    //[PHONENUM]     BIGINT        NOT NULL,
    //[PASSWORD]     VARCHAR (500) NOT NULL,
    //[SALT]         VARCHAR (500) NOT NULL,



    }
}
