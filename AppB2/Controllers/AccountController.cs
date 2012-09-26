using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppB2.Models;
using System.Web.Security;
using AppB2.Custom;
using DAL;

namespace AppB2.Controllers
{
    public class AccountController : Controller
    {
        [Authorize(Roles = "admin")]
        public ViewResult CreateFranchize()
        {
            return View();
        }

        public ViewResult CreateOwner()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CreateFranchize(NewFranchize fch)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MembershipUser mu = Membership.CreateUser(fch.Email, fch.Password, fch.Email);

                    if (mu != null)
                    {
                        System.Web.Security.Roles.AddUserToRole(fch.Email, "franchize");
                        user_franchaser newFranchizer = new user_franchaser
                        {
                            first_name = fch.FirstName,
                            last_name = fch.LastName,
                            company_name = fch.CompanyName,
                            country = fch.Country,
                            domain_name = fch.DomainName,
                            email = fch.Email,
                            telephone = fch.Phone,
                            user_id = (string)mu.ProviderUserKey
                        };
                        DataManager.CreateFranchize(newFranchizer);
                    }
                }
                catch (MembershipCreateUserException muex)
                {
                    TempData["Error"] = Helper.GetMembershipUserCreateStatus(muex.StatusCode);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }

            return RedirectToAction("CreateFranchize");
        }

        [HttpPost]
        public ActionResult CreateOwner(NewOwner no)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MembershipUser mu = Membership.CreateUser(no.Email, no.Password, no.Email);

                    if (mu != null)
                    {
                        System.Web.Security.Roles.AddUserToRole(no.Email, "owner");
                        user_owner newOwner = new user_owner
                        {
                            first_name = no.FirstName,
                            last_name = no.LastName,
                            email = no.Email,
                            telephone = no.Phone,
                            franchaser_id = no.FranchizerId,
                            user_id = (string)mu.ProviderUserKey
                        };
                        DataManager.CreateOwner(newOwner);
                    }
                }
                catch (MembershipCreateUserException muex)
                {
                    TempData["Error"] = Helper.GetMembershipUserCreateStatus(muex.StatusCode);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }

            return RedirectToAction("CreateOwner");
        }
    }
}
