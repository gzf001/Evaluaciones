using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Evaluaciones.Helpers
{
    public static class ValidationSummary
    {
        public static string CustomValidationSummary(this HtmlHelper helper, string validationMessage = "")
        {
            StringBuilder html = new StringBuilder(string.Empty);

            if (!helper.ViewData.ModelState.IsValid)
            {
                html.Append(@"<div class=""alert alert-sm alert-border-left alert-danger alert-dismissable"">");
                html.Append(@"<button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button>");
                html.Append(@"</ul>");

                foreach (var key in helper.ViewData.ModelState.Keys)
                {
                    foreach (var error in helper.ViewData.ModelState[key].Errors)
                    {
                        html.Append(@"<li>" + helper.Encode(error.ErrorMessage) + @"</li>");
                    }
                }

                html.Append(@"</ul></div>");
            }
            return html.ToString();
        }
    }
}