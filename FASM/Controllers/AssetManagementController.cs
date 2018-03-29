using System;
using System.Web.Mvc;
using FASM_BI.AssetManagement;
using FASM_EN.AssetManagement;
using FASM.Action_Filters;
using FASM.GeneralObjects;
using System.Data;
using FASM_BI.Setups;
using FASM_GN;
using System.IO;
using System.Web;

namespace FASM.Controllers
{
    [FASM]
    public class AssetManagementController : Controller
    {

        #region Asset Definition
        public ActionResult IndexAssetDefinition()
        {
            ViewBag.AllowAdd = this.HasPermission(ControllerName.AssetManagement + "-CreateAssetDefinition");
            ViewBag.AllowEdit = this.HasPermission(ControllerName.AssetManagement + "-EditAssetDefinition");
            ViewBag.AllowDelete = this.HasPermission(ControllerName.AssetManagement + "-DeleteAssetDefinition");

            DataTable dtCategory = CategoriesBI.GetCategories();
            ViewData["CategoryName"] = General.DataTableToSelectList(dtCategory, "CategoryId", "CategoryName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtDepreciationMethods = General.GetDepreciationMethods();
            ViewData["DepreciationMethods"] = General.DataTableToSelectList(dtDepreciationMethods, "Value", "Text", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            AssetDefinition eAssetDefinition = new AssetDefinition();
            eAssetDefinition.dtAssetDefinition = AssetDefinitionBI.GetAssetDefinition();
            return View(eAssetDefinition);
        }

        [HttpPost]
        public ActionResult CreateAssetDefinition(AssetDefinition eAssetDefinition)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    DataTable dtCategory = CategoriesBI.GetCategories();
                    ViewData["CategoryName"] = General.DataTableToSelectList(dtCategory, "CategoryId", "CategoryName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

                    DataTable dtDepreciationMethods = General.GetDepreciationMethods();
                    ViewData["DepreciationMethods"] = General.DataTableToSelectList(dtDepreciationMethods, "Value", "Text", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

                    FASM_Enums.InfoMessages SaveResult = AssetDefinitionBI.SaveAssetDefinition(ref eAssetDefinition);
                    switch (SaveResult)
                    {
                        case FASM_Enums.InfoMessages.Success:
                            message = FASM_Msg.SuccessfulSaved;
                            break;
                        case FASM_Enums.InfoMessages.AlreadyExist:
                            message = "Asset Name already exist";
                            break;
                    }
                    return Json(new { msg = message, JsonRequestBehavior.AllowGet });
                }
                catch (Exception ex)
                {
                    ViewBag.CatchedMsg = ex.Message;
                }
            }

            return View(eAssetDefinition);
        }

        [HttpPost]
        public ActionResult EditAssetDefinition(AssetDefinition eAssetDefinition)
        {
            if (eAssetDefinition.isLoad == false)
            {
                DataTable dtCategory = CategoriesBI.GetCategories();
                ViewData["CategoryName"] = General.DataTableToSelectList(dtCategory, "CategoryId", "CategoryName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

                DataTable dtDepreciationMethods = General.GetDepreciationMethods();
                ViewData["DepreciationMethods"] = General.DataTableToSelectList(dtDepreciationMethods, "Value", "Text", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

                eAssetDefinition.AssetDefinitionId = Convert.ToInt32(Request.Params["AssetDefinitionId"]);
                AssetDefinitionBI.LoadAssetDefinition(ref eAssetDefinition);
                return PartialView(eAssetDefinition);
            }
            else
            {
                string message = "";
                if (ModelState.IsValid)
                {
                    try
                    {
                        FASM_Enums.InfoMessages Results = AssetDefinitionBI.SaveAssetDefinition(ref eAssetDefinition);

                        switch (Results)
                        {
                            case FASM_Enums.InfoMessages.Success:
                                message = FASM_Msg.Updated;
                                break;
                            case FASM_Enums.InfoMessages.AlreadyExist:
                                message = "Sorry! the Asset Name already exist";
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

            return View(eAssetDefinition);
        }

        [HttpPost]
        public ActionResult DeleteAssetDefinition(AssetDefinition eAssetDefinition)
        {
            string message = "";
            try
            {
                if (eAssetDefinition.AssetDefinitionId > 0)
                {
                    FASM_Enums.InfoMessages DeleteResult = AssetDefinitionBI.DeleteAssetDefinition(eAssetDefinition.AssetDefinitionId);
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

        #region Asset Registration
        public ActionResult IndexAssetRegister()
        {
            ViewBag.AllowAdd = this.HasPermission(ControllerName.AssetManagement + "-CreateAssetRegister");
            ViewBag.AllowEdit = this.HasPermission(ControllerName.AssetManagement + "-EditAssetRegister");
            ViewBag.AllowDelete = this.HasPermission(ControllerName.AssetManagement + "-DeleteAssetRegister");

            DataTable dtGetAssetName = AssetDefinitionBI.GetAssetDefinition();
            ViewData["AssetName"] = General.DataTableToSelectList(dtGetAssetName, "AssetDefinitionId", "AssetName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetSupplier = SuppliersBI.GetSuppliers();
            ViewData["SupplierName"] = General.DataTableToSelectList(dtGetSupplier, "SupplierId", "SupplierFullName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetLocation = LocationBI.GetLocation();
            ViewData["LocationName"] = General.DataTableToSelectList(dtGetLocation, "LocationId", "LocationName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetDepartment = DepartmentsBI.GetDepartments();
            ViewData["DepartmentName"] = General.DataTableToSelectList(dtGetDepartment, "DepartmentId", "DepartmentName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetCustodian = EmployeesBI.GetEmployees();
            ViewData["CustodianName"] = General.DataTableToSelectList(dtGetCustodian, "EmployeeId", "FirstName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });


            DataTable dtGetAssetConditions = General.GetAssetConditions();
            ViewData["AssetConditions"] = General.DataTableToSelectList(dtGetAssetConditions, "Value", "Text", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetAssetStatus = General.GetAssetStatus();
            ViewData["AssetStatuses"] = General.DataTableToSelectList(dtGetAssetStatus, "Value", "Text", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            ViewBag.PreloadDate = DateTime.Now.ToString("dd MMMM yyyy");

            AssetRegister eAssetRegister = new AssetRegister();
            eAssetRegister.dtAssetRegister = AssetRegisterBI.GetAssetRegister();
            return View(eAssetRegister);
        }

        public ActionResult CreateAssetRegister()
        {
            AssetRegister eAssetRegister = new AssetRegister();
            DataTable dtGetAssetName = AssetDefinitionBI.GetAssetDefinition();
            ViewData["AssetName"] = General.DataTableToSelectList(dtGetAssetName, "AssetDefinitionId", "AssetName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetSupplier = SuppliersBI.GetSuppliers();
            ViewData["SupplierName"] = General.DataTableToSelectList(dtGetSupplier, "SupplierId", "SupplierFullName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetLocation = LocationBI.GetLocation();
            ViewData["LocationName"] = General.DataTableToSelectList(dtGetLocation, "LocationId", "LocationName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetDepartment = DepartmentsBI.GetDepartments();
            ViewData["DepartmentName"] = General.DataTableToSelectList(dtGetDepartment, "DepartmentId", "DepartmentName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetCustodian = EmployeesBI.GetEmployees();
            ViewData["CustodianName"] = General.DataTableToSelectList(dtGetCustodian, "EmployeeId", "FirstName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });


            DataTable dtGetAssetConditions = General.GetAssetConditions();
            ViewData["AssetConditions"] = General.DataTableToSelectList(dtGetAssetConditions, "Value", "Text", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetAssetStatus = General.GetAssetStatus();
            ViewData["AssetStatuses"] = General.DataTableToSelectList(dtGetAssetStatus, "Value", "Text", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            ViewBag.PreloadDate = DateTime.Now.ToString("dd MMMM yyyy");
            return View(eAssetRegister);
        }

        [HttpPost]
        public ActionResult CreateAssetRegister(AssetRegister eAssetRegister)
        {
            DataTable dtGetAssetName = AssetDefinitionBI.GetAssetDefinition();
            ViewData["AssetName"] = General.DataTableToSelectList(dtGetAssetName, "AssetDefinitionId", "AssetName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetSupplier = SuppliersBI.GetSuppliers();
            ViewData["SupplierName"] = General.DataTableToSelectList(dtGetSupplier, "SupplierId", "SupplierFullName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetLocation = LocationBI.GetLocation();
            ViewData["LocationName"] = General.DataTableToSelectList(dtGetLocation, "LocationId", "LocationName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetDepartment = DepartmentsBI.GetDepartments();
            ViewData["DepartmentName"] = General.DataTableToSelectList(dtGetDepartment, "DepartmentId", "DepartmentName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetCustodian = EmployeesBI.GetEmployees();
            ViewData["CustodianName"] = General.DataTableToSelectList(dtGetCustodian, "EmployeeId", "FirstName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetAssetConditions = General.GetAssetConditions();
            ViewData["AssetConditions"] = General.DataTableToSelectList(dtGetAssetConditions, "Value", "Text", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetAssetStatus = General.GetAssetStatus();
            ViewData["AssetStatuses"] = General.DataTableToSelectList(dtGetAssetStatus, "Value", "Text", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            ViewBag.PreloadDate = DateTime.Now.ToString("dd MMMM yyyy");

            HttpPostedFileBase file = Request.Files["ImageFile"];
            if (file.FileName != "")
            {
                Random rmd = new Random();
                string FileName = Path.GetFileNameWithoutExtension(file.FileName);
                string Extension = Path.GetExtension(eAssetRegister.ImageFile.FileName);
                if (Extension == ".jpg" || Extension == ".png")
                {
                    FileName = FileName + rmd.Next(100) + Extension;
                    eAssetRegister.ImagePath = "~/images/" + FileName;
                    FileName = Path.Combine(Server.MapPath("/images/"), FileName);
                    file.SaveAs(FileName);
                }
                else
                {
                    ModelState.AddModelError("Success", "Unrequired format. Only .jpg and .png images are permitted");
                    ViewBag.ReturnMsg = "Failed";
                    return View(eAssetRegister);
                }

            }
            if (ModelState.IsValid)
            {
                try
                {
                    FASM_Enums.InfoMessages SaveResult = AssetRegisterBI.SaveAssetRegister(ref eAssetRegister);
                    switch (SaveResult)
                    {
                        case FASM_Enums.InfoMessages.Success:
                            ModelState.AddModelError("Success", FASM_Msg.SuccessfulSaved);
                            ViewBag.ReturnMsg = "Success";
                            break;
                        case FASM_Enums.InfoMessages.AlreadyExist:
                            ModelState.AddModelError("Success", "Asset Code already exist!");
                            ViewBag.ReturnMsg = "Failed";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(eAssetRegister);
        }

        public ActionResult EditAssetRegister()
        {
            AssetRegister eAssetRegister = new AssetRegister();

            DataTable dtGetAssetName = AssetDefinitionBI.GetAssetDefinition();
            ViewData["AssetName"] = General.DataTableToSelectList(dtGetAssetName, "AssetDefinitionId", "AssetName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetSupplier = SuppliersBI.GetSuppliers();
            ViewData["SupplierName"] = General.DataTableToSelectList(dtGetSupplier, "SupplierId", "SupplierFullName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetLocation = LocationBI.GetLocation();
            ViewData["LocationName"] = General.DataTableToSelectList(dtGetLocation, "LocationId", "LocationName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetDepartment = DepartmentsBI.GetDepartments();
            ViewData["DepartmentName"] = General.DataTableToSelectList(dtGetDepartment, "DepartmentId", "DepartmentName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetCustodian = EmployeesBI.GetEmployees();
            ViewData["CustodianName"] = General.DataTableToSelectList(dtGetCustodian, "EmployeeId", "FirstName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });


            DataTable dtGetAssetConditions = General.GetAssetConditions();
            ViewData["AssetConditions"] = General.DataTableToSelectList(dtGetAssetConditions, "Value", "Text", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtGetAssetStatus = General.GetAssetStatus();
            ViewData["AssetStatuses"] = General.DataTableToSelectList(dtGetAssetStatus, "Value", "Text", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            try
            {
                string IdVal = Url.RequestContext.RouteData.Values["Id"].ToString();
                if (General.IsNumeric(IdVal))
                {
                    eAssetRegister.AssetRegisterId = int.Parse(IdVal);
                }

                if (eAssetRegister.AssetRegisterId > 0)
                {
                    AssetRegisterBI.LoadAssetRegister(ref eAssetRegister);
                    ViewBag.PurchaseDate = eAssetRegister.PurchaseDate.ToString("dd/MMM/yyyy");
                    if (eAssetRegister.WarrantyExpiry != null)
                    {
                        ViewBag.WarantyExpiry = Convert.ToDateTime(eAssetRegister.WarrantyExpiry).ToString("dd/MMM/yyyy");
                    }
                    if (eAssetRegister.ImagePath != null)
                    {
                        ViewBag.ImagePath = eAssetRegister.ImagePath;
                    }
                }
                if (eAssetRegister.AssetRegisterId == 0)
                    return RedirectToAction("IndexAssetIdentity", ControllerName.AssetManagement);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(eAssetRegister);
        }

        [HttpPost]
        public ActionResult EditAssetRegister(AssetRegister eAssetRegister)
        {

            if (ModelState.IsValid)
            {
                DataTable dtGetAssetName = AssetDefinitionBI.GetAssetDefinition();
                ViewData["AssetName"] = General.DataTableToSelectList(dtGetAssetName, "AssetDefinitionId", "AssetName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

                DataTable dtGetSupplier = SuppliersBI.GetSuppliers();
                ViewData["SupplierName"] = General.DataTableToSelectList(dtGetSupplier, "SupplierId", "SupplierFullName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

                DataTable dtGetLocation = LocationBI.GetLocation();
                ViewData["LocationName"] = General.DataTableToSelectList(dtGetLocation, "LocationId", "LocationName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

                DataTable dtGetDepartment = DepartmentsBI.GetDepartments();
                ViewData["DepartmentName"] = General.DataTableToSelectList(dtGetDepartment, "DepartmentId", "DepartmentName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

                DataTable dtGetCustodian = EmployeesBI.GetEmployees();
                ViewData["CustodianName"] = General.DataTableToSelectList(dtGetCustodian, "EmployeeId", "FirstName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

                DataTable dtGetAssetConditions = General.GetAssetConditions();
                ViewData["AssetConditions"] = General.DataTableToSelectList(dtGetAssetConditions, "Value", "Text", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

                DataTable dtGetAssetStatus = General.GetAssetStatus();
                ViewData["AssetStatuses"] = General.DataTableToSelectList(dtGetAssetStatus, "Value", "Text", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

                ViewBag.PurchaseDate = eAssetRegister.PurchaseDate.ToString("dd/MMM/yyyy");
                if (eAssetRegister.WarrantyExpiry != null)
                {
                    ViewBag.WarantyExpiry = Convert.ToDateTime(eAssetRegister.WarrantyExpiry).ToString("dd/MMM/yyyy");
                }
                if (eAssetRegister.ImagePath != null)
                {
                    ViewBag.ImagePath = eAssetRegister.ImagePath;
                }
                try
                {
                    HttpPostedFileBase file = Request.Files["ImageFile"];
                    if (file.FileName != "")
                    {
                        Random rmd = new Random();
                        string FileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string Extension = Path.GetExtension(eAssetRegister.ImageFile.FileName);
                        if (Extension == ".jpg" || Extension == ".png")
                        {
                            FileName = FileName + rmd.Next(100) + Extension;
                            eAssetRegister.ImagePath = "~/images/" + FileName;

                            AssetRegister eNewAssetRegister = new AssetRegister();
                            eNewAssetRegister.AssetRegisterId = eAssetRegister.AssetRegisterId;
                            AssetRegisterBI.LoadAssetRegister(ref eNewAssetRegister);
                            string filepath = eNewAssetRegister.ImagePath;
                            //Deleting the old photo
                            if (filepath != "")
                            {
                                filepath = Server.MapPath(filepath);
                                if (System.IO.File.Exists(filepath)) { System.IO.File.Delete(filepath); }
                            }
                            //Saving the updated image
                            FileName = Path.Combine(Server.MapPath("/images/"), FileName);
                            file.SaveAs(FileName);
                        }
                        else
                        {
                            ModelState.AddModelError("Success", "Unrequired format. Only .jpg and .png images are permitted");
                            ViewBag.ReturnMsg = "Failed";
                            return View(eAssetRegister);
                        }
                    }else
                        {
                            AssetRegister eNewAssetRegister = new AssetRegister();
                            eNewAssetRegister.AssetRegisterId = eAssetRegister.AssetRegisterId;
                            AssetRegisterBI.LoadAssetRegister(ref eNewAssetRegister);
                            string filepath = eNewAssetRegister.ImagePath;
                            eAssetRegister.ImagePath = filepath;
                        }
                    FASM_Enums.InfoMessages Results = AssetRegisterBI.SaveAssetRegister(ref eAssetRegister);
                    switch (Results)
                    {
                        case FASM_Enums.InfoMessages.Success:
                            ModelState.AddModelError("Success", FASM_Msg.Updated);
                            ViewBag.ReturnMsg = "Success";
                            break;
                        case FASM_Enums.InfoMessages.AlreadyExist:
                            ModelState.AddModelError("Success", "Sorry! the Asset Code already exist");
                            ViewBag.ReturnMsg = "Failed";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(eAssetRegister);
        }

        [HttpPost]
        public ActionResult DeleteAssetRegister(AssetRegister eAssetRegister)
        {
            string message = "";
            try
            {
                if (eAssetRegister.AssetRegisterId > 0)
                {
                    AssetRegisterBI.LoadAssetRegister(ref eAssetRegister);

                    string filepath = eAssetRegister.ImagePath;

                    filepath = Server.MapPath(filepath);

                    if (System.IO.File.Exists(filepath))
                    {
                        System.IO.File.Delete(filepath);
                    }

                    FASM_Enums.InfoMessages DeleteResult = AssetRegisterBI.DeleteAssetRegister(eAssetRegister.AssetRegisterId);
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
    }
}