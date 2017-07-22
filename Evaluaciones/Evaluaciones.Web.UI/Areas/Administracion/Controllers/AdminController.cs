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
        public JsonResult GetRoles()
        {
            Evaluaciones.Web.UI.Areas.Administracion.Models.Rol.Roles rol = new Evaluaciones.Web.UI.Areas.Administracion.Models.Rol.Roles();

            rol.data = new List<Evaluaciones.Web.UI.Areas.Administracion.Models.Rol>();

            foreach (Evaluaciones.Membresia.Rol r in Evaluaciones.Membresia.Rol.GetAll())
            {
                rol.data.Add(new Evaluaciones.Web.UI.Areas.Administracion.Models.Rol
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Clave = r.Clave,
                    Accion = string.Format("{0}{1}{2}", Evaluaciones.Helpers.ActionLinkExtension.ActionLink(null, null, string.Format("GetPermissions/{0}", r.Id), "Admin", "Administracion", Evaluaciones.Helpers.TypeButton.Accept, r.Id, "btn btn-success btn-xs btn-flat", "fa-legal", "Configurar permisos", null, this),
                                                        Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(r.Id, null, Evaluaciones.Helpers.TypeButton.Edit, this),
                                                        Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(r.Id, null, Evaluaciones.Helpers.TypeButton.Delete, this))
                });
            }

            return this.Json(rol, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Add }, Root = "Roles", Area = Area)]
        public ActionResult AddRol(int ambitoCodigo)
        {
            Evaluaciones.Ambito ambito = Evaluaciones.Ambito.Get(ambitoCodigo);

            if (ambito == null)
            {
                throw new Exception("Código de ámbito no válido");
            }

            return this.Json(new Evaluaciones.Web.UI.Areas.Administracion.Models.Rol(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult EditRol(Guid id)
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
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Accept }, Root = "Roles", Area = Area)]
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
                foreach (Evaluaciones.Membresia.RolAccion rolAccion in Evaluaciones.Membresia.RolAccion.GetAll(aplicacion, rol).Where<Evaluaciones.Membresia.RolAccion>(x => !x.MenuItem.Equals(Evaluaciones.Membresia.MenuItem.RolPermiso)))
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
                    context.SubmitChanges();

                }
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

                    model.AccionNombre += "<div class='option-group field'>";

                    if (!menuItem.RootNode)
                    {
                        foreach (Evaluaciones.Membresia.Accion accion in acciones)
                        {
                            bool exists = Evaluaciones.Membresia.RolAccion.Exists(rol, menuItem, accion);

                            model.AccionNombre += string.Format("<label class='option'><input type='checkbox' {0} name='Accion' data-parent={1} data-value={2}><span class='checkbox'></span>{3}</label></label>", exists ? "checked= '{0}'" : string.Empty, menuItem.Id, accion.Codigo, accion.Nombre);

                            model.Acciones.Add(new SelectListItem
                            {
                                Text = accion.Nombre,
                                Value = accion.Codigo.ToString()
                            });
                        }
                    }

                    model.AccionNombre += "</div>";

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

        #region Usuarios

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Access }, Root = "Usuarios", Area = Area)]
        public ActionResult Usuarios()
        {
            Evaluaciones.Web.UI.Areas.Administracion.Models.Usuario usuario = new Evaluaciones.Web.UI.Areas.Administracion.Models.Usuario();

            usuario.FindType = Evaluaciones.FindType.Equals;

            return this.View(usuario);
        }

        [Authorize]
        [HttpPost]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Accept }, Root = "Usuarios", Area = Area)]
        public ActionResult Usuarios(Evaluaciones.Web.UI.Areas.Administracion.Models.Usuario model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                Evaluaciones.Membresia.Usuario usuario = Evaluaciones.Membresia.Usuario.Get(model.Id);

                string textoRun = model.Persona.Run.Replace(".", string.Empty).Replace("-", string.Empty);

                int runCuerpo = int.Parse(textoRun.Substring(0, textoRun.Length - 1));
                char runDigito = char.Parse(textoRun.Replace(runCuerpo.ToString(), string.Empty));

                using (Evaluaciones.Membresia.Context context = new Evaluaciones.Membresia.Context())
                {
                    Evaluaciones.Persona persona = new Evaluaciones.Persona
                    {
                        Id = model.Id,
                        RunCuerpo = runCuerpo,
                        RunDigito = runDigito,
                        Nombres = model.Persona.Nombres,
                        ApellidoPaterno = model.Persona.ApellidoPaterno,
                        ApellidoMaterno = model.Persona.ApellidoMaterno,
                        Email = model.Persona.Email,
                        SexoCodigo = model.Persona.SexoCodigo,
                        FechaNacimiento = model.Persona.FechaNacimiento,
                        NacionalidadCodigo = model.Persona.NacionalidadCodigo,
                        EstadoCivilCodigo = model.Persona.EstadoCivilCodigo,
                        NivelEducacionalCodigo = model.Persona.NivelEducacionalCodigo,
                        RegionCodigo = model.Persona.ComunaCodigo.Value > 0 ? model.Persona.RegionCodigo.Value : default(short),
                        CiudadCodigo = model.Persona.ComunaCodigo.Value > 0 ? model.Persona.CiudadCodigo.Value : default(short),
                        ComunaCodigo = model.Persona.ComunaCodigo.Value > 0 ? model.Persona.ComunaCodigo.Value : default(short),
                        VillaPoblacion = model.Persona.VillaPoblacion,
                        Direccion = model.Persona.Direccion,
                        Telefono = model.Persona.Telefono,
                        Celular = model.Persona.Celular,
                        Observaciones = default(string)
                    };

                    persona.Save(context);

                    context.SubmitChanges();

                    if (usuario == null)
                    {
                        Evaluaciones.Membresia.Account.RegisterLogin(persona);
                    }
                    else
                    {
                        new Evaluaciones.Membresia.Usuario
                        {
                            Id = usuario.Id,
                            Password = usuario.Password,
                            Aprobado = usuario.Aprobado,
                            Bloqueado = usuario.Bloqueado,
                            Creacion = usuario.Creacion,
                            UltimaActividad = usuario.UltimaActividad,
                            UltimoAcceso = usuario.UltimoAcceso,
                            UltimoCambioPassword = usuario.UltimoCambioPassword,
                            UltimoDesbloqueo = usuario.UltimoDesbloqueo,
                            NumeroIntentosFallidos = usuario.NumeroIntentosFallidos,
                            FechaIntentoFallido = usuario.FechaIntentoFallido
                        }.Save(context);

                        context.SubmitChanges();
                    }
                }

                return this.Json("200", JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("RunCuerpoIndex"))
                {
                    return this.Json("El R.U.N. se encuentra registrado con otro usuario", JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return this.Json(ex.Message, JsonRequestBehavior.DenyGet);
                }
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetAllUsuarios(Evaluaciones.FindType findType, string filter)
        {
            Evaluaciones.Web.UI.Areas.Administracion.Models.Usuario.Usuarios usuarios = this.UsuarioGridView(findType, filter);

            return this.Json(usuarios, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Access }, Root = "Usuarios", Area = Area)]
        public JsonResult GetUsuarios(string run)
        {
            string textoRun = run.Replace(".", string.Empty).Replace("-", string.Empty);

            int runCuerpo = int.Parse(textoRun.Substring(0, textoRun.Length - 1));
            char runDigito = char.Parse(textoRun.Replace(runCuerpo.ToString(), string.Empty));

            if (!Evaluaciones.Helper.ValidaRun(runCuerpo, runDigito))
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }

            Evaluaciones.Web.UI.Areas.Administracion.Models.Usuario.Usuarios usuarios = this.UsuarioGridView(null, null, runCuerpo, runDigito);

            return this.Json(usuarios, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Add }, Root = "Usuarios", Area = Area)]
        public JsonResult AddUsuario()
        {
            return this.Json(new Evaluaciones.Web.UI.Areas.Administracion.Models.Usuario
            {
                Id = Guid.NewGuid(),
                Password = string.Empty,
                Aprobado = true,
                Bloqueado = true,
                Creacion = DateTime.Now,
                UltimaActividad = DateTime.Now,
                UltimoAcceso = DateTime.Now,
                UltimoCambioPassword = default(DateTime),
                UltimoDesbloqueo = default(DateTime),
                NumeroIntentosFallidos = default(int),
                FechaIntentoFallido = default(DateTime),
                FechaNacimientoString = string.Empty,
                Persona = new Evaluaciones.Persona()
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Edit }, Root = "Usuarios", Area = Area)]
        public JsonResult EditUsuario(Guid id)
        {
            Evaluaciones.Membresia.Usuario usuario = Evaluaciones.Membresia.Usuario.Get(id);

            return this.Json(new Evaluaciones.Web.UI.Areas.Administracion.Models.Usuario
            {
                Id = usuario.Id,
                Password = string.Empty,
                Aprobado = usuario.Aprobado,
                Bloqueado = usuario.Bloqueado,
                Creacion = usuario.Creacion,
                UltimaActividad = usuario.UltimaActividad,
                UltimoAcceso = usuario.UltimoAcceso,
                UltimoCambioPassword = usuario.UltimoCambioPassword,
                UltimoDesbloqueo = usuario.UltimoDesbloqueo,
                NumeroIntentosFallidos = usuario.NumeroIntentosFallidos,
                FechaIntentoFallido = usuario.FechaIntentoFallido,
                FechaNacimientoString = usuario.Persona.FechaNacimiento.HasValue ? usuario.Persona.FechaNacimiento.Value.ToShortDateString() : string.Empty,
                Persona = usuario.Persona
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Delete }, Root = "Usuarios", Area = Area)]
        public JsonResult DeleteUsuario(Guid id)
        {
            try
            {
                Evaluaciones.Membresia.Usuario usuario = Evaluaciones.Membresia.Usuario.Get(id);

                using (Evaluaciones.Membresia.Context context = new Evaluaciones.Membresia.Context())
                {
                    new Evaluaciones.Membresia.Usuario
                    {
                        Id = usuario.Id,
                        Password = usuario.Password,
                        Aprobado = usuario.Aprobado,
                        Bloqueado = usuario.Bloqueado,
                        Creacion = usuario.Creacion,
                        UltimaActividad = usuario.UltimaActividad,
                        UltimoAcceso = usuario.UltimoAcceso,
                        UltimoCambioPassword = usuario.UltimoCambioPassword,
                        UltimoDesbloqueo = usuario.UltimoDesbloqueo,
                        NumeroIntentosFallidos = usuario.NumeroIntentosFallidos,
                        FechaIntentoFallido = usuario.FechaIntentoFallido
                    }.Delete(context);

                    context.SubmitChanges();
                }

                return this.Json("200", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Access }, Root = "Usuarios", Area = Area)]
        public JsonResult Usuario(string run)
        {
            string textoRun = run.Replace(".", string.Empty).Replace("-", string.Empty);

            int runCuerpo = int.Parse(textoRun.Substring(0, textoRun.Length - 1));
            char runDigito = char.Parse(textoRun.Replace(runCuerpo.ToString(), string.Empty));

            if (!Evaluaciones.Helper.ValidaRun(runCuerpo, runDigito))
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }

            Evaluaciones.Membresia.Usuario usuario = Evaluaciones.Membresia.Usuario.Get(runCuerpo, runDigito);

            if (usuario == null)
            {
                Evaluaciones.Persona persona = Evaluaciones.Persona.Get(runCuerpo, runDigito);

                if (persona == null)
                {
                    persona = new Evaluaciones.Persona();

                    persona.RunCuerpo = runCuerpo;
                    persona.RunDigito = runDigito;
                    persona.Run = string.Format("{0}-{1}", runCuerpo.ToString("N0"), runDigito);
                }

                usuario = new Evaluaciones.Web.UI.Areas.Administracion.Models.Usuario
                {
                    FechaNacimientoString = persona.FechaNacimiento.HasValue ? persona.FechaNacimiento.Value.ToShortDateString() : string.Empty,
                    Persona = persona
                };

                return this.Json(usuario, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new Evaluaciones.Web.UI.Areas.Administracion.Models.Usuario
            {
                Id = usuario.Id,
                Aprobado = usuario.Aprobado,
                Bloqueado = usuario.Bloqueado,
                Creacion = usuario.Creacion,
                UltimaActividad = usuario.UltimaActividad,
                UltimoAcceso = usuario.UltimoAcceso,
                UltimoCambioPassword = usuario.UltimoCambioPassword,
                UltimoDesbloqueo = usuario.UltimoDesbloqueo,
                NumeroIntentosFallidos = usuario.NumeroIntentosFallidos,
                FechaIntentoFallido = usuario.FechaIntentoFallido,
                FechaNacimientoString = usuario.Persona.FechaNacimiento.HasValue ? usuario.Persona.FechaNacimiento.Value.ToShortDateString() : string.Empty,
                Persona = usuario.Persona
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Accept }, Root = "Usuarios", Area = Area)]
        public ActionResult ChangePass(Evaluaciones.Web.UI.Areas.Administracion.Models.Usuario model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                Evaluaciones.Membresia.Usuario usuario = Evaluaciones.Membresia.Usuario.Get(model.Id);

                Evaluaciones.Membresia.Account.DoChangePassword(usuario, model.ChangePass.Password1, model.ChangePass.Password2);

                return this.Json("200", JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                return this.Json(ex.Message, JsonRequestBehavior.DenyGet);
            }
        }

        [Authorize]
        [HttpGet]
        public JsonResult UsuarioRol(Guid personaId, int ambitoCodigo, Guid? empresaId, Guid? centroCostoId)
        {
            Evaluaciones.Persona persona = Evaluaciones.Persona.Get(personaId);

            Evaluaciones.Empresa empresa = new Evaluaciones.Empresa();

            Evaluaciones.CentroCosto centroCosto = new Evaluaciones.CentroCosto();

            Evaluaciones.Ambito ambito = Evaluaciones.Ambito.Get(ambitoCodigo);

            if (empresaId.HasValue)
            {
                empresa = Evaluaciones.Empresa.Get(empresaId.Value);
            }
            else
            {
                empresa = null;
            }

            if (centroCostoId.HasValue)
            {
                centroCosto = Evaluaciones.CentroCosto.Get(empresaId.Value, centroCostoId.Value);
            }
            else
            {
                centroCosto = null;
            }

            Evaluaciones.Web.UI.Areas.Administracion.Models.Rol.Roles rol = new Evaluaciones.Web.UI.Areas.Administracion.Models.Rol.Roles();

            rol.data = new List<Evaluaciones.Web.UI.Areas.Administracion.Models.Rol>();

            foreach (Evaluaciones.Membresia.Rol r in Evaluaciones.Membresia.Rol.GetAll(ambito, false))
            {
                bool exists = false;

                if (ambito.Equals(Evaluaciones.Ambito.Aplicacion))
                {
                    exists = Evaluaciones.Membresia.RolPersona.Exists(persona, r);
                }
                else if (ambito.Equals(Evaluaciones.Ambito.Empresa))
                {
                    exists = Evaluaciones.Membresia.RolPersonaEmpresa.Exists(r, persona, empresa);
                }
                else
                {
                    exists = Evaluaciones.Membresia.RolPersonaCentroCosto.Exists(r, persona, centroCosto);
                }

                rol.data.Add(new Evaluaciones.Web.UI.Areas.Administracion.Models.Rol
                {
                    Nombre = r.Nombre,
                    Accion = string.Format("<label class='option'><input type='checkbox' {0} name='asignacion' data-value={1}><span class='checkbox'></span></label>", exists ? "checked" : string.Empty, r.Id)
                });
            }

            return this.Json(rol, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult UsuarioRol(List<Evaluaciones.Web.UI.Areas.Administracion.Models.Usuario.RolPersona> model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json("501", JsonRequestBehavior.AllowGet);
            }

            List<Evaluaciones.Membresia.Account.Rol> roles = (from m in model
                                                              select new Evaluaciones.Membresia.Account.Rol
                                                              {
                                                                  PersonaId = m.PersonaId,
                                                                  AmbitoCodigo = m.AmbitoCodigo,
                                                                  RolId = m.RolId,
                                                                  EmpresaId = m.EmpresaId,
                                                                  CentroCostoId = m.CentroCostoId
                                                              }).ToList<Evaluaciones.Membresia.Account.Rol>();

            return this.Json(Evaluaciones.Membresia.Account.Rol.Asignar(roles), JsonRequestBehavior.DenyGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult DeshabilitarUsuario(Guid usuarioId)
        {
            Evaluaciones.Membresia.Usuario usuario = Evaluaciones.Membresia.Usuario.Get(usuarioId);

            Evaluaciones.Membresia.Account.Lock(usuario);

            return this.Json("200", JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult HabilitarUsuario(Guid usuarioId)
        {
            Evaluaciones.Membresia.Usuario usuario = Evaluaciones.Membresia.Usuario.Get(usuarioId);

            Evaluaciones.Membresia.Account.UnLock(usuario);

            return this.Json("200", JsonRequestBehavior.AllowGet);
        }

        private Evaluaciones.Web.UI.Areas.Administracion.Models.Usuario.Usuarios UsuarioGridView(Evaluaciones.FindType? findType, string filter, int? runCuerpo = null, char? runDigito = null)
        {
            Evaluaciones.Web.UI.Areas.Administracion.Models.Usuario.Usuarios usuarios = new Evaluaciones.Web.UI.Areas.Administracion.Models.Usuario.Usuarios();

            usuarios.data = new List<Evaluaciones.Web.UI.Areas.Administracion.Models.Usuario>();

            List<Evaluaciones.Membresia.Usuario> lista = new List<Evaluaciones.Membresia.Usuario>();

            if (runCuerpo.HasValue && runDigito.HasValue)
            {
                Evaluaciones.Membresia.Usuario usuario = Evaluaciones.Membresia.Usuario.Get(runCuerpo.Value, runDigito.Value);

                if (usuario != null)
                {
                    lista.Add(usuario);
                }
            }
            else
            {
                lista = Evaluaciones.Membresia.Usuario.GetAll(findType.Value, filter);
            }

            foreach (Evaluaciones.Membresia.Usuario usuario in lista)
            {
                MvcHtmlString botonHabilitado;

                if (usuario.Bloqueado)
                {
                    botonHabilitado = Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(usuario.Id, null, Evaluaciones.Helpers.TypeButton.OtherAction, this, "fa-unlock", "Habilitar cuenta", "enableAccount");
                }
                else
                {
                    botonHabilitado = Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(usuario.Id, null, Evaluaciones.Helpers.TypeButton.OtherAction, this, "fa-lock", "Deshabilitar cuenta", "disableAccount");
                }

                usuarios.data.Add(new Evaluaciones.Web.UI.Areas.Administracion.Models.Usuario
                {
                    Nombre = usuario.Persona.Nombre,
                    Run = usuario.Persona.Run,
                    Estado = usuario.Bloqueado ? "Bloquedo" : "Activo",
                    UltimoLogin = usuario.UltimoAcceso.ToString(),
                    Accion = string.Format("{0}{1}{2}{3}", Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(usuario.Id, null, Evaluaciones.Helpers.TypeButton.Edit, this),
                                                              Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(usuario.Id, null, Evaluaciones.Helpers.TypeButton.OtherAction, this, "fa-key", "Establecer contraseña", "changePass"),
                                                              Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(usuario.Id, null, Evaluaciones.Helpers.TypeButton.OtherAction, this, "fa-wrench", "Asignar roles", "assignRole"),
                                                              botonHabilitado)
                });
            }

            return usuarios;
        }

        #endregion

        #region Usuarios conectados

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Access }, Root = "UsuarioConectados", Area = Area)]
        public ActionResult UsuarioConectados()
        {
            Evaluaciones.Web.UI.Areas.Administracion.Models.UsuariosConectados usuariosConectados = new Evaluaciones.Web.UI.Areas.Administracion.Models.UsuariosConectados();

            usuariosConectados.NumeroUsuarios = (int)System.Web.HttpContext.Current.Application["NumberUsers"];

            return this.View(usuariosConectados);
        }

        [Authorize]
        [HttpGet]
        public JsonResult CountUsuarioConectados()
        {
            return this.Json((int)System.Web.HttpContext.Current.Application["NumberUsers"], JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Empresas

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Access }, Root = "Empresas", Area = Area)]
        public ActionResult Empresas()
        {
            Evaluaciones.Web.UI.Areas.Administracion.Models.Empresa empresa = new Evaluaciones.Web.UI.Areas.Administracion.Models.Empresa();

            empresa.FindType = Evaluaciones.FindType.Equals;

            return this.View(empresa);
        }

        [Authorize]
        [HttpPost]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Accept }, Root = "Empresas", Area = Area)]
        public ActionResult Empresas(Evaluaciones.Web.UI.Areas.Administracion.Models.Empresa model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                string textoRun = model.Rut.Replace(".", string.Empty).Replace("-", string.Empty);

                int rutCuerpo = int.Parse(textoRun.Substring(0, textoRun.Length - 1));
                char rutDigito = char.Parse(textoRun.Replace(rutCuerpo.ToString(), string.Empty));

                using (Evaluaciones.Context context = new Evaluaciones.Context())
                {
                    new Evaluaciones.Empresa
                    {
                        Id = model.Id,
                        RutCuerpo = rutCuerpo,
                        RutDigito = rutDigito,
                        RazonSocial = model.RazonSocial,
                        RegionCodigo = model.RegionCodigo,
                        CiudadCodigo = model.CiudadCodigo,
                        ComunaCodigo = model.ComunaCodigo,
                        Direccion = model.Direccion,
                        Email = model.Email,
                        PaginaWeb = model.PaginaWeb,
                        Telefono1 = model.Telefono1,
                        Telefono2 = model.Telefono2,
                        Fax = model.Fax,
                        Celular = model.Celular,
                        Bloqueada = model.Bloqueada
                    }.Save(context);

                    context.SubmitChanges();
                }

                return this.Json("200", JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("RutCuerpoIndex"))
                {
                    return this.Json("El R.U.T. se encuentra registrado con otra empresa", JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return this.Json(ex.Message, JsonRequestBehavior.DenyGet);
                }
            }
        }

        [Authorize]
        [HttpGet]
        //[Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Access }, Root = "Empresas", Area = Area)]
        public JsonResult GetAllEmpresas(Evaluaciones.FindType findType, string filter)
        {
            Evaluaciones.Web.UI.Areas.Administracion.Models.Empresa.Empresas empresas = this.EmpresaGridView(findType, filter);

            return this.Json(empresas, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Access }, Root = "Empresas", Area = Area)]
        public JsonResult GetEmpresas(string rut)
        {
            string textoRun = rut.Replace(".", string.Empty).Replace("-", string.Empty);

            int rutCuerpo = int.Parse(textoRun.Substring(0, textoRun.Length - 1));
            char rutDigito = char.Parse(textoRun.Replace(rutCuerpo.ToString(), string.Empty));

            if (!Evaluaciones.Helper.ValidaRun(rutCuerpo, rutDigito))
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }

            Evaluaciones.Web.UI.Areas.Administracion.Models.Empresa.Empresas empresas = this.EmpresaGridView(null, null, rutCuerpo, rutDigito);

            return this.Json(empresas, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Add }, Root = "Empresas", Area = Area)]
        public JsonResult AddEmpresa()
        {
            return this.Json(new Evaluaciones.Web.UI.Areas.Administracion.Models.Empresa(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Edit }, Root = "Empresas", Area = Area)]
        public JsonResult EditEmpresa(Guid id)
        {
            Evaluaciones.Empresa empresa = Evaluaciones.Empresa.Get(id);

            return this.Json(new Evaluaciones.Web.UI.Areas.Administracion.Models.Empresa
            {
                Id = empresa.Id,
                Rut = empresa.Rut,
                RutCuerpo = empresa.RutCuerpo,
                RutDigito = empresa.RutDigito,
                RazonSocial = empresa.RazonSocial,
                RegionCodigo = empresa.RegionCodigo,
                CiudadCodigo = empresa.CiudadCodigo,
                ComunaCodigo = empresa.ComunaCodigo,
                Direccion = empresa.Direccion,
                Email = empresa.Email,
                PaginaWeb = empresa.PaginaWeb,
                Telefono1 = empresa.Telefono1,
                Telefono2 = empresa.Telefono2,
                Fax = empresa.Fax,
                Celular = empresa.Celular,
                Bloqueada = empresa.Bloqueada
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Access }, Root = "Empresas", Area = Area)]
        public JsonResult Empresa(string rut)
        {
            string textoRun = rut.Replace(".", string.Empty).Replace("-", string.Empty);

            int rutCuerpo = int.Parse(textoRun.Substring(0, textoRun.Length - 1));
            char rutDigito = char.Parse(textoRun.Replace(rutCuerpo.ToString(), string.Empty));

            if (!Evaluaciones.Helper.ValidaRun(rutCuerpo, rutDigito))
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }

            Evaluaciones.Empresa empresa = Evaluaciones.Empresa.Get(rutCuerpo, rutDigito);

            if (empresa == null)
            {
                empresa = new Evaluaciones.Empresa();

                empresa.RutCuerpo = rutCuerpo;
                empresa.RutDigito = rutDigito;
                empresa.Rut = string.Format("{0}-{1}", rutCuerpo.ToString("N0"), rutDigito);

                return this.Json(empresa, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new Evaluaciones.Web.UI.Areas.Administracion.Models.Empresa
            {
                Id = empresa.Id,
                RutCuerpo = empresa.RutCuerpo,
                RutDigito = empresa.RutDigito,
                RazonSocial = empresa.RazonSocial,
                RegionCodigo = empresa.RegionCodigo,
                CiudadCodigo = empresa.CiudadCodigo,
                ComunaCodigo = empresa.ComunaCodigo,
                Direccion = empresa.Direccion,
                Email = empresa.Email,
                PaginaWeb = empresa.PaginaWeb,
                Telefono1 = empresa.Telefono1,
                Telefono2 = empresa.Telefono2,
                Fax = empresa.Fax,
                Celular = empresa.Celular,
                Bloqueada = empresa.Bloqueada
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult DeshabilitarEmpresa(Guid empresaId)
        {
            Evaluaciones.Empresa empresa = Evaluaciones.Empresa.Get(empresaId);

            Evaluaciones.Membresia.Account.Lock(empresa);

            return this.Json("200", JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult HabilitarEmpresa(Guid empresaId)
        {
            Evaluaciones.Empresa empresa = Evaluaciones.Empresa.Get(empresaId);

            Evaluaciones.Membresia.Account.UnLock(empresa);

            return this.Json("200", JsonRequestBehavior.AllowGet);
        }

        private Evaluaciones.Web.UI.Areas.Administracion.Models.Empresa.Empresas EmpresaGridView(Evaluaciones.FindType? findType, string filter, int? rutCuerpo = null, char? rutDigito = null)
        {
            Evaluaciones.Web.UI.Areas.Administracion.Models.Empresa.Empresas empresas = new Evaluaciones.Web.UI.Areas.Administracion.Models.Empresa.Empresas();

            empresas.data = new List<Evaluaciones.Web.UI.Areas.Administracion.Models.Empresa>();

            List<Evaluaciones.Empresa> lista = new List<Evaluaciones.Empresa>();

            if (rutCuerpo.HasValue && rutDigito.HasValue)
            {
                Evaluaciones.Empresa empresa = Evaluaciones.Empresa.Get(rutCuerpo.Value, rutDigito.Value);

                if (empresa != null)
                {
                    lista.Add(empresa);
                }
            }
            else
            {
                lista = Evaluaciones.Empresa.GetAll(findType.Value, filter);
            }

            foreach (Evaluaciones.Empresa empresa in lista)
            {
                MvcHtmlString botonHabilitado;

                if (empresa.Bloqueada)
                {
                    botonHabilitado = Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(empresa.Id, null, Evaluaciones.Helpers.TypeButton.OtherAction, this, "fa-unlock", "Desbloquear empresa", "enableEnterprise");
                }
                else
                {
                    botonHabilitado = Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(empresa.Id, null, Evaluaciones.Helpers.TypeButton.OtherAction, this, "fa-lock", "Bloquear empresa", "disableEnterprise");
                }

                empresas.data.Add(new Evaluaciones.Web.UI.Areas.Administracion.Models.Empresa
                {
                    RazonSocial = empresa.RazonSocial,
                    Rut = empresa.Rut,
                    Estado = empresa.Bloqueada ? "Bloqueda" : "Activa",
                    Accion = string.Format("{0}{1}", Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(empresa.Id, null, Evaluaciones.Helpers.TypeButton.Edit, this),
                                                     botonHabilitado)
                });
            }

            return empresas;
        }

        #endregion

        #region Centro de costo

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Access }, Root = "CentrosCosto", Area = Area)]
        public ActionResult CentrosCosto()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Accept }, Root = "CentrosCosto", Area = Area)]
        public ActionResult CentrosCosto(Evaluaciones.Web.UI.Areas.Administracion.Models.CentroCosto model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                string textoRbd = model.Rbd.Replace(".", string.Empty).Replace("-", string.Empty);

                int rbdCuerpo = int.Parse(textoRbd.Substring(0, textoRbd.Length - 1));
                int rbdDigito = int.Parse(textoRbd.Replace(rbdCuerpo.ToString(), string.Empty));

                using (Evaluaciones.Context context = new Evaluaciones.Context())
                {
                    new Evaluaciones.CentroCosto
                    {
                        EmpresaId = model.EmpresaId,
                        Id = model.Id,
                        Nombre = model.Nombre,
                        RbdCuerpo = rbdCuerpo,
                        RbdDigito = rbdDigito,
                        Sigla = model.Sigla,
                        AreaGeograficaCodigo = model.AreaGeograficaCodigo,
                        RegionCodigo = model.RegionCodigo,
                        CiudadCodigo = model.CiudadCodigo,
                        ComunaCodigo = model.ComunaCodigo,
                        Email = model.Email,
                        Direccion = model.Direccion,
                        Telefono1 = model.Telefono1,
                        Telefono2 = model.Telefono2,
                        Fax = model.Fax,
                        Celular = model.Celular,
                        AutorizacionNumero = model.AutorizacionNumero,
                        AutorizacionFecha = model.AutorizacionFecha
                    }.Save(context);

                    context.SubmitChanges();
                }

                return this.Json("200", JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Rbd_Index"))
                {
                    return this.Json("El R.B.D. se encuentra registrado en otro establecimiento", JsonRequestBehavior.DenyGet);
                }
                else
                {
                    return this.Json(ex.Message, JsonRequestBehavior.DenyGet);
                }
            }
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Access }, Root = "CentrosCosto", Area = Area)]
        public JsonResult GetCentrosCosto(string empresaId)
        {
            Evaluaciones.Web.UI.Areas.Administracion.Models.CentroCosto.CentrosCosto centroCosto = new Evaluaciones.Web.UI.Areas.Administracion.Models.CentroCosto.CentrosCosto();

            centroCosto.data = new List<Evaluaciones.Web.UI.Areas.Administracion.Models.CentroCosto>();

            Guid id;

            if (Guid.TryParse(empresaId, out id))
            {
                Evaluaciones.Empresa empresa = Evaluaciones.Empresa.Get(id);

                foreach (Evaluaciones.CentroCosto c in Evaluaciones.CentroCosto.GetAll(empresa))
                {
                    centroCosto.data.Add(new Evaluaciones.Web.UI.Areas.Administracion.Models.CentroCosto
                    {
                        Id = c.Id,
                        Nombre = c.Nombre,
                        AreaGeograficaNombre = c.AreaGeografica.Nombre,
                        Accion = string.Format("{0}", Evaluaciones.Helpers.ActionLinkExtension.ActionLinkCrudEmbedded(c.Id, null, Evaluaciones.Helpers.TypeButton.Edit, this))
                    });
                }

                return this.Json(centroCosto, JsonRequestBehavior.AllowGet);
            }

            return this.Json(centroCosto, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Add }, Root = "CentrosCosto", Area = Area)]
        public JsonResult AddCentroCosto(Guid empresaId)
        {
            Evaluaciones.Empresa empresa = Evaluaciones.Empresa.Get(empresaId);

            return this.Json(new Evaluaciones.Web.UI.Areas.Administracion.Models.CentroCosto { Empresa = empresa }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Edit }, Root = "CentrosCosto", Area = Area)]
        public JsonResult EditCentrosCosto(Guid empresaId, Guid centroCostoId)
        {
            Evaluaciones.CentroCosto centroCosto = Evaluaciones.CentroCosto.Get(empresaId, centroCostoId);

            return this.Json(new Evaluaciones.Web.UI.Areas.Administracion.Models.CentroCosto
            {
                EmpresaId = centroCosto.EmpresaId,
                Empresa = centroCosto.Empresa,
                Id = centroCosto.Id,
                Nombre = centroCosto.Nombre,
                Rbd = centroCosto.Rbd,
                RbdCuerpo = centroCosto.RbdCuerpo,
                RbdDigito = centroCosto.RbdDigito,
                Sigla = centroCosto.Sigla,
                AreaGeograficaCodigo = centroCosto.AreaGeograficaCodigo,
                RegionCodigo = centroCosto.RegionCodigo,
                CiudadCodigo = centroCosto.CiudadCodigo,
                ComunaCodigo = centroCosto.ComunaCodigo,
                Email = centroCosto.Email,
                Direccion = centroCosto.Direccion,
                Telefono1 = centroCosto.Telefono1,
                Telefono2 = centroCosto.Telefono2,
                Fax = centroCosto.Fax,
                Celular = centroCosto.Celular,
                AutorizacionNumero = centroCosto.AutorizacionNumero,
                AutorizacionFecha = centroCosto.AutorizacionFecha,
                FechaAutorizacionString = centroCosto.AutorizacionFecha.ToShortDateString()
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Access }, Root = "CentrosCosto", Area = Area)]
        public JsonResult CentroCosto(Guid empresaId, string rbd)
        {
            string textoRun = rbd.Replace(".", string.Empty).Replace("-", string.Empty);

            int rbdCuerpo = int.Parse(textoRun.Substring(0, textoRun.Length - 1));
            char rbdDigito = char.Parse(textoRun.Replace(rbdCuerpo.ToString(), string.Empty));

            if (!Evaluaciones.Helper.ValidaRun(rbdCuerpo, rbdDigito))
            {
                return this.Json("500", JsonRequestBehavior.AllowGet);
            }

            Evaluaciones.Empresa empresa = Evaluaciones.Empresa.Get(empresaId);

            Evaluaciones.CentroCosto centroCosto = Evaluaciones.CentroCosto.Get(rbdCuerpo, int.Parse(rbdDigito.ToString()));

            if (centroCosto == null)
            {
                centroCosto = new Evaluaciones.CentroCosto();

                centroCosto.Empresa = empresa;
                centroCosto.RbdCuerpo = rbdCuerpo;
                centroCosto.RbdDigito = rbdDigito;
                centroCosto.Rbd = string.Format("{0}-{1}", rbdCuerpo.ToString("N0"), rbdDigito);

                return this.Json(centroCosto, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new Evaluaciones.Web.UI.Areas.Administracion.Models.CentroCosto
            {
                EmpresaId = centroCosto.EmpresaId,
                Empresa = centroCosto.Empresa,
                Id = centroCosto.Id,
                Nombre = centroCosto.Nombre,
                Rbd = centroCosto.Rbd,
                RbdCuerpo = centroCosto.RbdCuerpo,
                RbdDigito = centroCosto.RbdDigito,
                Sigla = centroCosto.Sigla,
                AreaGeograficaCodigo = centroCosto.AreaGeograficaCodigo,
                RegionCodigo = centroCosto.RegionCodigo,
                CiudadCodigo = centroCosto.CiudadCodigo,
                ComunaCodigo = centroCosto.ComunaCodigo,
                Email = centroCosto.Email,
                Direccion = centroCosto.Direccion,
                Telefono1 = centroCosto.Telefono1,
                Telefono2 = centroCosto.Telefono2,
                Fax = centroCosto.Fax,
                Celular = centroCosto.Celular,
                AutorizacionNumero = centroCosto.AutorizacionNumero,
                AutorizacionFecha = centroCosto.AutorizacionFecha,
                FechaAutorizacionString = centroCosto.AutorizacionFecha.ToShortDateString()
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}