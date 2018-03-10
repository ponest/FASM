using FASM_EN.Setups;
using FASM_EN.SQLHelper;
using System;
using System.Data;

namespace FASM_DAL.Setups
{
    public class DistrictsDAL
    {
        private string query = string.Empty;
        private KarazeSQLHelper data = new KarazeSQLHelper();

        public DataTable GetDistricts()
        {
            query = "SELECT DistrictId,DistrictName FROM Districts";

            data.SetSqlStatement(query, CommandType.Text);

            return data.FillData();
        }

        public void LoadDistricts(ref Districts eDistricts)
        {
            query = "SELECT DistrictId,DistrictName FROM Districts WHERE DistrictId=@DistrictId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@DistrictId", SqlDbType.Int, eDistricts.DistrictId);

            DataTable dt = data.FillData();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                eDistricts = new Districts()
                {
                    DistrictId = Int32.Parse(dr["DistrictId"].ToString()),
                    DistrictName = dr["DistrictName"].ToString()
                };
            }
        }

        //public void LoadDistricts2(ref Districts eDistricts)
        //{
        //    query = ";WITH Ids AS ( ";
        //    query += "SELECT DistrictId, CASE WHEN LAG(DistrictId,1, 0) OVER(ORDER BY DistrictId) = 0 THEN DistrictId ELSE LAG(DistrictId,1, 0) OVER(ORDER BY DistrictId) END PrevDistrictId, ";
        //    query += " CASE WHEN LEAD(DistrictId,1, 0) OVER(ORDER BY DistrictId) = 0 THEN DistrictId ELSE LEAD(DistrictId,1, 0) OVER(ORDER BY DistrictId) END NextDistrictId";
        //    query += " FROM Districts)";
        //    query += " SELECT tbl.*,PrevDistrictId,NextDistrictId FROM Ids INNER JOIN ";
        //    query += " (SELECT DistrictId,DistrictName FROM Districts WHERE DistrictId=@DistrictId) tbl";
        //    query += " ON tbl.DistrictId = Ids.DistrictId";

        //    data.SetSqlStatement(query, CommandType.Text);

        //    data.Parameter("@DistrictId", SqlDbType.Int, eDistricts.DistrictId);

        //    DataTable dt = data.FillData();

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        DataRow dr = dt.Rows[0];
        //        eDistricts = new Districts()
        //        {
        //            DistrictId = Int32.Parse(dr["DistrictId"]),
        //            DistrictName = dr["DistrictName"].ToString()
        //        };
        //    }
        //}

        public void InsertDistricts(ref Districts eDistricts)
        {
            query = "INSERT INTO Districts(DistrictName) VALUES(@DistrictName); SELECT @DistrictId = SCOPE_IDENTITY()";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@DistrictId", SqlDbType.Int, eDistricts.DistrictId, ParameterDirection.Output);
            data.Parameter("@DistrictName", SqlDbType.NVarChar, 50, eDistricts.DistrictName);

            data.ExecuteScalar();

            eDistricts.DistrictId = Int32.Parse(data.GetParamValue("@DistrictId").ToString());
        }

        public void UpdateDistricts(Districts eDistricts)
        {
            query = "UPDATE Districts SET DistrictName = @DistrictName WHERE DistrictId= @DistrictId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@DistrictId", SqlDbType.Int, eDistricts.DistrictId);
            data.Parameter("@DistrictName", SqlDbType.NVarChar, 100, eDistricts.DistrictName);

            data.ExecuteScalar();
        }

        public int DoesDistrictsExists(Int32 DistrictId, string DistrictName)
        {
            query = "SELECT COUNT(1) FROM Districts WHERE DistrictId <> @DistrictId AND DistrictName = @DistrictName";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@DistrictId", SqlDbType.Int, DistrictId);
            data.Parameter("@DistrictName", SqlDbType.NVarChar, 100, DistrictName);

            return (int)data.FillData().Rows[0][0];
        }

        public void DeleteDistricts(Int32 DistrictId)
        {
            query = "DELETE FROM Districts WHERE DistrictId = @DistrictId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@DistrictId", SqlDbType.Int, DistrictId);

            data.ExecuteScalar();
        }
    }
}
