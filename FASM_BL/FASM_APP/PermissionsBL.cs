
using FASM_DAL.User;
using System;
using System.Data;

namespace FASM_BL.User
{
    public class PermissionsBL
    {
        private PermissionsDAL PermissionsDAL = new PermissionsDAL();

        public DataTable GetPermissions(Int32 RoleId)
        {
            return PermissionsDAL.GetPermissions(RoleId);
        }
    }
}
