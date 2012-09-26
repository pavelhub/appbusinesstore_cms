using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppB2.Controllers
{
    [Authorize(Roles = "franchize")]
    public class FranchizeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
