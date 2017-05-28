using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Evaluaciones.Helpers
{
    public static class DropDownListExtensions
    {
        public static MvcHtmlString DropDownListFor1<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            Type tipo = expression.ReturnType;
            
            TagBuilder t = new TagBuilder("select");

            foreach (var property in htmlAttributes.GetType().GetProperties())
            {
                t.MergeAttribute(property.Name, property.GetValue(htmlAttributes, null).ToString());
            }

            t.InnerHtml += "<option value='-1'>[Seleccione]</option>";

            if (tipo.GetMethods().Count(x => x.Name.Equals("GetAll")) > 1)
            {
                return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
            }

            System.Reflection.MethodInfo getAll = tipo.GetMethod("GetAll");

            foreach (var item in getAll.Invoke(null, null) as IEnumerable<object>)
            {
                System.Reflection.PropertyInfo codigo = item.GetType().GetProperty("Codigo");

                System.Reflection.PropertyInfo nombre = item.GetType().GetProperty("Nombre");

                t.InnerHtml += string.Format("<option value='{0}'>{1}</option>", codigo.GetValue(item, null), nombre.GetValue(item, null));
            }

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }
    }
}
