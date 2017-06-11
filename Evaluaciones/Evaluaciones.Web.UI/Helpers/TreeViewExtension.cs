using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Web.UI.Helpers
{
    public static class TreeViewExtension
    {
        public static MvcHtmlString TreeViewMenu(this HtmlHelper helper, string id)
        {
            TagBuilder t = new TagBuilder("ul");

            t.Attributes.Add("id", id);

            string fisicalPath = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.FilePath);

            fisicalPath = string.Format("{0}\\Areas", fisicalPath.Substring(0, fisicalPath.LastIndexOf(".UI") + 4));

            foreach (string directory in System.IO.Directory.GetDirectories(fisicalPath))
            {
                string area = directory.Remove(0, directory.LastIndexOf("\\") + 1);

                t.InnerHtml += string.Format("<li class='folder expanded'>{0}", area);

                t.InnerHtml += "<ul>";

                string targetDirectory = string.Format("{0}\\Controllers", directory);

                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(targetDirectory);

                foreach (System.IO.FileInfo fileInfo in directoryInfo.GetFiles())
                {
                    string className = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);

                    string controlador = className.Replace("Controller", string.Empty);

                    t.InnerHtml += string.Format("<li class='folder expanded'>{0}", controlador);

                    t.InnerHtml += "<ul>";

                    string classFullName = string.Format("Evaluaciones.Web.UI.Areas.{0}.Controllers.{1}", area, className);

                    Type type = Type.GetType(classFullName);

                    //Obtención de métodos que sea tipo GET
                    IEnumerable<System.Reflection.MethodInfo> getMethod = type.GetMethods().Where<System.Reflection.MethodInfo>(x => x.CustomAttributes.Any<System.Reflection.CustomAttributeData>(y => y.AttributeType.Equals(typeof(System.Web.Mvc.HttpGetAttribute))) && !x.GetParameters().Any<System.Reflection.ParameterInfo>());

                    foreach (System.Reflection.MethodInfo methodInfo in getMethod)
                    {
                        t.InnerHtml += string.Format("<li>/{0}/{1}/{2}", area, controlador, methodInfo.Name);
                    }

                    t.InnerHtml += "</ul></li>";
                }

                t.InnerHtml += "</ul></li>";
            }

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }
    }
}