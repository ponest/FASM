using FASM_DAL.User;
using FASM_GN;


namespace FASM_BL.User
{
    public class LoginBL
    {
        private UsersDAL UsersDAL = new UsersDAL();
        public FASM_Enums.InfoMessages GetUsers(string Username, string Password)
        {
            LoginDAL loginDAL = new LoginDAL();
            if (loginDAL.GetUsers(Username, Password) > 0)
            {
                return FASM_Enums.InfoMessages.Success;
            }else
            {
                return FASM_Enums.InfoMessages.Failed;
            }
        }
    }
}
