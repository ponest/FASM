using FASM_EN.User;
using System;
using System.Data;
using FASM_BL.User;
using FASM_GN;

namespace FASM_BI.User
{
    public class RolesBI
    {

        public static DataTable GetRoles()
        {
            RolesBL RolesBL = new RolesBL();
            return RolesBL.GetRoles();
        }

        public static void LoadRoles(ref Roles eRoles)
        {
            RolesBL RolesBL = new RolesBL();
            RolesBL.LoadRoles(ref eRoles);
        }

        public static FASM_Enums.InfoMessages SaveRoles(ref Roles eRoles)
        {
            RolesBL RolesBL = new RolesBL();
            return RolesBL.SaveRoles(ref eRoles);
        }

        public static FASM_Enums.InfoMessages DeleteRoles(Int32 RoleId)
        {
            RolesBL RolesBL = new RolesBL();
            return RolesBL.DeleteRoles(RoleId);
        }
    }
}
