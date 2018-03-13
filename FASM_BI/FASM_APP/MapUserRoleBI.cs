using FASM_BL.User;
using FASM_GN;
using System.Data;

namespace FASM_BI.User
{
    public class MapUserRoleBI
    {
        public static FASM_Enums.InfoMessages InsertMapUserRole(DataTable dtMapUserRole)
        {
            MapUserRoleBL MapUserRoleBL = new MapUserRoleBL();
            return MapUserRoleBL.InsertMapUserRole(dtMapUserRole);
        }
    }
}
