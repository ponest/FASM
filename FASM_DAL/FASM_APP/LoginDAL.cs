using System.Data;
using FASM_EN.SQLHelper;

namespace FASM_DAL.User
{
    public class LoginDAL
    {
        private string query = string.Empty;
        private KarazeSQLHelper data = new KarazeSQLHelper();

        //public void GetUsers(Login elogin)
        //{
        //    query = "SELECT * FROM users WHERE Username = @username AND Password = @pass";

        //    data.SetSqlStatement(query, CommandType.Text);

        //    data.Parameter("@username", SqlDbType.NVarChar,50, elogin.Username);
        //    data.Parameter("@pass", SqlDbType.NVarChar, 50,elogin.Password);


        //}

        public int GetUsers(string Username,string Password)
        {
            query = "SELECT COUNT(1) FROM users WHERE Username = @Username AND Password = @Password";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@Username", SqlDbType.NVarChar, 50,Username);
            data.Parameter("@Password", SqlDbType.NVarChar, 50,Password);

            return (int)data.FillData().Rows[0][0];
        }
    }
}
