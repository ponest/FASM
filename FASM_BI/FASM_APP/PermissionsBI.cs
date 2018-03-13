using System.Data;
using FASM_BL.User;
using System;

namespace FASM_BI.User
{
    public class PermissionsBI
    {

        public static DataTable GetPermissions(Int32 RoleId)
        {
            PermissionsBL PermissionsBL = new PermissionsBL();
            return PermissionsBL.GetPermissions(RoleId);
        }
    }
}
