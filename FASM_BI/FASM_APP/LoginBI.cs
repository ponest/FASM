using FASM_BL.User;
using FASM_GN;
namespace FASM_BI.User
{
    public class LoginBI
    {
        public static FASM_Enums.InfoMessages GetUsers(string Username,string Password)
        {
            LoginBL loginBL = new LoginBL();
            return loginBL.GetUsers(Username, Password);
        }
    }
}
