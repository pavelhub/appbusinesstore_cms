using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace AppB2.Custom
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            string lang = string.Empty;

            if (requestContext.HttpContext.Request.Cookies["lang"] != null)
            {
                string cookieLang = requestContext.HttpContext.Request.Cookies["lang"].Value;
                lang = CultureHelper.GetValidCulture(cookieLang);
            }
            else
            {
                string headerLang = requestContext.HttpContext.Request.Headers.Get("Accept-Language");
                lang = CultureHelper.GetHeaderLang(headerLang);           
            }

            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang);
            ViewBag._lang = lang;

            base.Initialize(requestContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Is it View ?
            //    ViewResultBase view = filterContext.Result as ViewResultBase;
            // if (view == null) // if not exit
            //      return;

            //         string cultureName = Thread.CurrentThread.CurrentCulture.Name; // e.g. "en-US" // filterContext.HttpContext.Request.UserLanguages[0]; // needs validation return "en-us" as default            

            // Is it default culture? exit
            //if (cultureName == CultureHelper.GetDefaultCulture())
            //  return;

            // Are views implemented separately for this culture?  if not exit
            //bool viewImplemented = CultureHelper.IsViewSeparate(cultureName);
            // if (viewImplemented == false)
            //   return;

            //filterContext.Controller.ViewBag._culture = cultureName;

            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru");
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("ru");

            /*string languageId = "en";
            try
            {
                // all your code here. You have access to all the context information, 
                // like querystring values:
                string languageId = requestContext.HttpContext.Request.QueryString["lang"];
                Thread.CurrentThread.CurrentUICulture =
                   CultureInfo.CreateSpecificCulture(languageId);
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture =
                  CultureInfo.CreateSpecificCulture(languageId);
            }
              base.Initialize(requestContext);
             */

            base.OnActionExecuted(filterContext);
        }

        /*string cultureName = null;
        // Attempt to read the culture cookie from Request
        HttpCookie cultureCookie = Request.Cookies["_culture"];
        if (cultureCookie != null)
            cultureName = cultureCookie.Value;
        else
            cultureName = Request.UserLanguages[0]; // obtain it from HTTP header AcceptLanguages

        // Validate culture name
        cultureName = CultureHelper.GetValidCulture(cultureName); // This is safe



        // Modify current thread's culture            
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru");
        Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("ru");
            
            
        base.ExecuteCore();*/

    }
}