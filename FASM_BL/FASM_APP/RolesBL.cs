using FASM_DAL.User;
using FASM_EN.User;
using FASM_GN;
using System;
using System.Data;

namespace FASM_BL.User
{
    public class RolesBL
    {
        private RolesDAL RolesDAL = new RolesDAL();

        public DataTable GetRoles()
        {
            return RolesDAL.GetRoles();
        }

        public DataTable ShowRoles(Int32 UserId)
        {
            return RolesDAL.ShowRoles(UserId);
        }

        public void LoadRoles(ref Roles eRoles)
        {
            RolesDAL.LoadRoles(ref eRoles);
        }

        public FASM_Enums.InfoMessages SaveRoles(ref Roles eRoles)
        {
            //Check if already exists
            if (RolesDAL.DoesRolesExists(eRoles.RoleId, eRoles.RoleName) > 0)
            {
                return FASM_Enums.InfoMessages.AlreadyExist;
            }
            if (eRoles.RoleId == 0)
            {
                RolesDAL.InsertRoles(ref eRoles);
            }
            else
            {
                RolesDAL.UpdateRoles(eRoles);
            }
            return FASM_Enums.InfoMessages.Success;
        }

        public FASM_Enums.InfoMessages DeleteRoles(Int32 RoleId)
        {
            //Check if its not in use before Deleting
            RolesDAL.DeleteRoles(RoleId);
            return FASM_Enums.InfoMessages.Success;
        }
    }
}
