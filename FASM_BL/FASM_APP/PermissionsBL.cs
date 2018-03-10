
using FASM_DAL.User;
using System.Data;

namespace FASM_BL.User
{
    public class PermissionsBL
    {
        private PermissionsDAL PermissionsDAL = new PermissionsDAL();

        public DataTable GetPermissions()
        {
            return PermissionsDAL.GetPermissions();
        }
    }
}
