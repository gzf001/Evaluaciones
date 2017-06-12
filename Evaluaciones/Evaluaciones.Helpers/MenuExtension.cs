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

            t.AddCssClass("nav sidebar-menu");

            t.InnerHtml += "<li class='sidebar-label pt20'>Menu</li>";

            t.InnerHtml += "<li class='active'><a href='dashboard.html'><span class='glyphicon glyphicon-home'></span><span class='sidebar-title'>Dashboard</span></a></li>";

            t.InnerHtml += "<li class='sidebar-label pt15'>Modulos</li>";

            foreach (Evaluaciones.Membresia.Aplicacion aplicacion in Evaluaciones.Membresia.Aplicacion.GetAll())
            {
                List<Evaluaciones.Membresia.MenuItem> itemPadre = Evaluaciones.Membresia.MenuItem.GetAll(Evaluaciones.Membresia.Menu.MenuPrincipal, aplicacion, aplicacion.Inicio);

                if (string.IsNullOrEmpty(aplicacion.Fa_Icon))
                {
                    t.InnerHtml += "<li><a class='accordion-toggle' href='#'><span class='sidebar-title'>" + aplicacion.Nombre + "</span><span class='caret'></span></a>";
                }
                else
                {
                    t.InnerHtml += "<li><a class='accordion-toggle' href='#'><span class='fa " + aplicacion.Fa_Icon + "'></span><span class='sidebar-title'>" + aplicacion.Nombre + "</span><span class='caret'></span></a>";
                }

                t.InnerHtml += "<ul class='nav sub-nav'>";

                foreach (Evaluaciones.Membresia.MenuItem menuItem in itemPadre)
                {
                    List<Evaluaciones.Membresia.MenuItem> items = Evaluaciones.Membresia.MenuItem.GetAll(menuItem);

                    if (items.Any())
                    {
                        t.InnerHtml += string.Format("<li><a class='accordion-toggle menu-open' href = '#'><span class='fa fa fa-arrows-h'></span>{0}<span class='caret'></span></a>", menuItem.Nombre);
                        t.InnerHtml += "<ul class='nav sub-nav'>";
                        t.InnerHtml += MenuExtension.MenuString(menuItem);
                        t.InnerHtml += "</ul></li>";
                    }
                    else
                    {
                        t.InnerHtml += "<li>";
                        t.InnerHtml += string.Format("<li><a href='{0}'>{1}</a>", menuItem.Url, menuItem.Nombre);
                        t.InnerHtml += "</li>";
                    }
                }

                t.InnerHtml += "</ul></li>";
            }

            return new MvcHtmlString(t.ToString(TagRenderMode.Normal));
        }

        private static string MenuString(Evaluaciones.Membresia.MenuItem menuItem)
        {
            string retorno = string.Empty;

            foreach (Evaluaciones.Membresia.MenuItem m in Evaluaciones.Membresia.MenuItem.GetAll(menuItem))
            {
                List<Evaluaciones.Membresia.MenuItem> items = Evaluaciones.Membresia.MenuItem.GetAll(m);

                if (items.Any())
                {
                    retorno += string.Format("<li><a href='{0}'>{1}</a>", m.Url, m.Nombre);
                    retorno += "<ul class='nav sub-nav'>";
                    retorno += MenuExtension.MenuString(m);
                    retorno += "</ul></li>";
                }
                else
                {
                    retorno += string.Format("<li><a href='{0}'>{1}</a>", m.Url, m.Nombre);
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

            List<Evaluaciones.Membresia.MenuItem> lista = Evaluaciones.Membresia.MenuItem.GetAll(Evaluaciones.Membresia.Menu.MenuPrincipal, aplicacion, aplicacion.Inicio);

            if (lista.Any())
            {
                foreach (Evaluaciones.Membresia.MenuItem menuItem in lista)
                {
                    t.InnerHtml += string.Format("<li class='dd-item' data-id='{0}'>", menuItem.Id);

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
            }
            else
            {
                t.InnerHtml += string.Format("<li class='dd-item' data-id='{0}'>", aplicacion.Inicio.Id);

                t.InnerHtml += string.Format("<div class='dd-handle'>{0}</div>", aplicacion.Inicio.Nombre);

                t.InnerHtml += string.Format("<div>{0}</div>", Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(aplicacion.Inicio.Id, aplicacion.Inicio.MenuItemId, TypeButton.Add, controller));
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