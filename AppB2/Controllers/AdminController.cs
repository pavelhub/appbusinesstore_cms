using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppB2.Models;
using System.Web.Security;
using AppB2.Custom;

namespace AppB2.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : BaseController
    {     
        public ActionResult Index()
        {           
            return View();
        }

        public RedirectToRouteResult CreateFranchize(Credentials model)
        {
            if (ModelState.IsValid)
            {
                MembershipUser mu = Membership.CreateUser(model.Email, model.Password, model.Email);
                if (mu != null) {
                    Roles.AddUserToRole(mu.UserName, "franchize");                  
                    TempData["isCreated"] = "User is created"; 
                }
                else TempData["isCreated"] = "User not created";
            }

            return RedirectToAction("Index");
        }
    }
}
