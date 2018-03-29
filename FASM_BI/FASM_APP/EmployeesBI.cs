using System;
using System.Data;
using FASM_BL.Setups;
using FASM_EN.Setups;

namespace FASM_BI.Setups
{
    public class EmployeesBI
    {

        public static DataTable GetEmployees()
        {
            EmployeesBL EmployeesBL = new EmployeesBL();
            return EmployeesBL.GetEmployees();
        }

        public static void LoadEmployees(ref Employees eEmployees)
        {
            EmployeesBL EmployeesBL = new EmployeesBL();
            EmployeesBL.LoadEmployees(ref eEmployees);
        }

        public static FASM_GN.FASM_Enums.InfoMessages SaveEmployees(ref Employees eEmployees)
        {
            EmployeesBL EmployeesBL = new EmployeesBL();
            return EmployeesBL.SaveEmployees(ref eEmployees);
        }

        public static FASM_GN.FASM_Enums.InfoMessages DeleteEmployees(Int32 EmployeeId)
        {
            EmployeesBL EmployeesBL = new EmployeesBL();
            return EmployeesBL.DeleteEmployees(EmployeeId);
        }
    }
}
