using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppB2.Custom
{
    public static class JqueryValidationLocaleScriptRenderHelper
    {
        static readonly List<string> _avaluable = new List<string>
        {
            "cz","da","de","en","es","et","fa","fi","fr","hr","hu","it","ja","nl","pl","pt","pt_BR","ro","ru","se","tr","vi","zh_CN","zh_TW"
        };

        public static MvcHtmlString JqueryValidationLocale(this System.Web.Mvc.HtmlHelper helper, string culture)
        {
            string src = "/Scripts/jquery.validationEngine.languages/jquery.validationEngine-";
            string result = string.Format("<script type='text/javascript' src='{0}en.js'></script>",src); //default
            
            if (!string.IsNullOrEmpty(culture))
            {
                IEnumerable<string> match = _avaluable.Where(x=>x.Contains(culture));
                if (match.Count() > 0) 
                    result = string.Format("<script type='text/javascript' src='{0}{1}.js'></script>",src,match.First());
            }
                         
            return new MvcHtmlString(result);
        }
    }
}