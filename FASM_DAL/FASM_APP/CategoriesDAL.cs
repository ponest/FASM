using System.Data;
using FASM_EN.SQLHelper;
using System;
using FASM_EN.Setups;

namespace FASM_DAL.Setups
{
    public class CategoriesDAL
    {
        private string query = string.Empty;
        private KarazeSQLHelper data = new KarazeSQLHelper();

        public DataTable GetCategories()
        {
            query = "SELECT CategoryId,CategoryName FROM Categories ORDER BY CategoryId";

            data.SetSqlStatement(query, CommandType.Text);

            return data.FillData();
        }

        public void LoadCategories(ref Categories eCategories)
        {
            query = "SELECT CategoryId,CategoryName FROM Categories WHERE CategoryId=@CategoryId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@CategoryId", SqlDbType.Int, eCategories.CategoryId);

            DataTable dt = data.FillData();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                eCategories = new Categories()
                {
                    CategoryId = Int32.Parse(dr["CategoryId"].ToString()),
                    CategoryName = dr["CategoryName"].ToString()
                };
            }
        }

        //public void LoadCategories2(ref Categories eCategories)
        //{
        //    query = ";WITH Ids AS ( ";
        //    query += "SELECT CategoryId, CASE WHEN LAG(CategoryId,1, 0) OVER(ORDER BY CategoryId) = 0 THEN CategoryId ELSE LAG(CategoryId,1, 0) OVER(ORDER BY CategoryId) END PrevCategoryId, ";
        //    query += " CASE WHEN LEAD(CategoryId,1, 0) OVER(ORDER BY CategoryId) = 0 THEN CategoryId ELSE LEAD(CategoryId,1, 0) OVER(ORDER BY CategoryId) END NextCategoryId";
        //    query += " FROM Categories)";
        //    query += " SELECT tbl.*,PrevCategoryId,NextCategoryId FROM Ids INNER JOIN ";
        //    query += " (SELECT CategoryId,CategoryName FROM Categories WHERE CategoryId=@CategoryId) tbl";
        //    query += " ON tbl.CategoryId = Ids.CategoryId";

        //    data.SetSqlStatement(query, CommandType.Text);

        //    data.Parameter("@CategoryId", SqlDbType.Int, eCategories.CategoryId);

        //    DataTable dt = data.FillData();

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        DataRow dr = dt.Rows[0];
        //        eCategories = new Categories()
        //        {
        //            CategoryId = Int32.Parse(dr["CategoryId"]),
        //            CategoryName = dr["CategoryName"].ToString()
        //        };
        //    }
        //}

        public void InsertCategories(ref Categories eCategories)
        {
            query = "INSERT INTO Categories(CategoryName) VALUES(@CategoryName); SELECT @CategoryId = SCOPE_IDENTITY()";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@CategoryId", SqlDbType.Int, eCategories.CategoryId, ParameterDirection.Output);
            data.Parameter("@CategoryName", SqlDbType.NVarChar, 50, eCategories.CategoryName);

            data.ExecuteScalar();

            eCategories.CategoryId = Int32.Parse(data.GetParamValue("@CategoryId").ToString());
        }

        public void UpdateCategories(Categories eCategories)
        {
            query = "UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryId= @CategoryId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@CategoryId", SqlDbType.Int, eCategories.CategoryId);
            data.Parameter("@CategoryName", SqlDbType.NVarChar, 50, eCategories.CategoryName);

            data.ExecuteScalar();
        }

        public int DoesCategoriesExists(Int32 CategoryId, string CategoryName)
        {
            query = "SELECT COUNT(1) FROM Categories WHERE CategoryId <> @CategoryId AND CategoryName = @CategoryName";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@CategoryId", SqlDbType.Int, CategoryId);
            data.Parameter("@CategoryName", SqlDbType.NVarChar, 50, CategoryName);

            return (int)data.FillData().Rows[0][0];
        }

        public void DeleteCategories(Int32 CategoryId)
        {
            query = "DELETE FROM Categories WHERE CategoryId = @CategoryId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@CategoryId", SqlDbType.Int, CategoryId);

            data.ExecuteScalar();
        }
    }
}
