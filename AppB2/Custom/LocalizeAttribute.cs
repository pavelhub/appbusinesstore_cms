using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Globalization;
using System.Web.Mvc;

namespace AppB2.Custom
{
    public class LocalizeAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string lang = string.Empty;

            if (filterContext.HttpContext.Request.Cookies["lang"] != null)
            {
                string cookieLang = filterContext.HttpContext.Request.Cookies["lang"].Value;
                lang = CultureHelper.GetValidCulture(cookieLang);
            }
            else
            {
                string headerLang = filterContext.HttpContext.Request.Headers.Get("Accept-Language");
                lang = CultureHelper.GetHeaderLang(headerLang);
            }

            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang);

            base.OnResultExecuting(filterContext);
        }
    }
}