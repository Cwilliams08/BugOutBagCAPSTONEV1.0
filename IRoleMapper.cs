using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDataAccessLayer
{
    public interface IRoleMapper
    {
        List<IRoleDO> ReadAllRoles();

        IRoleDO ReadRoleByID(int RoleID);
    }
}
