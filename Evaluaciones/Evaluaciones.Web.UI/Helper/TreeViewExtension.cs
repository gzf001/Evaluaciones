using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Web.UI.Helper
{
    public static class TreeViewExtension
    {
        public static MvcHtmlString TreeViewMenu(this HtmlHelper helper)
        {
            TagBuilder t = new TagBuilder("ul");

            t.Attributes.Add("id", "path");

            t.AddCssClass("nav nav-list treeview collapse");

            string fisicalPath = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.FilePath);

            fisicalPath = string.Format("{0}\\Areas", fisicalPath.Substring(0, fisicalPath.LastIndexOf(".UI") + 4));

            foreach (string directory in System.IO.Directory.GetDirectories(fisicalPath))
            {
                string area = directory.Remove(0, directory.LastIndexOf("\\") + 1);

                t.InnerHtml += "<li class='open'>";

                t.InnerHtml += string.Format("<label class='tree-toggler nav-header'><i class='fa fa-folder-open-o'></i>{0}</label>", area);

                t.InnerHtml += "<ul class='nav nav-list tree'>";

                string targetDirectory = string.Format("{0}\\Controllers", directory);

                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(targetDirectory);

                foreach (System.IO.FileInfo fileInfo in directoryInfo.GetFiles())
                {
                    string className = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);

                    string controlador = className.Replace("Controller", string.Empty);

                    t.InnerHtml += "<li class='open'>";

                    t.InnerHtml += string.Format("<label class='tree-toggler nav-header'><i class='fa fa-folder-open-o'></i>{0}</label>", controlador);

                    t.InnerHtml += "<ul class='nav nav-list tree'>";

                    string classFullName = string.Format("Evaluaciones.Web.UI.Areas.{0}.Controllers.{1}", area, className);

                    Type type = Type.GetType(classFullName);

                    //Obtención de métodos que sea tipo GET
                    IEnumerable<System.Reflection.MethodInfo> getMethod = type.GetMethods().Where<System.Reflection.MethodInfo>(x => x.CustomAttributes.Any<System.Reflection.CustomAttributeData>(y => y.AttributeType.Equals(typeof(System.Web.Mvc.HttpGetAttribute))) && !x.GetParameters().Any<System.Reflection.ParameterInfo>());

                    foreach (System.Reflection.MethodInfo methodInfo in getMethod)
                    {
                        t.InnerHtml += string.Format("<li><a href='#' data-value='/{0}/{1}/{2}'>{3}()</a></li>", area, controlador, methodInfo.Name, methodInfo.Name);
                    }

                    t.InnerHtml += "</ul></li>";
                }

                t.InnerHtml += "</ul></li>";
            }

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }
    }
}