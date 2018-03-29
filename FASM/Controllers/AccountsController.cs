using FASM.Action_Filters;
using FASM.GeneralObjects;
using FASM_BI.User;
using FASM_EN.User;
using FASM_GN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace FASM.Controllers
{
    [FASM]
    public class AccountsController : Controller
    {

        // GET: Accounts
        public ActionResult Index()
        {
            ViewBag.Username = Session["user"];
            return View();
        }
        #region Login & Logout
        public string encryption(String password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Login eLogin)
        {
            eLogin.Password = encryption(eLogin.Password);
            if (ModelState.IsValid)
            {
                try
                {
                    FASM_Enums.InfoMessages Result = LoginBI.GetUsers(eLogin.Username, eLogin.Password);
                    switch (Result)
                    {
                        case FASM_Enums.InfoMessages.Success:
                            Session["user"] = eLogin.Username;
                            return RedirectToAction("Index", "Accounts");

                        case FASM_Enums.InfoMessages.Failed:
                            ModelState.AddModelError("fail", "Wrong Username or Password");
                            ViewBag.ReturnMsg = "Failed";
                            return View();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Accounts");
        }
        #endregion

        #region Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users eUsers)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (eUsers.Password == eUsers.ConfirmPassword)
                    {
                        eUsers.Password = encryption(eUsers.Password);
                        FASM_Enums.InfoMessages SaveResult = UsersBI.SaveUsers(ref eUsers);
                        switch (SaveResult)
                        {
                            case FASM_Enums.InfoMessages.Success:
                                ModelState.AddModelError("Success", "Successfully created!");
                                ViewBag.ReturnMsg = "Success";
                                break;
                            case FASM_Enums.InfoMessages.AlreadyExist:
                                ModelState.AddModelError("Success", "The username already exist!");
                                ViewBag.ReturnMsg = "Failed";
                                break;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Success", "Password does not match confirm password");
                        ViewBag.ReturnMsg = "Failed";
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(eUsers);
        }
        #endregion

        #region Roles
        public ActionResult IndexRoles()
        {
            ViewBag.AllowAdd = this.HasPermission(ControllerName.Accounts + "-CreateRoles");
            ViewBag.AllowEdit = this.HasPermission(ControllerName.Accounts + "-EditRoles");
            ViewBag.AllowDelete = this.HasPermission(ControllerName.Accounts + "-DeleteRoles");
            ViewBag.AssignPermissions = this.HasPermission(ControllerName.Accounts + "-ViewPermissions");

            FASM_EN.User.Roles eRoles = new FASM_EN.User.Roles();
            eRoles.dtRoles = RolesBI.GetRoles();
            return View(eRoles);
        }

        [HttpPost]
        public ActionResult CreateRoles(FASM_EN.User.Roles eRoles)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    FASM_Enums.InfoMessages SaveResult = RolesBI.SaveRoles(ref eRoles);
                    switch (SaveResult)
                    {
                        case FASM_Enums.InfoMessages.Success:
                            message = FASM_Msg.SuccessfulSaved;
                            break;
                        case FASM_Enums.InfoMessages.AlreadyExist:
                            message = "Sorry! the role name already exist";
                            break;
                    }
                    return Json(new { msg = message, JsonRequestBehavior.AllowGet });
                }
                catch (Exception ex)
                {
                    ViewBag.CatchedMsg = ex.Message;
                }
            }

            return View(eRoles);
        }

        [HttpPost]
        public ActionResult EditRoles(FASM_EN.User.Roles eRoles)
        {
            if(eRoles.isLoad == false)
            {
                eRoles.RoleId = Convert.ToInt32(Request.Params["RoleId"]);
                RolesBI.LoadRoles(ref eRoles);
                return PartialView(eRoles);
            }else
            {
                string message = "";
                if (ModelState.IsValid)
                {
                    try
                    {
                        FASM_Enums.InfoMessages Results = RolesBI.SaveRoles(ref eRoles);

                        switch (Results)
                        {
                            case FASM_Enums.InfoMessages.Success:
                                message = FASM_Msg.Updated;
                                break;
                            case FASM_Enums.InfoMessages.AlreadyExist:
                                message = "Sorry! the role name already exist";
                                break;
                        }
                        return Json(new { msg = message, JsonRequestBehavior.AllowGet });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.CatchedMsg = ex.Message;
                    }
                }
            }
            
            return View(eRoles);
        }

        [HttpPost]
        public ActionResult DeleteRoles(FASM_EN.User.Roles eRoles)
        {

            string message = "";
            try
            {
                if (eRoles.RoleId > 0)
                {
                    FASM_Enums.InfoMessages DeleteResult = RolesBI.DeleteRoles(eRoles.RoleId);
                    switch (DeleteResult)
                    {
                        case FASM_Enums.InfoMessages.Success:
                            message = "Successfully Deleted!";
                            break;
                        case FASM_Enums.InfoMessages.Failed:
                            message = "Still in Use!";
                            break;
                    }
                    return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            catch (Exception ex)
            {
                ViewBag.CatchedMsg = ex.Message;
            }
            return View();
        }
        #endregion
               
        #region Users
        public ActionResult IndexUsers()
        {
            ViewBag.AllowAdd = this.HasPermission(ControllerName.Accounts + "-CreateUsers");
            ViewBag.AllowEdit = this.HasPermission(ControllerName.Accounts + "-EditUsers");
            ViewBag.AllowDelete = this.HasPermission(ControllerName.Accounts + "-DeleteUsers");
            ViewBag.AssignRoles = this.HasPermission(ControllerName.Accounts + "-AssignRoles");
            Users eUsers = new Users();
            eUsers.dtUsers = UsersBI.GetUsers();
            return View(eUsers);
        }

        [HttpPost]
        public ActionResult CreateUsers(Users eUsers)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string message = "";
                    if (eUsers.Password == eUsers.ConfirmPassword)
                    {
                        eUsers.Password = encryption(eUsers.Password);
                        FASM_Enums.InfoMessages SaveResult = UsersBI.SaveUsers(ref eUsers);
                        switch (SaveResult)
                        {
                            case FASM_Enums.InfoMessages.Success:
                                message = "Successfully created!";
                                break;
                            case FASM_Enums.InfoMessages.AlreadyExist:
                                message = "The username already exist!";
                                break;
                        }
                    }
                    else
                    {
                        message = "Password does not match confirm password";
                    }
                    return Json(new { msg = message, JsonRequestBehavior.AllowGet });
                }
                catch (Exception ex)
                {
                    ViewBag.CatchedMsg = ex.Message;
                }
            }
            return View(eUsers);
        }

        [HttpPost]
        public ActionResult EditUsers(Users eUsers)
        {
            if (eUsers.isLoad == false)
            {
                eUsers.UserId = Convert.ToInt32(Request.Params["UserId"]);
                UsersBI.LoadUsers(ref eUsers);
                return PartialView(eUsers);
            }else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        string message = "";
                        FASM_Enums.InfoMessages SaveResult = UsersBI.SaveUsers(ref eUsers);
                        switch (SaveResult)
                        {
                            case FASM_Enums.InfoMessages.Success:
                                message = FASM_Msg.Updated;
                                break;
                            case FASM_Enums.InfoMessages.AlreadyExist:
                                message = "The username already exist!";
                                break;
                        }
                        return Json(new { msg = message, JsonRequestBehavior.AllowGet });
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult DeleteUsers(Users eUsers)
        {
            string message = string.Empty;
            try
            {
                if (eUsers.UserId > 0)
                {
                    FASM_Enums.InfoMessages DeleteResult = UsersBI.DeleteUsers(eUsers.UserId);
                    switch (DeleteResult)
                    {
                        case FASM_Enums.InfoMessages.Success:
                            message = "Successfully Deleted!";
                            break;
                        case FASM_Enums.InfoMessages.Failed:
                            message = "Still in Use!";
                            break;
                    }
                    return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }
        #endregion

        #region Assign Permissions
       
        public ActionResult ViewPermissions()
        {
            FASM_EN.User.Roles eRoles = new FASM_EN.User.Roles();
            int RoleId = Convert.ToInt32(Url.RequestContext.RouteData.Values["Id"]);
            ViewBag.RoleId = RoleId; eRoles.RoleId = RoleId;
            RolesBI.LoadRoles(ref eRoles);
            ViewBag.RoleName = eRoles.RoleName;
            Permissions ePermissions = new Permissions();
            ePermissions.dtPermissions = PermissionsBI.GetPermissions(RoleId);
            return View(ePermissions);
        }

        [HttpPost]
        public ActionResult AssignPermissions(IEnumerable<MapRolePermission> eMapRolePermissions)
        {
            if (eMapRolePermissions != null)
            {
                string message = "";
                try
                {
                    DataTable dt = ReturnPermissionsDetails(eMapRolePermissions);
                    FASM_Enums.InfoMessages Results = MapRolePermissionBI.InsertMapRolePermission(dt);

                    switch (Results)
                    {
                        case FASM_Enums.InfoMessages.Success:
                            message = FASM_Msg.SuccessfulSaved;
                            break;
                    }
                    return Json(new { msg = message, JsonRequestBehavior.AllowGet });
                }
                catch (Exception ex)
                {
                    ViewBag.CatchedMsg = ex.Message;
                }
            }
            else
            {
                string message = "You have not assigned any permission please choose one and try again";
                return Json(new { msg = message, JsonRequestBehavior.AllowGet });
            }
            return View(eMapRolePermissions);
        }

        private DataTable ReturnPermissionsDetails(IEnumerable<MapRolePermission> eMapRolePermissions)
        {
            DataTable dtPermission = General.GetdtParams();

            MapRolePermission eMapRolePermission = new MapRolePermission();
            if (eMapRolePermissions != null)
            {
                for (int i = 0; i < eMapRolePermissions.Count(); i++)
                {
                    eMapRolePermission = eMapRolePermissions.ElementAt(i);
                    DataRow dr = dtPermission.NewRow();
                    dr["Int1"] = eMapRolePermission.RoleId;
                    dr["Int2"] = eMapRolePermission.PermissionId;
                    dtPermission.Rows.Add(dr);
                }
            }
            return dtPermission;
        }
        #endregion

        #region Assign Roles
        public ActionResult ViewRoles()
        {
            Users eUsers = new Users();
            int UserId = Convert.ToInt32(Url.RequestContext.RouteData.Values["Id"]);
            ViewBag.UserId = UserId;eUsers.UserId = UserId;
            UsersBI.LoadUsers(ref eUsers);
            ViewBag.Username = eUsers.Username;
            FASM_EN.User.Roles eRoles = new FASM_EN.User.Roles();
            eRoles.dtRoles = RolesBI.ShowRoles(UserId);
            return View(eRoles);
        }


        public ActionResult AssignRoles(IEnumerable<MapUserRole> eMapUserRoles)
        {
            if (eMapUserRoles != null)
            {
                string message = "";
                try
                {
                    DataTable dt = ReturnRolesDetails(eMapUserRoles);
                    FASM_Enums.InfoMessages Results = MapUserRoleBI.InsertMapUserRole(dt);

                    switch (Results)
                    {
                        case FASM_Enums.InfoMessages.Success:
                            message = FASM_Msg.SuccessfulSaved;
                            break;
                    }
                    return Json(new { msg = message, JsonRequestBehavior.AllowGet });
                }
                catch (Exception ex)
                {
                    ViewBag.CatchedMsg = ex.Message;
                }
            }
            else
            {
                return RedirectToAction("ViewRoles");
            }
            return View(eMapUserRoles);
        }

        private DataTable ReturnRolesDetails(IEnumerable<MapUserRole> eMapUserRoles)
        {
            DataTable dtPermission = General.GetdtParams();

            MapUserRole eMapUserRole = new MapUserRole();
            if (eMapUserRoles != null)
            {
                for (int i = 0; i < eMapUserRoles.Count(); i++)
                {
                    eMapUserRole = eMapUserRoles.ElementAt(i);
                    DataRow dr = dtPermission.NewRow();
                    dr["Int1"] = eMapUserRole.UserId;
                    dr["Int2"] = eMapUserRole.RoleId;
                    dtPermission.Rows.Add(dr);
                }
            }
            return dtPermission;
        }
        #endregion



    }
}



