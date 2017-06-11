using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Evaluaciones.Helpers
{
    public static class BreadCrumb
    {
        public static string BuildBreadcrumbNavigation(this HtmlHelper helper)
        {
            System.Web.Routing.RouteData routeData = helper.ViewContext.RouteData;

            string controller = routeData.Values["controller"].ToString();

            string action = routeData.Values["action"].ToString();

            if (controller == "Admin" && action == "Index")
            {
                return string.Empty;
            }

            StringBuilder breadcrumb = new StringBuilder("<ol class='breadcrumb'>");

            breadcrumb.Append("<li class='crumb-active'>");
            breadcrumb.Append(helper.ActionLink("Dashboard", "Index", "Dashboard", new { Area = "Admin" }, new { }).ToHtmlString());
            breadcrumb.Append("</li>");

            breadcrumb.Append("<li class='crumb-icon'><span class='glyphicon glyphicon-home'></span>");
            breadcrumb.Append("</li>");

            breadcrumb.Append("<li>");
            breadcrumb.Append(helper.ActionLink(helper.ViewContext.RouteData.Values["controller"].ToString().Titleize(),
                                               "Index",
                                               helper.ViewContext.RouteData.Values["controller"].ToString()));
            breadcrumb.Append("</li>");

            if (helper.ViewContext.RouteData.Values["action"].ToString() != "Index")
            {
                breadcrumb.Append("<li>");
                breadcrumb.Append(helper.ViewContext.RouteData.Values["action"].ToString());
                breadcrumb.Append("</li>");
            }

            return breadcrumb.Append("</ol>").ToString();
        }
    }
}