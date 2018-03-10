using System.Data;
using FASM_BL.User;

namespace FASM_BI.User
{
    public class PermissionsBI
    {

        public static DataTable GetPermissions()
        {
            PermissionsBL PermissionsBL = new PermissionsBL();
            return PermissionsBL.GetPermissions();
        }
    }
}
