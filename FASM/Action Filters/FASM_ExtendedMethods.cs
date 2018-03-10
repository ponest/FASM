using System.Web;
using System.Web.Mvc;

namespace FASM.Action_Filters
{
    public static class FASM_ExtendedMethods
    {
        public static bool HasRole(this ControllerBase controller, string role)
        {
            bool bFound = false;
            try
            {
                //Check if the requesting user has the specified role...
                bFound = new FASMUser(HttpContext.Current.Session["user"].ToString()).HasRole(role);
            }
            catch { }
            return bFound;
        }

        public static bool HasRoles(this ControllerBase controller, string roles)
        {
            bool bFound = false;
            try
            {
                //Check if the requesting user has any of the specified roles...
                //Make sure you separate the roles using ; (ie "Sales Manager;Sales Operator"
                bFound = new FASMUser(HttpContext.Current.Session["user"].ToString()).HasRoles(roles);
            }
            catch { }
            return bFound;
        }

        public static bool HasPermission(this ControllerBase controller, string permission)
        {
            bool bFound = false;
            try
            {
                //Check if the requesting user has the specified application permission...
                bFound = new FASMUser(HttpContext.Current.Session["user"].ToString()).HasPermission(permission);
            }
            catch { }
            return bFound;
        }

        public static bool IsSysAdmin(this ControllerBase controller)
        {
            bool bIsSysAdmin = false;
            try
            {
                //Check if the requesting user has the System Administrator privilege...
                bIsSysAdmin = new FASMUser(HttpContext.Current.Session["user"].ToString()).IsSysAdmin;
            }
            catch { }
            return bIsSysAdmin;
        }
    }
}