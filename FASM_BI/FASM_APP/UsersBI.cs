using System.Data;
using FASM_BL.User;
using FASM_EN.User;
using FASM_GN;
using System;

namespace FASM_BI.User
{
    public class UsersBI
    {

        public static DataTable GetUsers()
        {
            UsersBL UsersBL = new UsersBL();
            return UsersBL.GetUsers();
        }

        public static void LoadUsers(ref Users eUsers)
        {
            UsersBL UsersBL = new UsersBL();
            UsersBL.LoadUsers(ref eUsers);
        }

        public static FASM_Enums.InfoMessages SaveUsers(ref Users eUsers)
        {
            UsersBL UsersBL = new UsersBL();
            return UsersBL.SaveUsers(ref eUsers);
        }

        public static FASM_Enums.InfoMessages DeleteUsers(Int64 UserId)
        {
            UsersBL UsersBL = new UsersBL();
            return UsersBL.DeleteUsers(UserId);
        }
    }
}
