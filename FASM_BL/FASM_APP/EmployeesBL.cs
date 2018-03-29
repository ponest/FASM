using FASM_DAL.Setups;
using FASM_EN.Setups;
using FASM_GN;
using System;
using System.Data;

namespace FASM_BL.Setups
{
    public class EmployeesBL
    {
        private EmployeesDAL EmployeesDAL = new EmployeesDAL();

        public DataTable GetEmployees()
        {
            return EmployeesDAL.GetEmployees();
        }

        public void LoadEmployees(ref Employees eEmployees)
        {
            EmployeesDAL.LoadEmployees(ref eEmployees);
        }

        public FASM_Enums.InfoMessages SaveEmployees(ref Employees eEmployees)
        {
            ////Check if already exists
            //if (EmployeesDAL.DoesEmployeesExists(eEmployees.EmployeeId, eEmployees.NameToCheck) > 0)
            //{
            //    return object;
            //}
            if (eEmployees.EmployeeId == 0)
            {
                EmployeesDAL.InsertEmployees(ref eEmployees);
            }
            else
            {
                EmployeesDAL.UpdateEmployees(eEmployees);
            }
            return FASM_Enums.InfoMessages.Success;
        }

        public FASM_Enums.InfoMessages DeleteEmployees(Int32 EmployeeId)
        {
            //Check if its not in use before Deleting
            EmployeesDAL.DeleteEmployees(EmployeeId);
            return FASM_Enums.InfoMessages.Success;
        }
    }
}