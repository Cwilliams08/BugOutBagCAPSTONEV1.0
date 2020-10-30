using IDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DataObjects
{
    public class RoleDO : IRoleDO
    {
        public int PKRoleID { get; set; }

        public string ROLE { get; set; }


        }
    }

