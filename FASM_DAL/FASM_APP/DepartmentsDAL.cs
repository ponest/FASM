using FASM_EN.SQLHelper;
using FASM_EN.Setups;
using System;
using System.Data;
namespace FASM_DAL.Setups
{
    public class DepartmentsDAL
    {
        private string query = string.Empty;
        private KarazeSQLHelper data = new KarazeSQLHelper();

        public DataTable GetDepartments()
        {
            query = "SELECT DepartmentId,DepartmentName FROM Departments";

            data.SetSqlStatement(query, CommandType.Text);

            return data.FillData();
        }

        public void LoadDepartments(ref Departments eDepartments)
        {
            query = "SELECT DepartmentId,DepartmentName FROM Departments WHERE DepartmentId=@DepartmentId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@DepartmentId", SqlDbType.Int, eDepartments.DepartmentId);

            DataTable dt = data.FillData();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                eDepartments = new Departments()
                {
                    DepartmentId = Int32.Parse(dr["DepartmentId"].ToString()),
                    DepartmentName = dr["DepartmentName"].ToString()
                };
            }
        }

        public void InsertDepartments(ref Departments eDepartments)
        {
            query = "INSERT INTO Departments(DepartmentName) VALUES(@DepartmentName); SELECT @DepartmentId = SCOPE_IDENTITY()";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@DepartmentId", SqlDbType.Int, eDepartments.DepartmentId, ParameterDirection.Output);
            data.Parameter("@DepartmentName", SqlDbType.NVarChar, 100, eDepartments.DepartmentName);

            data.ExecuteScalar();

            eDepartments.DepartmentId = Int32.Parse(data.GetParamValue("@DepartmentId").ToString());
        }

        public void UpdateDepartments(Departments eDepartments)
        {
            query = "UPDATE Departments SET DepartmentName = @DepartmentName WHERE DepartmentId= @DepartmentId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@DepartmentId", SqlDbType.Int, eDepartments.DepartmentId);
            data.Parameter("@DepartmentName", SqlDbType.NVarChar, 100, eDepartments.DepartmentName);

            data.ExecuteScalar();
        }

        public int DoesDepartmentsExists(Int32 DepartmentId, string DepartmentName)
        {
            query = "SELECT COUNT(1) FROM Departments WHERE DepartmentId <> @DepartmentId AND DepartmentName = @DepartmentName";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@DepartmentId", SqlDbType.Int, DepartmentId);
            data.Parameter("@DepartmentName", SqlDbType.NVarChar, 100, DepartmentName);

            return (int)data.FillData().Rows[0][0];
        }

        public void DeleteDepartments(Int32 DepartmentId)
        {
            query = "DELETE FROM Departments WHERE DepartmentId = @DepartmentId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@DepartmentId", SqlDbType.Int, DepartmentId);

            data.ExecuteScalar();
        }
    }
}