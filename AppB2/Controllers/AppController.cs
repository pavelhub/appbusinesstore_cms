using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppB2.Models;
using AppB2.Custom;
using DAL;

namespace AppB2.Controllers
{
    [Authorize(Roles = "owner")]
    [SessionExpireFilter("Login", "Index")]
    public class AppController : Controller
    {
        public ViewResult List()
        {
            List<application> apps = DataManager.GetApps();
            IEnumerable<App> viewModelApps = null;

            if (apps.Count > 0)
            {
                viewModelApps = apps.Select(x => new App
                {
                    Name = x.app_name,
                    IsActive = x.is_active,
                    Id = x.id
                });
            }

            return View(viewModelApps);
        }

        public ActionResult Create()
        {
            ViewBag.Mode = "Create";
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Create([Bind(Exclude = "Id")]App newApp)
        {
            if (ModelState.IsValid)
            {
                application app = new application();
                app.app_name = newApp.Name;
                app.is_active = newApp.IsActive;
                app.owner_id = Convert.ToInt32(Session["OwnerId"]);
                TempData["Test"] = DataManager.CreateApp(app);
            }

            return RedirectToAction("List");
        }

        public ActionResult Update(int id = 0)
        {
            ViewBag.Mode = "Update";

            if (id != 0)
            {
                application app = DataManager.GetAppById(id);
                if (app != null)
                {
                    App viewModelApp = new App
                    {
                        Id = app.id,
                        IsActive = app.is_active,
                        Name = app.app_name
                    };

                    return View(viewModelApp);
                }
            }

            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Update(App app)
        {
            if (ModelState.IsValid)
            {
                application updatedApp = new application
                {
                    id = app.Id,
                    app_name = app.Name,
                    is_active = app.IsActive,
                    owner_id = Convert.ToInt32(Session["OwnerId"])
                };
                DataManager.UpdateApp(updatedApp);
            }

            return RedirectToAction("List");
        }

        public RedirectToRouteResult Delete(int id = 0)
        {
            if (id != 0) DataManager.DeleteApp(id, Convert.ToInt32(Session["OwnerId"]));
            return RedirectToAction("List");
        }
    }
}
