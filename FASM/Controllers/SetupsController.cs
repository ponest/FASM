using System;
using System.Web.Mvc;
using FASM_GN;
using FASM_EN.Setups;
using FASM_BI.Setups;
using FASM.Action_Filters;
using FASM.GeneralObjects;
using System.Data;

namespace FixedAssetsManagement.Controllers
{
    [FASM]
    public class SetupsController : Controller
    {

        // GET: Setups
        #region Categories
        public ActionResult IndexCategories()
        {
           
            ViewBag.AllowAdd = this.HasPermission(ControllerName.Setups + "-CreateCategory");
            ViewBag.AllowEdit = this.HasPermission(ControllerName.Setups + "-PostEditCategory");
            ViewBag.AllowLoadEdit = this.HasPermission(ControllerName.Setups + "-LoadEditCategory");
            ViewBag.AllowDelete = this.HasPermission(ControllerName.Setups + "-DeleteCategory");

            Categories eCategory = new Categories();
            eCategory.dtCategory = CategoriesBI.GetCategories();
            return View(eCategory);
        }

        [HttpPost]
        public ActionResult CreateCategory(Categories eCategories)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    FASM_Enums.InfoMessages SaveResult = CategoriesBI.SaveCategories(ref eCategories);
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

            return View(eCategories);
        }

        [HttpPost]
        public ActionResult LoadEditCategory(Categories eCategories)
        {
            eCategories.CategoryId = Convert.ToInt32(Request.Params["CategoryId"]);
            CategoriesBI.LoadCategories(ref eCategories);
            return PartialView(eCategories);
        }

        [HttpPost]
        public ActionResult PostEditCategory(Categories eCategories)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    FASM_Enums.InfoMessages Results = CategoriesBI.SaveCategories(ref eCategories);

                    switch (Results)
                    {
                        case FASM_Enums.InfoMessages.Success:
                            message = FASM_Msg.Updated;
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
            return View(eCategories);
        }

        [HttpPost]
        public ActionResult DeleteCategory(Categories eCategories)
        {
            string message = "";
            try
            {
                if (eCategories.CategoryId > 0)
                {
                    FASM_Enums.InfoMessages DeleteResult = CategoriesBI.DeleteCategories(eCategories.CategoryId);
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

        #region Location
        public ActionResult IndexLocation()
        {
            ViewBag.AllowAdd = this.HasPermission(ControllerName.Setups + "-CreateLocation");
            ViewBag.AllowEdit = this.HasPermission(ControllerName.Setups + "-PostEditLocation");
            ViewBag.AllowLoadEdit = this.HasPermission(ControllerName.Setups + "-LoadEditLocation");
            ViewBag.AllowDelete = this.HasPermission(ControllerName.Setups + "-DeleteLocation");

            Location eLocation = new Location();
            eLocation.dtLocation = LocationBI.GetLocation();
            return View(eLocation);
        }

        [HttpPost]
        public ActionResult CreateLocation(Location eLocation)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    FASM_Enums.InfoMessages SaveResult = LocationBI.SaveLocation(ref eLocation);
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

            return View(eLocation);
        }

        [HttpPost]
        public ActionResult LoadEditLocation(Location eLocation)
        {
            eLocation.LocationId = Convert.ToInt32(Request.Params["LocationId"]);
            LocationBI.LoadLocation(ref eLocation);
            return PartialView(eLocation);
        }

        [HttpPost]
        public ActionResult PostEditLocation(Location eLocation)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    FASM_Enums.InfoMessages Results = LocationBI.SaveLocation(ref eLocation);

                    switch (Results)
                    {
                        case FASM_Enums.InfoMessages.Success:
                            message = FASM_Msg.Updated;
                            break;
                        case FASM_Enums.InfoMessages.AlreadyExist:
                            message = "Sorry! Location name " + eLocation.LocationName + " " + FASM_Msg.AlreadyExist;
                            break;
                    }
                    return Json(new { msg = message, JsonRequestBehavior.AllowGet });
                }
                catch (Exception ex)
                {
                    ViewBag.CatchedMsg = ex.Message;
                }
            }
            return View(eLocation);
        }

        [HttpPost]
        public ActionResult DeleteLocation(Location eLocation)
        {
            string message = "";
            try
            {
                if (eLocation.LocationId > 0)
                {
                    FASM_Enums.InfoMessages DeleteResult = LocationBI.DeleteLocation(eLocation.LocationId);
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

        #region Districts
        public ActionResult IndexDistrict()
        {
            ViewBag.AllowAdd = this.HasPermission(ControllerName.Setups + "-CreateDistrict");
            ViewBag.AllowEdit = this.HasPermission(ControllerName.Setups + "-PostEditDistrict");
            ViewBag.AllowLoadEdit = this.HasPermission(ControllerName.Setups + "-LoadEditDistrict");
            ViewBag.AllowDelete = this.HasPermission(ControllerName.Setups + "-DeleteDistrict");

            Districts eDistricts = new Districts();
            eDistricts.dtDistricts = DistrictsBI.GetDistricts();
            return View(eDistricts);
        }

        [HttpPost]
        public ActionResult CreateDistrict(Districts eDistricts)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    FASM_Enums.InfoMessages SaveResult = DistrictsBI.SaveDistricts(ref eDistricts);
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

            return View(eDistricts);
        }

        [HttpPost]
        public ActionResult LoadEditDistrict(Districts eDistricts)
        {
            eDistricts.DistrictId = Convert.ToInt32(Request.Params["DistrictId"]);
            DistrictsBI.LoadDistricts(ref eDistricts);
            return PartialView(eDistricts);
        }

        [HttpPost]
        public ActionResult PostEditDistrict(Districts eDistricts)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    FASM_Enums.InfoMessages Results = DistrictsBI.SaveDistricts(ref eDistricts);

                    switch (Results)
                    {
                        case FASM_Enums.InfoMessages.Success:
                            message = FASM_Msg.Updated;
                            break;
                        case FASM_Enums.InfoMessages.AlreadyExist:
                            message = "Sorry! the district " + eDistricts.DistrictName + " " + FASM_Msg.AlreadyExist;
                            break;
                    }
                    return Json(new { msg = message, JsonRequestBehavior.AllowGet });
                }
                catch (Exception ex)
                {
                    ViewBag.CatchedMsg = ex.Message;
                }
            }
            return View(eDistricts);
        }

        [HttpPost]
        public ActionResult DeleteDistrict(Districts eDistricts)
        {
           
            string message = "";
            try
            {
                if (eDistricts.DistrictId > 0)
                {
                    FASM_Enums.InfoMessages DeleteResult = DistrictsBI.DeleteDistricts(eDistricts.DistrictId);
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

        #region Region
        public ActionResult IndexRegions()
        {
            ViewBag.AllowAdd = this.HasPermission(ControllerName.Setups + "-CreateRegions");
            ViewBag.AllowEdit = this.HasPermission(ControllerName.Setups + "-PostEditRegions");
            ViewBag.AllowLoadEdit = this.HasPermission(ControllerName.Setups + "-LoadEditRegions");
            ViewBag.AllowDelete = this.HasPermission(ControllerName.Setups + "-DeleteRegions");

            Regions eRegions = new Regions();
            eRegions.dtRegion = RegionsBI.GetRegions();
            return View(eRegions);
        }

        [HttpPost]
        public ActionResult CreateRegions(Regions eRegions)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    FASM_Enums.InfoMessages SaveResult = RegionsBI.SaveRegions(ref eRegions);
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

            return View(eRegions);
        }

        [HttpPost]
        public ActionResult LoadEditRegions(Regions eRegions)
        {
            eRegions.RegionId = Convert.ToInt32(Request.Params["RegionId"]);
            RegionsBI.LoadRegions(ref eRegions);
            return PartialView(eRegions);
        }

        [HttpPost]
        public ActionResult PostEditRegions(Regions eRegions)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    FASM_Enums.InfoMessages Results = RegionsBI.SaveRegions(ref eRegions);

                    switch (Results)
                    {
                        case FASM_Enums.InfoMessages.Success:
                            message = FASM_Msg.Updated;
                            break;
                        case FASM_Enums.InfoMessages.AlreadyExist:
                            message = "Sorry! the region " + eRegions.RegionName + " " + FASM_Msg.AlreadyExist;
                            break;
                    }
                    return Json(new { msg = message, JsonRequestBehavior.AllowGet });
                }
                catch (Exception ex)
                {
                    ViewBag.CatchedMsg = ex.Message;
                }
            }
            return View(eRegions);
        }

        [HttpPost]
        public ActionResult DeleteRegions(Regions eRegions)
        {

            string message = "";
            try
            {
                if (eRegions.RegionId > 0)
                {
                    FASM_Enums.InfoMessages DeleteResult = RegionsBI.DeleteRegions(eRegions.RegionId);
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

        #region Supplier
        public ActionResult IndexSuppliers()
        {
            ViewBag.AllowAdd = this.HasPermission(ControllerName.Setups + "-CreateSuppliers");
            ViewBag.AllowEdit = this.HasPermission(ControllerName.Setups + "-PostEditSuppliers");
            ViewBag.AllowLoadEdit = this.HasPermission(ControllerName.Setups + "-LoadEditSuppliers");
            ViewBag.AllowDelete = this.HasPermission(ControllerName.Setups + "-DeleteSuppliers");

            DataTable dtRegion = RegionsBI.GetRegions();
            ViewData["RegionName"] = General.DataTableToSelectList(dtRegion, "RegionId", "RegionName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtDistrict = DistrictsBI.GetDistricts();
            ViewData["DistrictName"] = General.DataTableToSelectList(dtDistrict, "DistrictId", "DistrictName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });


            Suppliers eSuppliers = new Suppliers();
            eSuppliers.dtSuppliers = SuppliersBI.GetSuppliers();
            return View(eSuppliers);
        }

        [HttpPost]
        public ActionResult CreateSuppliers(Suppliers eSuppliers)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    DataTable dtRegion = RegionsBI.GetRegions();
                    ViewData["RegionName"] = General.DataTableToSelectList(dtRegion, "RegionId", "RegionName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

                    DataTable dtDistrict = DistrictsBI.GetDistricts();
                    ViewData["DistrictName"] = General.DataTableToSelectList(dtDistrict, "DistrictId", "DistrictName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

                    FASM_Enums.InfoMessages SaveResult = SuppliersBI.SaveSuppliers(ref eSuppliers);
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

            return View(eSuppliers);
        }

        [HttpPost]
        public ActionResult LoadEditSuppliers(Suppliers eSuppliers)
        {
            DataTable dtRegion = RegionsBI.GetRegions();
            ViewData["RegionName"] = General.DataTableToSelectList(dtRegion, "RegionId", "RegionName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            DataTable dtDistrict = DistrictsBI.GetDistricts();
            ViewData["DistrictName"] = General.DataTableToSelectList(dtDistrict, "DistrictId", "DistrictName", "0", TopEmptyItem: new SelectListItem { Value = "0", Text = "" });

            eSuppliers.SupplierId = Convert.ToInt32(Request.Params["SupplierId"]);
            SuppliersBI.LoadSuppliers(ref eSuppliers);
            return PartialView(eSuppliers);
        }

        [HttpPost]
        public ActionResult PostEditSuppliers(Suppliers eSuppliers)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    FASM_Enums.InfoMessages Results = SuppliersBI.SaveSuppliers(ref eSuppliers);

                    switch (Results)
                    {
                        case FASM_Enums.InfoMessages.Success:
                            message = FASM_Msg.Updated;
                            break;
                        case FASM_Enums.InfoMessages.AlreadyExist:
                            message = "Sorry! the Email " + eSuppliers.Email + " " + FASM_Msg.AlreadyExist;
                            break;
                    }
                    return Json(new { msg = message, JsonRequestBehavior.AllowGet });
                }
                catch (Exception ex)
                {
                    ViewBag.CatchedMsg = ex.Message;
                }
            }
            return View(eSuppliers);
        }

        [HttpPost]
        public ActionResult DeleteSuppliers(Suppliers eSuppliers)
        {

            string message = "";
            try
            {
                if (eSuppliers.SupplierId > 0)
                {
                    FASM_Enums.InfoMessages DeleteResult = SuppliersBI.DeleteSuppliers(eSuppliers.SupplierId);
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