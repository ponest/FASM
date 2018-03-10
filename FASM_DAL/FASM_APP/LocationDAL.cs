
using FASM_EN.Setups;
using System;
using System.Data;
using FASM_EN.SQLHelper;

namespace FASM_DAL.Setups
{
    public class LocationDAL
    {
        private string query = string.Empty;
        private KarazeSQLHelper data = new KarazeSQLHelper();

        public DataTable GetLocation()
        {
            query = "SELECT LocationId,LocationName FROM Location";

            data.SetSqlStatement(query, CommandType.Text);

            return data.FillData();
        }

        public void LoadLocation(ref Location eLocation)
        {
            query = "SELECT LocationId,LocationName FROM Location WHERE LocationId=@LocationId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@LocationId", SqlDbType.Int, eLocation.LocationId);

            DataTable dt = data.FillData();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                eLocation = new Location()
                {
                    LocationId = Int32.Parse(dr["LocationId"].ToString()),
                    LocationName = dr["LocationName"].ToString()
                };
            }
        }

        //public void LoadLocation2(ref Location eLocation)
        //{
        //    query = ";WITH Ids AS ( ";
        //    query += "SELECT LocationId, CASE WHEN LAG(LocationId,1, 0) OVER(ORDER BY LocationId) = 0 THEN LocationId ELSE LAG(LocationId,1, 0) OVER(ORDER BY LocationId) END PrevLocationId, ";
        //    query += " CASE WHEN LEAD(LocationId,1, 0) OVER(ORDER BY LocationId) = 0 THEN LocationId ELSE LEAD(LocationId,1, 0) OVER(ORDER BY LocationId) END NextLocationId";
        //    query += " FROM Location)";
        //    query += " SELECT tbl.*,PrevLocationId,NextLocationId FROM Ids INNER JOIN ";
        //    query += " (SELECT LocationId,LocationName FROM Location WHERE LocationId=@LocationId) tbl";
        //    query += " ON tbl.LocationId = Ids.LocationId";

        //    data.SetSqlStatement(query, CommandType.Text);

        //    data.Parameter("@LocationId", SqlDbType.Int, eLocation.LocationId);

        //    DataTable dt = data.FillData();

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        DataRow dr = dt.Rows[0];
        //        eLocation = new Location()
        //        {
        //            LocationId = Int32.Parse(dr["LocationId"]),
        //            LocationName = dr["LocationName"].ToString()
        //        };
        //    }
        //}

        public void InsertLocation(ref Location eLocation)
        {
            query = "INSERT INTO Location(LocationName) VALUES(@LocationName); SELECT @LocationId = SCOPE_IDENTITY()";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@LocationId", SqlDbType.Int, eLocation.LocationId, ParameterDirection.Output);
            data.Parameter("@LocationName", SqlDbType.NVarChar, 100, eLocation.LocationName);

            data.ExecuteScalar();

            eLocation.LocationId = Int32.Parse(data.GetParamValue("@LocationId").ToString());
        }

        public void UpdateLocation(Location eLocation)
        {
            query = "UPDATE Location SET LocationName = @LocationName WHERE LocationId= @LocationId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@LocationId", SqlDbType.Int, eLocation.LocationId);
            data.Parameter("@LocationName", SqlDbType.NVarChar, 100, eLocation.LocationName);

            data.ExecuteScalar();
        }

        public int DoesLocationExists(Int32 LocationId, string LocationName)
        {
            query = "SELECT COUNT(1) FROM Location WHERE LocationId <> @LocationId AND LocationName = @LocationName";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@LocationId", SqlDbType.Int, LocationId);
            data.Parameter("@LocationName", SqlDbType.NVarChar, 100, LocationName);

            return (int)data.FillData().Rows[0][0];
        }

        public void DeleteLocation(Int32 LocationId)
        {
            query = "DELETE FROM Location WHERE LocationId = @LocationId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@LocationId", SqlDbType.Int, LocationId);

            data.ExecuteScalar();
        }
    }
}
