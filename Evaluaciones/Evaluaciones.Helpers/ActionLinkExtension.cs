using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Evaluaciones.Helpers
{
    public static class ActionLinkExtension
    {
        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string text, string action, string controller, string area, Evaluaciones.Helpers.TypeButton typeButton, object dataValue, string css, string faIcon, string toolTip)
        {
            TagBuilder t = new TagBuilder("a");

            if (!string.IsNullOrEmpty(css))
            {
                t.AddCssClass(css);
            }

            if (string.IsNullOrEmpty(action))
            {
                t.MergeAttribute("href", "#");
            }
            else
            {
                if (string.IsNullOrEmpty(area))
                {
                    t.MergeAttribute("href", string.Format("/{0}/{1}", controller, action));
                }
                else
                {
                    t.MergeAttribute("href", string.Format("/{0}/{1}/{2}", area, controller, action));
                }
            }

            if (!object.ReferenceEquals(dataValue, null))
            {
                t.MergeAttribute("data-value", dataValue.ToString());
            }

            if (!(typeButton.Equals(Evaluaciones.Helpers.TypeButton.Edit) || typeButton.Equals(Evaluaciones.Helpers.TypeButton.Edit)) && !string.IsNullOrEmpty(faIcon))
            {
                t.InnerHtml = string.Format("<i class='fa {0}'>{1}</i>", faIcon, text);
            }

            if (!(typeButton.Equals(Evaluaciones.Helpers.TypeButton.Edit) || typeButton.Equals(Evaluaciones.Helpers.TypeButton.Edit)) && !string.IsNullOrEmpty(toolTip))
            {
                t.MergeAttribute("title", toolTip);
            }

            switch (typeButton)
            {
                case Evaluaciones.Helpers.TypeButton.Edit:
                    {
                        t.MergeAttribute("title", "Editar");

                        t.InnerHtml = string.Format("<i class='fa fa-pencil'>{0}</i>", text);

                        break;
                    }
                case Evaluaciones.Helpers.TypeButton.Delete:
                    {
                        t.MergeAttribute("title", "Eliminar");

                        t.InnerHtml = string.Format("<i class='fa fa-times'>{0}</i>", text);

                        break;
                    }
            }

            t.MergeAttribute("onclick", "clickEdicion(this)");

            t.MergeAttribute("typeButton", typeButton.ToString());

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ActionLinkCrudEmbedded(Guid id, Guid? parentId, Evaluaciones.Helpers.TypeButton typeButton)
        {
            TagBuilder t = new TagBuilder("a");

            t.MergeAttribute("data-value", id.ToString());

            if (parentId.HasValue)
            {
                t.MergeAttribute("data-parent", parentId.Value.ToString());
            }

            switch (typeButton)
            {
                case Evaluaciones.Helpers.TypeButton.Add:
                    {
                        t.AddCssClass("btn btn-success btn-xs btn-flat md-trigger");

                        t.MergeAttribute("data-modal", "form-primary");

                        t.MergeAttribute("href", "#");

                        t.MergeAttribute("title", "Agregar");

                        t.InnerHtml = "<i class='fa fa-plus'></i>";

                        break;
                    }
                case Evaluaciones.Helpers.TypeButton.Edit:
                    {
                        t.AddCssClass("btn btn-primary btn-xs btn-flat md-trigger");

                        t.MergeAttribute("data-modal", "form-primary");

                        t.MergeAttribute("href", "#");

                        t.MergeAttribute("title", "Editar");

                        t.InnerHtml = "<i class='fa fa-pencil'></i>";

                        break;
                    }
                case Evaluaciones.Helpers.TypeButton.Delete:
                    {
                        t.AddCssClass("btn btn-danger btn-xs");

                        t.MergeAttribute("title", "Eliminar");

                        t.InnerHtml = "<i class='fa fa-times'></i>";

                        break;
                    }
            }

            t.MergeAttribute("typeButton", typeButton.ToString());

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }
    }
}