using FASM_EN.User;
using System.Data;
using FASM_EN.SQLHelper;
using System;

namespace FASM_DAL.User
{
    public class RolesDAL
    {
        private string query = string.Empty;
        private KarazeSQLHelper data = new KarazeSQLHelper();

        public DataTable GetRoles()
        {
            query = "SELECT RoleId,RoleName FROM Roles";

            data.SetSqlStatement(query, CommandType.Text);

            return data.FillData();
        }

        public void LoadRoles(ref Roles eRoles)
        {
            query = "SELECT RoleId,RoleName,IsSysAdmin FROM Roles WHERE RoleId=@RoleId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@RoleId", SqlDbType.Int, eRoles.RoleId);

            DataTable dt = data.FillData();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                eRoles = new Roles()
                {
                    RoleId = Int32.Parse(dr["RoleId"].ToString()),
                    RoleName = dr["RoleName"].ToString(),
                    IsSysAdmin = (bool)dr["IsSysAdmin"]
                };
            }
        }


        public void InsertRoles(ref Roles eRoles)
        {
            query = "INSERT INTO Roles(RoleName,IsSysAdmin) VALUES(@RoleName,@IsSysAdmin); SELECT @RoleId = SCOPE_IDENTITY()";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@RoleId", SqlDbType.Int, eRoles.RoleId, ParameterDirection.Output);
            data.Parameter("@RoleName", SqlDbType.NVarChar, 50, eRoles.RoleName);
            data.Parameter("@IsSysAdmin", SqlDbType.Bit, eRoles.IsSysAdmin);

            data.ExecuteScalar();

            eRoles.RoleId = Int32.Parse(data.GetParamValue("@RoleId").ToString());
        }

        public void UpdateRoles(Roles eRoles)
        {
            query = "UPDATE Roles SET RoleName = @RoleName,IsSysAdmin = @IsSysAdmin WHERE RoleId= @RoleId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@RoleId", SqlDbType.Int, eRoles.RoleId);
            data.Parameter("@RoleName", SqlDbType.NVarChar, 50, eRoles.RoleName);
            data.Parameter("@IsSysAdmin", SqlDbType.Bit, eRoles.IsSysAdmin);

            data.ExecuteScalar();
        }

        public int DoesRolesExists(Int32 RoleId, string RoleName)
        {
            query = "SELECT COUNT(1) FROM Roles WHERE RoleId <> @RoleId AND RoleName = @RoleName";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@RoleId", SqlDbType.Int, RoleId);
            data.Parameter("@RoleName", SqlDbType.NVarChar, 50, RoleName);

            return (int)data.FillData().Rows[0][0];
        }

        public void DeleteRoles(Int32 RoleId)
        {
            query = "DELETE FROM Roles WHERE RoleId = @RoleId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@RoleId", SqlDbType.Int, RoleId);

            data.ExecuteScalar();
        }
    }
}
