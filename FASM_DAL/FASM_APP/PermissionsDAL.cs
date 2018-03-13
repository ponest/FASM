using FASM_EN.SQLHelper;
using System;
using System.Data;

namespace FASM_DAL.User
{
    public class PermissionsDAL
    {
        private string query = string.Empty;
        private KarazeSQLHelper data = new KarazeSQLHelper();

        public DataTable GetPermissions(Int32 RoleId)
        {
            query = "SELECT P.PermissionId,PermissionDescription,[Description],ModName, CASE WHEN ext.PermissionId IS NULL THEN '' ELSE 'checked' END isChecked FROM Permissions P";
            query += " LEFT JOIN (SELECT MPR.RoleId,PermissionId FROM MapRolePermission MPR INNER JOIN Roles R ON R.RoleId = MPR.RoleId WHERE";
            query += " MPR.RoleId = @RoleId) ext ON ext.PermissionId =  P.PermissionId";
         
            data.SetSqlStatement(query, CommandType.Text);
            data.Parameter("@RoleId", SqlDbType.Int, RoleId);

            return data.FillData();
        }
    }
}
