using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDataAccessLayer
{
    public interface IRoleDO
    {
         int PKRoleID { get; set; }

         string ROLE { get; set; }
    }
}
