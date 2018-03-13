using FASM_BL.User;
using FASM_EN.User;
using FASM_GN;
using System;
using System.Data;

namespace FASM_BI.User
{
    public class MapRolePermissionBI
    {
        public static DataTable GetMapRolePermission()
        {
            MapRolePermissionBL MapRolePermissionBL = new MapRolePermissionBL();
            return MapRolePermissionBL.GetMapRolePermission();
        }

        public static void LoadMapRolePermission(ref MapRolePermission eMapRolePermission)
        {
            MapRolePermissionBL MapRolePermissionBL = new MapRolePermissionBL();
            MapRolePermissionBL.LoadMapRolePermission(ref eMapRolePermission);
        }

        public static FASM_Enums.InfoMessages InsertMapRolePermission(DataTable dtMapRolePermission)
        {
            MapRolePermissionBL MapRolePermissionBL = new MapRolePermissionBL();
            return MapRolePermissionBL.InsertMapRolePermission(dtMapRolePermission);
        }

        public static FASM_Enums.InfoMessages DeleteMapRolePermission(Int32 PermissionId)
        {
            MapRolePermissionBL MapRolePermissionBL = new MapRolePermissionBL();
            return MapRolePermissionBL.DeleteMapRolePermission(PermissionId);
        }
    }
}
