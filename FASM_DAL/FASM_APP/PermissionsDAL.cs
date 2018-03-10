using FASM_EN.SQLHelper;
using System.Data;

namespace FASM_DAL.User
{
    public class PermissionsDAL
    {
        private string query = string.Empty;
        private KarazeSQLHelper data = new KarazeSQLHelper();

        public DataTable GetPermissions()
        {
            query = "SELECT PermissionId,PermissionDescription,ModName FROM Permissions";

            data.SetSqlStatement(query, CommandType.Text);

            return data.FillData();
        }
    }
}
