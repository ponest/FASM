using FASM_EN.SQLHelper;
using FASM_EN.User;
using FASM_GN;
using System;
using System.Data;

namespace FASM_DAL.User
{
    public class MapRolePermissionDAL
    {
        private string query = string.Empty;
        private KarazeSQLHelper data = new KarazeSQLHelper();

        public DataTable GetMapRolePermission()
        {
            query = "SELECT RoleId,PermissionId FROM MapRolePermission";

            data.SetSqlStatement(query, CommandType.Text);

            return data.FillData();
        }

        public void LoadMapRolePermission(ref MapRolePermission eMapRolePermission)
        {
            query = "SELECT RoleId,PermissionId FROM MapRolePermission WHERE PermissionId=@PermissionId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@PermissionId", SqlDbType.Int, eMapRolePermission.PermissionId);

            DataTable dt = data.FillData();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                eMapRolePermission = new MapRolePermission()
                {
                    RoleId = Int32.Parse(dr["RoleId"].ToString()),
                    PermissionId = Int32.Parse(dr["PermissionId"].ToString())
                };
            }
        }

        public FASM_Enums.InfoMessages InsertMapRolePermission(DataTable dtMapRolePermission)
        {
            query = "DELETE MPR  FROM  MapRolePermission MPR INNER JOIN @dtMapRolePermission DMRP ON MPR.RoleId = DMRP.[Int1];";
            query += " INSERT INTO MapRolePermission(RoleId,PermissionId)";
            query += " SELECT [Int1],[Int2]FROM @dtMapRolePermission";


            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@dtMapRolePermission", dtMapRolePermission, "xParams");

            data.ExecuteNonQuery();

            return FASM_Enums.InfoMessages.Success;
        }

        public void UpdateMapRolePermission(MapRolePermission eMapRolePermission)
        {
            query = "UPDATE MapRolePermission SET RoleId = @RoleId, WHERE PermissionId= @PermissionId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@RoleId", SqlDbType.Int, eMapRolePermission.RoleId);
            data.Parameter("@PermissionId", SqlDbType.Int, eMapRolePermission.PermissionId);

            data.ExecuteScalar();
        }
        
        public void DeleteMapRolePermission(Int32 PermissionId)
        {
            query = "DELETE FROM MapRolePermission WHERE PermissionId = @PermissionId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@PermissionId", SqlDbType.Int, PermissionId);

            data.ExecuteScalar();
        }
    }
}
