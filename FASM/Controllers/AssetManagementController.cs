using System;
using System.Web.Mvc;
using FASM_BI.AssetManagement;
using FASM_EN.AssetManagement;
using FASM.Action_Filters;
using FASM.GeneralObjects;
using System.Data;
using FASM_BI.Setups;
using FASM_GN;

namespace FASM.Controllers
{
    [FASM]
    public class AssetManagementController : Controller
    {
        // GET: AssetManagement
        public ActionResult IndexAssetDefinition()
        {
            ViewBag.AllowAdd = this.HasPermission(ControllerName.AssetManagement + "-CreateAssetDefinition");
            ViewBag.AllowEdit = this.HasPermission(ControllerName.AssetManagement + "-PostEditAssetDefinition");
            ViewBag.AllowLoadEdit = this.HasPermission(ControllerName.AssetManagement + "-LoadEditAssetDefinition");
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

            return View(eAssetDefinition);
        }

        [HttpPost]
        public ActionResult LoadEditAssetDefinition(AssetDefinition eAssetDefinition)
        {
            DataTable dtCategory = CategoriesBI.GetCategories();
            ViewData["CategoryName"] = General.DataTableToSelectList(dtCategory, "CategoryId", "CategoryName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtDepreciationMethods = General.GetDepreciationMethods();
            ViewData["DepreciationMethods"] = General.DataTableToSelectList(dtDepreciationMethods, "Value", "Text", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            eAssetDefinition.AssetDefinitionId = Convert.ToInt32(Request.Params["AssetDefinitionId"]);
            AssetDefinitionBI.LoadAssetDefinition(ref eAssetDefinition);
            return PartialView(eAssetDefinition);
        }

        [HttpPost]
        public ActionResult PostEditAssetDefinition(AssetDefinition eAssetDefinition)
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
                            message = "Sorry! the Asset Name " + eAssetDefinition.AssetName + " " + FASM_Msg.AlreadyExist;
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
    }
}