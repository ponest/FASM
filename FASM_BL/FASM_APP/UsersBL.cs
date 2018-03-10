using System;
using System.Data;
using FASM_DAL.User;
using FASM_EN.User;
using FASM_GN;

namespace FASM_BL.User
{
    public class UsersBL
    {
        private UsersDAL UsersDAL = new UsersDAL();

        public DataTable GetUsers()
        {
            return UsersDAL.GetUsers();
        }

        public void LoadUsers(ref Users eUsers)
        {
            UsersDAL.LoadUsers(ref eUsers);
        }

        public FASM_Enums.InfoMessages SaveUsers(ref Users eUsers)
        {
            //Check if already exists
            if (UsersDAL.DoesUsersExists(eUsers.UserId, eUsers.Username) > 0)
            {
                return FASM_Enums.InfoMessages.AlreadyExist;
            }
            if (eUsers.UserId == 0)
            {
                UsersDAL.InsertUsers(ref eUsers);
            }
            else
            {
                UsersDAL.UpdateUsers(eUsers);
            }
            return FASM_Enums.InfoMessages.Success;
        }

        public FASM_Enums.InfoMessages DeleteUsers(Int64 UserId)
        {
            //Check if its not in use before Deleting
            UsersDAL.DeleteUsers(UserId);
            return FASM_Enums.InfoMessages.Success;
        }
    }
}
