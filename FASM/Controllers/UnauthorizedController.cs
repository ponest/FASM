using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FASM.Controllers
{
    public class UnauthorizedController : Controller
    {
        // GET: Unauthorized
        public ActionResult Index()
        {
            Session.Abandon();
            return View();
        }
    }
}