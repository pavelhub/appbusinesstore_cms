using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppB2.Custom
{
    public static class LanguageDropDownHelper
    {
        public static MvcHtmlString LanguageDropDown(this System.Web.Mvc.HtmlHelper helper, string id , string selectedCulture)
        {
            System.Text.StringBuilder html = new System.Text.StringBuilder();
            var cultures = CultureHelper.GetAll();

            html.Append(@"<select name='" + id + "' id='" + id + "'>");

            foreach (var element in cultures)
            {
                if (selectedCulture != null && selectedCulture.Equals(element.Value))
                    html.Append(@"<option value='" + element.Value + "' selected='selected'>");
                else html.Append(@"<option value='" + element.Value + "'>");
                html.Append(element.Key);
                html.Append("</option>");
            }
            html.Append("</select>");
            return new MvcHtmlString(html.ToString());
        }
    }
}