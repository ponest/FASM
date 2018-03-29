using FASM_EN.SQLHelper;
using FASM_EN.Setups;
using System;
using System.Data;

namespace FASM_DAL.Setups
{
    public class EmployeesDAL
    {
        private string query = string.Empty;
        private KarazeSQLHelper data = new KarazeSQLHelper();

        public DataTable GetEmployees()
        {
            query = "SELECT EmployeeId,FirstName,MiddleName,LastName,CASE Gender WHEN 'F' THEN 'Female' WHEN 'M' THEN 'Male' END Gender,";
            query += " PhoneNumber,Email FROM Employees";


            data.SetSqlStatement(query, CommandType.Text);

            return data.FillData();
        }

        public void LoadEmployees(ref Employees eEmployees)
        {
            query = "SELECT EmployeeId,FirstName,MiddleName,LastName,Gender,PhoneNumber,Email FROM Employees WHERE EmployeeId=@EmployeeId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@EmployeeId", SqlDbType.Int, eEmployees.EmployeeId);

            DataTable dt = data.FillData();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                eEmployees = new Employees()
                {
                    EmployeeId = Int32.Parse(dr["EmployeeId"].ToString()),
                    FirstName = dr["FirstName"].ToString(),
                    MiddleName = dr["MiddleName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    Gender = dr["Gender"].ToString(),
                    PhoneNumber = dr["PhoneNumber"].ToString(),
                    Email = dr["Email"].ToString()
                };
            }
        }

        public void InsertEmployees(ref Employees eEmployees)
        {
            query = "INSERT INTO Employees(FirstName,MiddleName,LastName,Gender,PhoneNumber,Email)"; 
            query+= " VALUES(@FirstName,@MiddleName,@LastName,@Gender,@PhoneNumber,@Email); SELECT @EmployeeId = SCOPE_IDENTITY()";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@EmployeeId", SqlDbType.Int, eEmployees.EmployeeId, ParameterDirection.Output);
            data.Parameter("@FirstName", SqlDbType.NVarChar, 30, eEmployees.FirstName);
            data.Parameter("@MiddleName", SqlDbType.NVarChar, 30, eEmployees.MiddleName);
            data.Parameter("@LastName", SqlDbType.NVarChar, 30, eEmployees.LastName);
            data.Parameter("@Gender", SqlDbType.Char, 1, eEmployees.Gender);
            data.Parameter("@PhoneNumber", SqlDbType.NVarChar, 20, eEmployees.PhoneNumber);
            data.Parameter("@Email", SqlDbType.NVarChar, 100, eEmployees.Email);

            data.ExecuteScalar();

            eEmployees.EmployeeId = Int32.Parse(data.GetParamValue("@EmployeeId").ToString());
        }

        public void UpdateEmployees(Employees eEmployees)
        {
            query = "UPDATE Employees SET FirstName = @FirstName,MiddleName = @MiddleName,LastName = @LastName,Gender = @Gender,";
            query+= " PhoneNumber = @PhoneNumber,Email = @Email WHERE EmployeeId= @EmployeeId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@EmployeeId", SqlDbType.Int, eEmployees.EmployeeId);
            data.Parameter("@FirstName", SqlDbType.NVarChar, 30, eEmployees.FirstName);
            data.Parameter("@MiddleName", SqlDbType.NVarChar, 30, eEmployees.MiddleName);
            data.Parameter("@LastName", SqlDbType.NVarChar, 30, eEmployees.LastName);
            data.Parameter("@Gender", SqlDbType.Char, 1, eEmployees.Gender);
            data.Parameter("@PhoneNumber", SqlDbType.NVarChar, 20, eEmployees.PhoneNumber);
            data.Parameter("@Email", SqlDbType.NVarChar, 100, eEmployees.Email);

            data.ExecuteScalar();
        }

        public void DeleteEmployees(Int32 EmployeeId)
        {
            query = "DELETE FROM Employees WHERE EmployeeId = @EmployeeId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@EmployeeId", SqlDbType.Int, EmployeeId);

            data.ExecuteScalar();
        }
    }
}
