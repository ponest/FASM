using FASM_EN.Setups;
using System;
using System.Data;
using FASM_BL.Setups;

namespace FASM_BI.Setups
{
    public class DepartmentsBI
    {

        public static DataTable GetDepartments()
        {
            DepartmentsBL DepartmentsBL = new DepartmentsBL();
            return DepartmentsBL.GetDepartments();
        }

        public static void LoadDepartments(ref Departments eDepartments)
        {
            DepartmentsBL DepartmentsBL = new DepartmentsBL();
            DepartmentsBL.LoadDepartments(ref eDepartments);
        }

        public static FASM_GN.FASM_Enums.InfoMessages SaveDepartments(ref Departments eDepartments)
        {
            DepartmentsBL DepartmentsBL = new DepartmentsBL();
            return DepartmentsBL.SaveDepartments(ref eDepartments);
        }

        public static FASM_GN.FASM_Enums.InfoMessages DeleteDepartments(Int32 DepartmentId)
        {
            DepartmentsBL DepartmentsBL = new DepartmentsBL();
            return DepartmentsBL.DeleteDepartments(DepartmentId);
        }
    }
}
