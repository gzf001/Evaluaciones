using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Web.UI.Areas.Administracion.Controllers
{
    public class AdminController : Evaluaciones.Web.Controller
    {
        const string Area = "Administracion";

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        [HttpGet]
        public JsonResult Ciudades(string regionCodigo)
        {
            Evaluaciones.Region region = Evaluaciones.Region.Get(short.Parse(regionCodigo));

            List<Evaluaciones.Ciudad> ciudades = Evaluaciones.Ciudad.GetAll(region);

            SelectList selectList = new SelectList(ciudades, "Codigo", "Nombre");

            IEnumerable<SelectListItem> defaultItem = Enumerable.Repeat(new SelectListItem
            {
                Value = "-1",
                Text = "[Seleccione]"
            }, count: 1);

            return this.Json(defaultItem.Concat(selectList), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult Comunas(string regionCodigo, string ciudadCodigo)
        {
            Evaluaciones.Ciudad ciudad = Evaluaciones.Ciudad.Get(short.Parse(regionCodigo), short.Parse(ciudadCodigo));

            List<Evaluaciones.Comuna> comunas = Evaluaciones.Comuna.GetAll(ciudad);

            SelectList selectList = new SelectList(comunas, "Codigo", "Nombre");

            IEnumerable<SelectListItem> defaultItem = Enumerable.Repeat(new SelectListItem
            {
                Value = "-1",
                Text = "[Seleccione]"
            }, count: 1);

            return this.Json(defaultItem.Concat(selectList), JsonRequestBehavior.AllowGet);
        }

        #region Aplicacion

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Access }, Root = "Aplicaciones", Area = Area)]
        public ActionResult Aplicaciones()
        {
            Evaluaciones.Web.UI.Areas.Administracion.Models.Aplicacion model = new Evaluaciones.Web.UI.Areas.Administracion.Models.Aplicacion();

            foreach (Evaluaciones.Membresia.Perfil perfil in Evaluaciones.Membresia.Perfil.GetAll())
            {
                model.Perfiles.Add(new SelectListItem
                {
                    Text = perfil.Nombre,
                    Value = perfil.Codigo.ToString()
                });
            }

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Accept }, Root = "Aplicaciones", Area = Area)]
        public ActionResult Aplicaciones(Evaluaciones.Web.UI.Areas.Administracion.Models.Aplicacion model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            Evaluaciones.Membresia.Aplicacion aplicacion = Evaluaciones.Membresia.Aplicacion.Get(model.Id);

            using (Evaluaciones.Membresia.Context context = new Evaluaciones.Membresia.Context())
            {
                foreach (Evaluaciones.Membresia.Perfil perfil in Evaluaciones.Membresia.Perfil.GetAll())
                {
                    new Evaluaciones.Membresia.AplicacionPerfil
                    {
                        AplicacionId = model.Id,
                        PerfilCodigo = perfil.Codigo
                    }.Delete(context);
                }

                context.SubmitChanges();
            }

            using (Evaluaciones.Membresia.Context context = new Evaluaciones.Membresia.Context())
            {
                new Evaluaciones.Membresia.Aplicacion
                {
                    Id = model.Id,
                    MenuId = aplicacion == null ? default(Guid) : aplicacion.MenuId,
                    MenuItemId = aplicacion == null ? default(Guid) : aplicacion.MenuItemId,
                    Nombre = model.Nombre,
                    Clave = model.Clave,
                    Fa_Icon = model.Fa_Icon,
                    Orden = model.Orden
                }.Save(context);

                foreach (int perfilCodigo in model.SelectedPerfil)
                {
                    new Evaluaciones.Membresia.AplicacionPerfil
                    {
                        AplicacionId = model.Id,
                        PerfilCodigo = perfilCodigo
                    }.Save(context);
                }

                context.SubmitChanges();
            }

            return this.Json("ok 200", JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Add }, Root = "Aplicaciones", Area = Area)]
        public ActionResult AddAplicacion()
        {
            return this.Json(new Evaluaciones.Membresia.Aplicacion(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Edit }, Root = "Aplicaciones", Area = Area)]
        public ActionResult EditAplicacion(Guid id)
        {
            Evaluaciones.Membresia.Aplicacion aplicacion = Evaluaciones.Membresia.Aplicacion.Get(id);

            List<Evaluaciones.Membresia.AplicacionPerfil> aplicacionPerfiles = Evaluaciones.Membresia.AplicacionPerfil.GetAll(aplicacion);

            return this.Json(new Evaluaciones.Web.UI.Areas.Administracion.Models.Aplicacion
            {
                Id = aplicacion.Id,
                Nombre = aplicacion.Nombre,
                Clave = aplicacion.Clave,
                Fa_Icon = aplicacion.Fa_Icon,
                Orden = aplicacion.Orden,
                SelectedPerfil = aplicacionPerfiles.Any() ? aplicacionPerfiles.Select<Evaluaciones.Membresia.AplicacionPerfil, int>(x => x.PerfilCodigo).ToList<int>() : null
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetAplicaciones()
        {
            Evaluaciones.Web.UI.Areas.Administracion.Models.Aplicacion.Aplicaciones aplicaciones = new Evaluaciones.Web.UI.Areas.Administracion.Models.Aplicacion.Aplicaciones();

            aplicaciones.data = new List<Evaluaciones.Web.UI.Areas.Administracion.Models.Aplicacion>();

            foreach (Evaluaciones.Membresia.Aplicacion aplicacion in Evaluaciones.Membresia.Aplicacion.GetAll())
            {
                aplicaciones.data.Add(new Evaluaciones.Web.UI.Areas.Administracion.Models.Aplicacion
                {
                    Id = aplicacion.Id,
                    Nombre = aplicacion.Nombre,
                    Clave = aplicacion.Clave,
                    Orden = aplicacion.Orden,
                    Accion = string.Format("{0}{1}", Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(aplicacion.Id, null, Evaluaciones.Helpers.TypeButton.Edit, this),
                                                     Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(aplicacion.Id, null, Evaluaciones.Helpers.TypeButton.Delete, this))
                });
            }

            return this.Json(aplicaciones, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Delete }, Root = "Aplicaciones", Area = Area)]
        public JsonResult DeleteAplicacion(Guid id)
        {
            Evaluaciones.Membresia.Aplicacion aplicacion = Evaluaciones.Membresia.Aplicacion.Get(id);

            using (Evaluaciones.Membresia.Context context = new Evaluaciones.Membresia.Context())
            {
                new Evaluaciones.Membresia.Aplicacion
                {
                    Id = aplicacion.Id,
                    MenuId = aplicacion.MenuId,
                    MenuItemId = aplicacion.MenuItemId,
                    Nombre = aplicacion.Nombre,
                    Clave = aplicacion.Clave,
                    Fa_Icon = aplicacion.Fa_Icon,
                    Orden = aplicacion.Orden
                }.Delete(context);

                context.SubmitChanges();
            }

            return this.Json("200 ok", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Ítems de menú

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Access }, Root = "ItemsMenu", Area = Area)]
        public ActionResult ItemsMenu()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ItemsMenu(Evaluaciones.Web.UI.Areas.Administracion.Models.MenuItem model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            try
            {
                Evaluaciones.Membresia.MenuItem menuItem = Evaluaciones.Membresia.MenuItem.Get(model.AplicacionId, Evaluaciones.Membresia.Menu.MenuPrincipal.Id, model.Id);

                using (Evaluaciones.Membresia.Context context = new Evaluaciones.Membresia.Context())
                {
                    Evaluaciones.Membresia.MenuItem m = new Evaluaciones.Membresia.MenuItem
                    {
                        AplicacionId = model.AplicacionId,
                        MenuId = Evaluaciones.Membresia.Menu.MenuPrincipal.Id,
                        Id = model.Id,
                        MenuItemId = model.MenuItemId,
                        Nombre = model.Nombre,
                        Informacion = model.Informacion,
                        Icono = model.Icono,
                        Url = model.Url,
                        Visible = model.Visible,
                        MuestraMenus = model.MuestraMenus
                    };

                    if (menuItem == null)
                    {
                        menuItem = Evaluaciones.Membresia.MenuItem.Get(model.AplicacionId, Evaluaciones.Membresia.Menu.MenuPrincipal.Id, model.MenuItemId.Value);

                        m.Orden = Evaluaciones.Membresia.MenuItem.Last(menuItem);
                    }
                    else
                    {
                        m.Orden = menuItem.Orden;
                    }

                    m.Save(context);

                    foreach (Evaluaciones.Membresia.Accion accion in Evaluaciones.Membresia.Accion.GetAll())
                    {
                        new Evaluaciones.Membresia.MenuItemAccion
                        {
                            AplicacionId = model.AplicacionId,
                            MenuId = Evaluaciones.Membresia.Menu.MenuPrincipal.Id,
                            MenuItemId = model.Id,
                            AccionCodigo = accion.Codigo
                        }.Save(context);
                    }

                    context.SubmitChanges();
                }

                return this.View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Edit }, Root = "ItemsMenu", Area = Area)]
        public JsonResult EditItemMenu(Guid aplicacionId, Guid itemId)
        {
            Evaluaciones.Membresia.MenuItem menuItem = Evaluaciones.Membresia.MenuItem.Get(aplicacionId, Evaluaciones.Membresia.Menu.MenuPrincipal.Id, itemId);

            Evaluaciones.Web.UI.Areas.Administracion.Models.MenuItem m = new Evaluaciones.Web.UI.Areas.Administracion.Models.MenuItem
            {
                Nombre = menuItem.Nombre,
                Informacion = menuItem.Informacion,
                Url = menuItem.Url,
                Visible = menuItem.Visible
            };

            return this.Json(m, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult DeleteItemsMenu(Guid aplicacionId, Guid itemId)
        {
            try
            {
                Evaluaciones.Membresia.MenuItem menuItem = Evaluaciones.Membresia.MenuItem.Get(aplicacionId, Evaluaciones.Membresia.Menu.MenuPrincipal.Id, itemId);

                using (Evaluaciones.Membresia.Context context = new Evaluaciones.Membresia.Context())
                {
                    foreach (Evaluaciones.Membresia.MenuItem m in Evaluaciones.Membresia.MenuItem.GetAll(menuItem))
                    {
                        new Evaluaciones.Membresia.MenuItem
                        {
                            AplicacionId = m.AplicacionId,
                            MenuId = m.MenuId,
                            Id = m.Id,
                            MenuItemId = m.MenuItemId,
                            Nombre = m.Nombre,
                            Informacion = m.Informacion,
                            Icono = m.Icono,
                            Url = m.Url,
                            Visible = m.Visible,
                            MuestraMenus = m.MuestraMenus
                        }.Delete(context);

                        context.SubmitChanges();
                    }

                    new Evaluaciones.Membresia.MenuItem
                    {
                        AplicacionId = menuItem.AplicacionId,
                        MenuId = menuItem.MenuId,
                        Id = menuItem.Id,
                        MenuItemId = menuItem.MenuItemId,
                        Nombre = menuItem.Nombre,
                        Informacion = menuItem.Informacion,
                        Icono = menuItem.Icono,
                        Url = menuItem.Url,
                        Visible = menuItem.Visible,
                        MuestraMenus = menuItem.MuestraMenus
                    }.Delete(context);

                    context.SubmitChanges();
                }

                return this.Json("200 ok", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetOrder(string data)
        {
            Evaluaciones.Membresia.MenuItem.OrderMenu(data);

            return this.Json("200 ok", JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetItemsMenu(Guid aplicacionId)
        {
            Evaluaciones.Membresia.Aplicacion aplicacion = Evaluaciones.Membresia.Aplicacion.Get(aplicacionId);

            return this.Json(Evaluaciones.Helpers.MenuExtension.MenuOrderable(null, aplicacion, this).ToString(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Roles

        [Authorize]
        [HttpGet]
        public ActionResult Roles()
        {

            return this.View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Roles(Evaluaciones.Web.UI.Areas.Administracion.Models.Rol model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            Evaluaciones.Membresia.Rol rol = Evaluaciones.Membresia.Rol.Get(model.Id);

            using (Evaluaciones.Membresia.Context context = new Evaluaciones.Membresia.Context())
            {
                new Evaluaciones.Membresia.Rol
                {
                    Id = model.Id,
                    AmbitoCodigo = model.AmbitoCodigo,
                    Nombre = model.Nombre.Trim(),
                    Clave = string.IsNullOrEmpty(model.Clave) ? default(string) : model.Clave.Trim()
                }.Save(context);

                context.SubmitChanges();
            }

            return this.Json("200 ok", JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetRoles(int ambitoCodigo)
        {
            Evaluaciones.Ambito ambito = Evaluaciones.Ambito.Get(ambitoCodigo);

            Evaluaciones.Web.UI.Areas.Administracion.Models.Rol.Roles rol = new Evaluaciones.Web.UI.Areas.Administracion.Models.Rol.Roles();

            rol.data = new List<Evaluaciones.Web.UI.Areas.Administracion.Models.Rol>();

            foreach (Evaluaciones.Membresia.Rol r in Evaluaciones.Membresia.Rol.GetAll(ambito, true))
            {
                rol.data.Add(new Evaluaciones.Web.UI.Areas.Administracion.Models.Rol
                {
                    Id = r.Id,
                    AmbitoCodigo = r.AmbitoCodigo,
                    Nombre = r.Nombre,
                    Clave = r.Clave,
                    Accion = string.Format("{0}{1}{2}", Evaluaciones.Helpers.ActionLinkExtension.ActionLink(null, null, string.Format("GetPermissions?rolId={0}", r.Id), "Admin", "Administracion", Evaluaciones.Helpers.TypeButton.Accept, r.Id, "btn btn-success btn-xs btn-flat", "fa-legal", "Configurar permisos"),
                                                        Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(r.Id, null, Evaluaciones.Helpers.TypeButton.Edit, this),
                                                        Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(r.Id, null, Evaluaciones.Helpers.TypeButton.Delete, this))
                });
            }

            return this.Json(rol, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetRol(Guid id)
        {
            Evaluaciones.Membresia.Rol rol = Evaluaciones.Membresia.Rol.Get(id);

            return this.Json(new Evaluaciones.Web.UI.Areas.Administracion.Models.Rol
            {
                Id = rol.Id,
                Nombre = rol.Nombre,
                Clave = rol.Clave
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult DeleteRol(Guid id)
        {
            Evaluaciones.Membresia.Rol rol = Evaluaciones.Membresia.Rol.Get(id);

            using (Evaluaciones.Membresia.Context context = new Evaluaciones.Membresia.Context())
            {
                new Evaluaciones.Membresia.Rol
                {
                    Id = rol.Id,
                    AmbitoCodigo = rol.AmbitoCodigo,
                    Nombre = rol.Nombre,
                    Clave = rol.Clave,
                }.Delete(context);

                context.SubmitChanges();
            }

            return this.Json("200 ok", JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetPermissions(Guid rolId)
        {
            Evaluaciones.Membresia.Rol rol = Evaluaciones.Membresia.Rol.Get(rolId);

            return this.View(new Evaluaciones.Web.UI.Areas.Administracion.Models.RolAccion
            {
                RolId = rol.Id,
                Rol = rol,
                AplicacionId = default(Guid),
                MenuId = Evaluaciones.Membresia.Menu.MenuPrincipal.Id,
                MenuItemId = default(Guid),
                AccionCodigo = default(int)
            });
        }

        [Authorize]
        [HttpPost]
        //[Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Accept }, Root = "Roles")]
        public ActionResult GetPermissions(List<Evaluaciones.Web.UI.Areas.Administracion.Models.RolAccion> model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            Evaluaciones.Membresia.Aplicacion aplicacion = Evaluaciones.Membresia.Aplicacion.Get(model.First<Evaluaciones.Web.UI.Areas.Administracion.Models.RolAccion>().AplicacionId);

            Evaluaciones.Membresia.Rol rol = Evaluaciones.Membresia.Rol.Get(model.First<Evaluaciones.Web.UI.Areas.Administracion.Models.RolAccion>().RolId);

            Evaluaciones.Membresia.Menu menu = Evaluaciones.Membresia.Menu.MenuPrincipal;

            using (Evaluaciones.Membresia.Context context = new Evaluaciones.Membresia.Context())
            {
                foreach (Evaluaciones.Membresia.RolAccion rolAccion in Evaluaciones.Membresia.RolAccion.GetAll(aplicacion, rol))
                {
                    new Evaluaciones.Membresia.RolAccion
                    {
                        RolId = rolAccion.RolId,
                        AplicacionId = rolAccion.AplicacionId,
                        MenuId = rolAccion.MenuId,
                        MenuItemId = rolAccion.MenuItemId,
                        AccionCodigo = rolAccion.AccionCodigo
                    }.Delete(context);
                }

                context.SubmitChanges();
            }

            using (Evaluaciones.Membresia.Context context = new Evaluaciones.Membresia.Context())
            {
                foreach (Evaluaciones.Web.UI.Areas.Administracion.Models.RolAccion rolAccion in model.Where<Evaluaciones.Web.UI.Areas.Administracion.Models.RolAccion>(x => x.AccionCodigo != default(int)))
                {
                    new Evaluaciones.Membresia.RolAccion
                    {
                        RolId = rolAccion.RolId,
                        AplicacionId = rolAccion.AplicacionId,
                        MenuId = menu.Id,
                        MenuItemId = rolAccion.MenuItemId,
                        AccionCodigo = rolAccion.AccionCodigo
                    }.Save(context);
                }

                context.SubmitChanges();
            }

            return this.Json("200 ok", JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetRolAccion(Guid rolId, string aplicacionId)
        {
            Guid apId;

            Evaluaciones.Web.UI.Areas.Administracion.Models.RolAccion.RolAcciones rolAcciones = new Evaluaciones.Web.UI.Areas.Administracion.Models.RolAccion.RolAcciones();

            if (Guid.TryParse(aplicacionId, out apId))
            {
                Evaluaciones.Membresia.Rol rol = Evaluaciones.Membresia.Rol.Get(rolId);

                Evaluaciones.Membresia.Aplicacion aplicacion = Evaluaciones.Membresia.Aplicacion.Get(apId);

                List<Evaluaciones.Membresia.Accion> acciones = Evaluaciones.Membresia.Accion.GetAll();

                foreach (Evaluaciones.Membresia.MenuItem menuItem in Evaluaciones.Membresia.MenuItem.GetAll(Evaluaciones.Membresia.Menu.MenuPrincipal, aplicacion).FindAll(x => !x.Equals(aplicacion.Inicio)))
                {
                    Evaluaciones.Web.UI.Areas.Administracion.Models.RolAccion model = new Evaluaciones.Web.UI.Areas.Administracion.Models.RolAccion
                    {
                        RolId = rol.Id,
                        AplicacionId = aplicacion.Id,
                        MenuId = menuItem.MenuId,
                        MenuItemId = menuItem.Id,
                        MenuItemNombre = menuItem.Nombre
                    };

                    if (!menuItem.RootNode)
                    {
                        foreach (Evaluaciones.Membresia.Accion accion in acciones)
                        {
                            bool exists = Evaluaciones.Membresia.RolAccion.Exists(rol, menuItem, accion);

                            model.AccionNombre += string.Format("<label class='checkbox-inline'><input type='checkbox' {0} name='Accion' class='icheck' data-parent={1} data-value={2}>{3}</label>", exists ? "checked= '{0}'" : string.Empty, menuItem.Id, accion.Codigo, accion.Nombre);

                            model.Acciones.Add(new SelectListItem
                            {
                                Text = accion.Nombre,
                                Value = accion.Codigo.ToString()
                            });
                        }
                    }

                    rolAcciones.data.Add(model);
                }

                return this.Json(rolAcciones, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(rolAcciones, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}