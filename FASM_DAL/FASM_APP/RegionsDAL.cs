using FASM_EN.Setups;
using FASM_EN.SQLHelper;
using System;
using System.Data;

namespace FASM_DAL.Setups
{
    public class RegionsDAL
    {
        private string query = string.Empty;
        private KarazeSQLHelper data = new KarazeSQLHelper();

        public DataTable GetRegions()
        {
            query = "SELECT RegionId,RegionName FROM Regions";

            data.SetSqlStatement(query, CommandType.Text);

            return data.FillData();
        }

        public void LoadRegions(ref Regions eRegions)
        {
            query = "SELECT RegionId,RegionName FROM Regions WHERE RegionId=@RegionId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@RegionId", SqlDbType.Int, eRegions.RegionId);

            DataTable dt = data.FillData();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                eRegions = new Regions()
                {
                    RegionId = Int32.Parse(dr["RegionId"].ToString()),
                    RegionName = dr["RegionName"].ToString()
                };
            }
        }
        
        public void InsertRegions(ref Regions eRegions)
        {
            query = "INSERT INTO Regions(RegionName) VALUES(@RegionName); SELECT @RegionId = SCOPE_IDENTITY()";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@RegionId", SqlDbType.Int, eRegions.RegionId, ParameterDirection.Output);
            data.Parameter("@RegionName", SqlDbType.NVarChar, 100, eRegions.RegionName);

            data.ExecuteScalar();

            eRegions.RegionId = Int32.Parse(data.GetParamValue("@RegionId").ToString());
        }

        public void UpdateRegions(Regions eRegions)
        {
            query = "UPDATE Regions SET RegionName = @RegionName WHERE RegionId= @RegionId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@RegionId", SqlDbType.Int, eRegions.RegionId);
            data.Parameter("@RegionName", SqlDbType.NVarChar, 100, eRegions.RegionName);

            data.ExecuteScalar();
        }

        public int DoesRegionsExists(Int32 RegionId, string RegionName)
        {
            query = "SELECT COUNT(1) FROM Regions WHERE RegionId <> @RegionId AND RegionName = @RegionName";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@RegionId", SqlDbType.Int, RegionId);
            data.Parameter("@RegionName", SqlDbType.NVarChar, 100, RegionName);

            return (int)data.FillData().Rows[0][0];
        }

        public void DeleteRegions(Int32 RegionId)
        {
            query = "DELETE FROM Regions WHERE RegionId = @RegionId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@RegionId", SqlDbType.Int, RegionId);

            data.ExecuteScalar();
        }
    }
}
