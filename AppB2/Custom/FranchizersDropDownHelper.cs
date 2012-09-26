using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace AppB2.Custom
{
    public static class FranchizersDropDownHelper
    {
        public static MvcHtmlString FranchizersDropDown(this System.Web.Mvc.HtmlHelper helper, string tagId)
        {
            System.Text.StringBuilder html = new System.Text.StringBuilder();
            List<user_franchaser> franchizers = DataManager.GetFranchizers();

            html.Append(@"<select name='" + tagId + "' id='" + tagId + "'>");

            foreach (var franchizer in franchizers)
            {
                html.Append(@"<option value='" + franchizer.id + "'>");
                html.Append(franchizer.company_name);
                html.Append("</option>");
            }
            html.Append("</select>");
            return new MvcHtmlString(html.ToString());
        }
    }
}