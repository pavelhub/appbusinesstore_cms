using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AppB2.Models;
using AppB2.Custom;

namespace AppB2.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Index()
        {
            IEnumerable<MembershipUser> users = Membership.GetAllUsers().Cast<MembershipUser>();
            ViewBag.Users = users;

            return View();
        }

        [HttpPost]
        public ActionResult SetLanguage(string lang)
        {
            HttpCookie c = new HttpCookie("lang", lang);
            c.HttpOnly = true;
            Response.Cookies.Add(c);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public RedirectToRouteResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public RedirectToRouteResult Index(Credentials model)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Email, model.Password))
                {
                    string userId = (string)Membership.FindUsersByEmail(model.Email).Cast<MembershipUser>().First().ProviderUserKey;
                    Session["UserId"] = userId;

                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    string [] userRoles = Roles.GetRolesForUser(model.Email);

                    if (userRoles.Contains("admin")) return RedirectToAction("CreateFranchize", "Account");
                    else if (userRoles.Contains("franchize")) return RedirectToAction("Index", "Franchize");
                    else if (userRoles.Contains("owner"))
                    {
                        Session["OwnerId"] = DAL.DataManager.GetOwnerId(userId);
                        return RedirectToAction("List", "App");
                    }
                }
            }

            return RedirectToAction("Index", "Login");
        }
    }
}
