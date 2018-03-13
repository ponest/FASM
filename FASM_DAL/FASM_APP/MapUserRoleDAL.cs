using FASM_EN.SQLHelper;
using FASM_GN;
using System.Data;

namespace FASM_DAL.User
{
    public class MapUserRoleDAL
    {
        private string query = string.Empty;
        private KarazeSQLHelper data = new KarazeSQLHelper();

        public FASM_Enums.InfoMessages InsertMapUserRole(DataTable dtMapUserRole)
        {
            query = "DELETE MUR  FROM  MapUserRole MUR INNER JOIN @dtMapUserRole DMUR ON MUR.UserId = DMUR.[Int1];";
            query += " INSERT INTO MapUserRole(UserId,RoleId)";
            query += " SELECT [Int1],[Int2]FROM @dtMapUserRole";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@dtMapUserRole", dtMapUserRole, "xParams");

            data.ExecuteNonQuery();

            return FASM_Enums.InfoMessages.Success;
        }
     
    }
}
