using FASM.Action_Filters;
using FASM.GeneralObjects;
using FASM_BI.User;
using FASM_EN.User;
using FASM_GN;
using System;
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
            ViewBag.AllowEdit = this.HasPermission(ControllerName.Accounts + "-PostEditRoles");
            ViewBag.AllowLoadEdit = this.HasPermission(ControllerName.Accounts + "-LoadEditRoles");
            ViewBag.AllowDelete = this.HasPermission(ControllerName.Accounts + "-DeleteRoles");
            ViewBag.AssignPermissions = this.HasPermission(ControllerName.Accounts + "-AssignPermissions");

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
                            message = FASM_Msg.AlreadyExist;
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
        public ActionResult LoadEditRoles(FASM_EN.User.Roles eRoles)
        {
            eRoles.RoleId = Convert.ToInt32(Request.Params["RoleId"]);
            RolesBI.LoadRoles(ref eRoles);
            return PartialView(eRoles);
        }

        [HttpPost]
        public ActionResult PostEditRoles(FASM_EN.User.Roles eRoles)
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
                            message = "Sorry! the district " + eRoles.RoleName + " " + FASM_Msg.AlreadyExist;
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

        #region Permissions
        public ActionResult Permission()
        {
            Permissions ePermissions = new Permissions();
            ePermissions.dtPermissions = PermissionsBI.GetPermissions();
            return View(ePermissions);
        }
        #endregion

        #region Users
        public ActionResult IndexUsers()
        {
            ViewBag.AllowAdd = this.HasPermission(ControllerName.Accounts + "-CreateUsers");
            ViewBag.AllowEdit = this.HasPermission(ControllerName.Accounts + "-EditUsers");
            ViewBag.AllowDelete = this.HasPermission(ControllerName.Accounts + "-DeleteUsers");
            Users eUsers = new Users();
            eUsers.dtUsers = UsersBI.GetUsers();
            return View(eUsers);
        }

        public ActionResult CreateUsers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUsers(Users eUsers)
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
            return View();
        }

        public ActionResult EditUsers()
        {
            Users eUsers = new Users();
            try
            {
                string IdVal = Url.RequestContext.RouteData.Values["Id"].ToString();

                if (General.IsNumeric(IdVal))
                {
                    eUsers.UserId = int.Parse(IdVal);
                }

                if (eUsers.UserId > 0)
                {
                    UsersBI.LoadUsers(ref eUsers);
                }
                if (eUsers.UserId == 0)
                    return RedirectToAction("IndexUsers", ControllerName.Accounts);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(eUsers);
        }

        [HttpPost]
        public ActionResult EditUsers(Users eUsers)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    FASM_Enums.InfoMessages SaveResult = UsersBI.SaveUsers(ref eUsers);
                    switch (SaveResult)
                    {
                        case FASM_Enums.InfoMessages.Success:
                            ModelState.AddModelError("Success", "Successfully Updated!");
                            ViewBag.ReturnMsg = "Success";
                            break;
                        case FASM_Enums.InfoMessages.AlreadyExist:
                            ModelState.AddModelError("Success", "The username already exist!");
                            ViewBag.ReturnMsg = "Failed";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
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
        [HttpPost]
        public ActionResult AssignPermissions()
        {
            Permissions ePermissions = new Permissions();
            ePermissions.dtPermissions = PermissionsBI.GetPermissions();
            ViewBag.RoleId = Convert.ToInt32(Request.Params["roleId"]);
            return View(ePermissions);
        }

       
        public ActionResult AssignPermissions(int m)
        {
            return View();
        }
        #endregion
    }
}