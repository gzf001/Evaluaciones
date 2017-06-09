using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Evaluaciones.Helpers
{
    public static class MenuExtension
    {
        public static MvcHtmlString Menu(this HtmlHelper helper, Guid usuarioId)
        {
            Evaluaciones.Membresia.Usuario usuario = Evaluaciones.Membresia.Usuario.Get(usuarioId);

            Evaluaciones.Persona persona = usuario.Persona;

            TagBuilder t = new TagBuilder("ul");

            t.Attributes.Add("id", "MenuPrincipal");

            t.AddCssClass("cl-vnavigation");

            bool primero = true;

            foreach (Evaluaciones.Membresia.Aplicacion aplicacion in Evaluaciones.Membresia.Aplicacion.GetAll(persona))
            {
                List<Evaluaciones.Membresia.MenuItem> itemPadre = Evaluaciones.Membresia.MenuItem.GetAll(Evaluaciones.Membresia.Menu.MenuPrincipal, aplicacion, aplicacion.Inicio);

                t.InnerHtml += "<li><a href='#'><i class='fa fa-home'></i><span>" + aplicacion.Nombre + "</span></a>";
                t.InnerHtml += "<ul class='sub-menu'>";

                foreach (Evaluaciones.Membresia.MenuItem menuItem in itemPadre)
                {
                    List<Evaluaciones.Membresia.MenuItem> items = Evaluaciones.Membresia.MenuItem.GetAll(menuItem);

                    if (items.Any())
                    {
                        if (primero)
                        {
                            t.InnerHtml += "<li class='active'><a href='#'><span>" + menuItem.Nombre + "</span><i class='fa fa-chevron-down'></i></a>";

                            primero = false;
                        }
                        else
                        {
                            t.InnerHtml += "<li><a href='#'>" + menuItem.Nombre + "<i class='fa fa-chevron-down'></i></a>";
                        }

                        t.InnerHtml += "<ul class='sub-menu'>";
                        t.InnerHtml += MenuExtension.MenuString(menuItem, 10);
                        t.InnerHtml += "</ul></li>";
                    }
                    else
                    {
                        if (primero)
                        {
                            t.InnerHtml += string.Format("<li class='active'><a href='{0}'>{1}</a></li>", menuItem.Url, menuItem.Nombre);

                            primero = false;
                        }
                        else
                        {
                            t.InnerHtml += string.Format("<li><a href='{0}'>{1}</a></li>", menuItem.Url, menuItem.Nombre);
                        }
                    }
                }

                t.InnerHtml += "</ul></li>";
            }

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }

        private static string MenuString(Evaluaciones.Membresia.MenuItem menuItem, int padding)
        {
            string retorno = string.Empty;

            foreach (Evaluaciones.Membresia.MenuItem m in Evaluaciones.Membresia.MenuItem.GetAll(menuItem))
            {
                string style = string.Format("style='padding-left: {0}px;'", padding);

                List<Evaluaciones.Membresia.MenuItem> items = Evaluaciones.Membresia.MenuItem.GetAll(m);

                if (items.Any())
                {
                    retorno += string.Format("<li><a href='{0}'><span {1}>" + m.Nombre + "</span><i class='fa fa-chevron-down parent'></i></a>", m.Url, style);
                    retorno += "<ul class=\"sub-menu\">";
                    retorno += MenuExtension.MenuString(m, padding + 5);
                    retorno += "</ul></li>";
                }
                else
                {
                    retorno += string.Format("<li><a href='{0}'><span {1}>" + m.Nombre + "</span></a>", m.Url, style);
                    retorno += "</li>";
                }
            }

            return retorno;
        }

        public static MvcHtmlString MenuOrderable(this HtmlHelper helper, Evaluaciones.Membresia.Aplicacion aplicacion, Evaluaciones.Web.Controller controller)
        {
            if (!aplicacion.MenuItemId.HasValue)
            {
                throw new Exception("La aplicación no tiene la relación con el ítem de menú root");
            }

            TagBuilder t = new TagBuilder("div");

            t.Attributes.Add("id", "menu");

            t.AddCssClass("dd");

            t.InnerHtml += "<ol class='dd-list'>";

            foreach (Evaluaciones.Membresia.MenuItem menuItem in Evaluaciones.Membresia.MenuItem.GetAll(Evaluaciones.Membresia.Menu.MenuPrincipal, aplicacion, aplicacion.Inicio))
            {
                t.InnerHtml += "<li class='dd-item' data-id='" + menuItem.Id.ToString() + "'>";

                t.InnerHtml += string.Format("<div class='dd-handle'>{0}</div>", menuItem.Nombre);

                t.InnerHtml += string.Format("<div>{0}{1}{2}</div>", Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(menuItem.Id, menuItem.MenuItemId, TypeButton.Add, controller),
                                                                     Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(menuItem.Id, menuItem.MenuItemId, TypeButton.Edit, controller),
                                                                     Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(menuItem.Id, menuItem.MenuItemId, TypeButton.Delete, controller));

                string html = MenuExtension.MenuOrderable(menuItem, controller);

                if (string.IsNullOrEmpty(html))
                {
                    t.InnerHtml += "</li>";
                }
                else
                {
                    t.InnerHtml += "<ol class='dd-list'>";

                    t.InnerHtml += html;

                    t.InnerHtml += "</ol></li>";
                }
            }

            t.InnerHtml += "</ol>";

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }

        private static string MenuOrderable(Evaluaciones.Membresia.MenuItem menuItem, Evaluaciones.Web.Controller controller)
        {
            string retorno = string.Empty;

            foreach (Evaluaciones.Membresia.MenuItem m in Evaluaciones.Membresia.MenuItem.GetAll(menuItem))
            {
                List<Evaluaciones.Membresia.MenuItem> items = Evaluaciones.Membresia.MenuItem.GetAll(m);

                if (items.Any())
                {
                    retorno += string.Format("<li class='dd-item' data-id='{0}'>", m.Id);
                    retorno += string.Format("<div class='dd-handle'>{0}</div>", m.Nombre);
                    retorno += string.Format("<div>{0}{1}{2}</div>", Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(m.Id, m.MenuItemId, TypeButton.Add, controller),
                                                                     Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(m.Id, m.MenuItemId, TypeButton.Edit, controller),
                                                                     Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(m.Id, m.MenuItemId, TypeButton.Delete, controller));
                    retorno += "<ol class='dd-list'>";
                    retorno += MenuExtension.MenuOrderable(m, controller);
                    retorno += "</ol></li>";
                }
                else
                {
                    retorno += string.Format("<li class='dd-item' data-id='{0}'>", m.Id);
                    retorno += string.Format("<div class='dd-handle'>{0}</div>", m.Nombre);
                    retorno += string.Format("<div>{0}{1}</div>", Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(m.Id, m.MenuItemId, TypeButton.Edit, controller),
                                                                  Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(m.Id, m.MenuItemId, TypeButton.Delete, controller));
                    retorno += "</li>";
                }
            }

            return retorno;
        }
    }
}