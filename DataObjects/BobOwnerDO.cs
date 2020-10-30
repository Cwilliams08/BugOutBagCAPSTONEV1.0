using IDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DataObjects
{
    public class BobOwnerDO : IBobOwnerDO
    {
        public int PKUserID { get; set; }

        public int FKRoleID { get; set; }

        public string UserName { get; set; }

        public long PHONENUM { get; set; }

        public string PASSWORD { get; set; }

        public string SALT { get; set; }

        public string EncryptedPassword { get; set; }



   //[PKUserID] INT           IDENTITY (1, 1) NOT NULL,
   //[FKRoleID] INT           NOT NULL,
   //[UserName] VARCHAR (200) NOT NULL,
   //[PHONENUM] BIGINT        NOT NULL,
   //[PASSWORD] VARCHAR (500) NOT NULL,
   //[SALT]     VARCHAR (500) NOT NULL,





    }
    }
    

