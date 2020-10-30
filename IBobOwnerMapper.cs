using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDataAccessLayer
{
    public interface IBobOwnerMapper
    {
        List<IBobOwnerDO> ReadAllBobOwners();
        void AddBobOwner(IBobOwnerDO BobOwnerToAdd);
        void DeleteBobOwner(IBobOwnerDO BobOwnerToDelete);
        void SearchBobOwner(IBobOwnerDO BobOwnerToSearch);
        void UpdateBobOwner(IBobOwnerDO BobownerToUpdate);
        void RegisterBobOwner(IBobOwnerDO BobOwnerToRegister);



        IBobOwnerDO ReadBobOwnerByUserName(string UserNameString);

        IBobOwnerDO ReadBobOwnerByUserID(int PKUserID);

        List<IRoleDO> ReadAllUserRoles();

        IRoleDO ReadRoleByID(int PKRoleID);

        int ReadUserID(string UserName);
    }
}
