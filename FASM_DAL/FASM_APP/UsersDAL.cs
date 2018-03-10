using System;
using System.Data;
using FASM_EN.User;
using FASM_EN.SQLHelper;
namespace FASM_DAL.User
{
    public class UsersDAL
    {
        private string query = string.Empty;
        private KarazeSQLHelper data = new KarazeSQLHelper();

        public DataTable GetUsers()
        {
            query = "SELECT UserId,FirstName,LastName,Email,PhoneNumber,Username,Password FROM Users";

            data.SetSqlStatement(query, CommandType.Text);

            return data.FillData();
        }

        public void LoadUsers(ref Users eUsers)
        {
            query = "SELECT UserId,FirstName,LastName,Email,PhoneNumber,Username,Password FROM Users WHERE UserId=@UserId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@UserId", SqlDbType.BigInt, eUsers.UserId);

            DataTable dt = data.FillData();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                eUsers = new Users()
                {
                    UserId = Int64.Parse(dr["UserId"].ToString()),
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    Email = dr["Email"].ToString(),
                    PhoneNumber = dr["PhoneNumber"].ToString(),
                    Username = dr["Username"].ToString(),
                    Password = dr["Password"].ToString()
                };
            }
        }

        public void InsertUsers(ref Users eUsers)
        {
            query = "INSERT INTO Users(FirstName,LastName,Email,PhoneNumber,Username,Password)";
            query += " VALUES(@FirstName,@LastName,@Email,@PhoneNumber,@Username,@Password);";
            query += "SELECT @UserId = SCOPE_IDENTITY()";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@UserId", SqlDbType.BigInt, eUsers.UserId, ParameterDirection.Output);
            data.Parameter("@FirstName", SqlDbType.NVarChar, 50, eUsers.FirstName);
            data.Parameter("@LastName", SqlDbType.NVarChar, 50, eUsers.LastName);
            data.Parameter("@Email", SqlDbType.NVarChar, 250, eUsers.Email);
            data.Parameter("@PhoneNumber", SqlDbType.NVarChar, 50, eUsers.PhoneNumber);
            data.Parameter("@Username", SqlDbType.NVarChar, 50, eUsers.Username);
            data.Parameter("@Password", SqlDbType.NVarChar, 50, eUsers.Password);

            data.ExecuteScalar();

            eUsers.UserId = Int64.Parse(data.GetParamValue("@UserId").ToString());
        }

        public void UpdateUsers(Users eUsers)
        {
            query = "UPDATE Users SET FirstName = @FirstName,LastName = @LastName,Email = @Email,";
            query += " PhoneNumber = @PhoneNumber,Username = @Username,Password = @Password WHERE UserId= @UserId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@UserId", SqlDbType.BigInt, eUsers.UserId);
            data.Parameter("@FirstName", SqlDbType.NVarChar, 50, eUsers.FirstName);
            data.Parameter("@LastName", SqlDbType.NVarChar, 50, eUsers.LastName);
            data.Parameter("@Email", SqlDbType.NVarChar, 250, eUsers.Email);
            data.Parameter("@PhoneNumber", SqlDbType.NVarChar, 50, eUsers.PhoneNumber);
            data.Parameter("@Username", SqlDbType.NVarChar, 50, eUsers.Username);
            data.Parameter("@Password", SqlDbType.NVarChar, 50, eUsers.Password);

            data.ExecuteScalar();
        }

        public int DoesUsersExists(Int64 UserId, string Username)
        {
            query = "SELECT COUNT(1) FROM Users WHERE UserId <> @UserId AND Username = @Username";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@UserId", SqlDbType.BigInt, UserId);
            data.Parameter("@Username", SqlDbType.NVarChar, 50, Username);

            return (int)data.FillData().Rows[0][0];
        }

        public void DeleteUsers(Int64 UserId)
        {
            query = "DELETE FROM Users WHERE UserId = @UserId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@UserId", SqlDbType.BigInt, UserId);

            data.ExecuteScalar();
        }
    }
}
