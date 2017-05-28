using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Evaluaciones.Helpers
{
    public static class LinkExtension
    {
        public static MvcHtmlString ActionImage(this HtmlHelper htmlHelper, string action, string controller, string routeImage)
        {
            TagBuilder t = new TagBuilder("a");

            t.MergeAttribute("href", string.Format("/{0}/{1}", controller, action));

            t.InnerHtml = string.Format("<img src=\"{0}\" alt=\"#\" />", routeImage).Replace("~", string.Empty);

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }
    }
}
