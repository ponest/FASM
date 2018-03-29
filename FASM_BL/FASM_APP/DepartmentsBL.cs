using FASM_DAL.Setups;
using FASM_EN.Setups;
using FASM_GN;
using System;
using System.Data;

namespace FASM_BL.Setups
{
    public class DepartmentsBL
    {
        private DepartmentsDAL DepartmentsDAL = new DepartmentsDAL();

        public DataTable GetDepartments()
        {
            return DepartmentsDAL.GetDepartments();
        }

        public void LoadDepartments(ref Departments eDepartments)
        {
            DepartmentsDAL.LoadDepartments(ref eDepartments);
        }

        public FASM_Enums.InfoMessages SaveDepartments(ref Departments eDepartments)
        {
            //Check if already exists
            if (DepartmentsDAL.DoesDepartmentsExists(eDepartments.DepartmentId, eDepartments.DepartmentName) > 0)
            {
                return FASM_Enums.InfoMessages.AlreadyExist;
            }
            if (eDepartments.DepartmentId == 0)
            {
                DepartmentsDAL.InsertDepartments(ref eDepartments);
            }
            else
            {
                DepartmentsDAL.UpdateDepartments(eDepartments);
            }
            return FASM_Enums.InfoMessages.Success;
        }

        public FASM_Enums.InfoMessages DeleteDepartments(Int32 DepartmentId)
        {
            //Check if its not in use before Deleting
            DepartmentsDAL.DeleteDepartments(DepartmentId);
            return FASM_Enums.InfoMessages.Success;
        }
    }
}