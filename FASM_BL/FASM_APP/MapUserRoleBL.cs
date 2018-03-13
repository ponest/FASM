using FASM_DAL.User;
using FASM_GN;
using System.Data;

namespace FASM_BL.User
{
    public class MapUserRoleBL
    {
        private MapUserRoleDAL MapUserRoleDAL = new MapUserRoleDAL();

        public FASM_Enums.InfoMessages InsertMapUserRole(DataTable dtMapUserRole)
        {
            return MapUserRoleDAL.InsertMapUserRole(dtMapUserRole);
        }

    }
}
