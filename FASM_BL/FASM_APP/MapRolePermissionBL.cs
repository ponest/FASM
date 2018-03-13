using FASM_GN;
using FASM_DAL.User;
using FASM_EN.User;
using System;
using System.Data;

namespace FASM_BL.User
{
    public class MapRolePermissionBL
    {
        private MapRolePermissionDAL MapRolePermissionDAL = new MapRolePermissionDAL();

        public DataTable GetMapRolePermission()
        {
            return MapRolePermissionDAL.GetMapRolePermission();
        }

        public void LoadMapRolePermission(ref MapRolePermission eMapRolePermission)
        {
            MapRolePermissionDAL.LoadMapRolePermission(ref eMapRolePermission);
        }

        public FASM_Enums.InfoMessages InsertMapRolePermission(DataTable dtMapRolePermission)
        {
            return MapRolePermissionDAL.InsertMapRolePermission(dtMapRolePermission);
        }

        public FASM_Enums.InfoMessages DeleteMapRolePermission(Int32 PermissionId)
        {
            //Check if its not in use before Deleting
            MapRolePermissionDAL.DeleteMapRolePermission(PermissionId);
            return FASM_Enums.InfoMessages.Success;
        }
    }
}
