using BusinessLogicLayer.BusinessObjects;
using IDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.IBLO
{
    public interface IBobOwnerBLO
    {

        List<BobOwner> ReadAllBobOwners();

        void AddBobOwner(BobOwner bobOwner);

        void DeleteBobOwner(BobOwner bobOwner);

        void SearchBobOwner(BobOwner bobowner);

        BobOwner ValidateBobOwner(BobOwner bobOwner);

        IBobOwnerDO ReadBobOwnerByUserID(int PKUserID);

        List<Role> ReadAllUserRoles();

        void UpdateBobOwner(BobOwner bobOwner);
    }
}
