using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Evaluaciones.Web
{
    public class Authorization : ActionFilterAttribute
    {
        public Evaluaciones.Web.ActionType[] ActionType
        {
            get;
            set;
        }

        public string Root
        {
            get;
            set;
        }

        public string Area
        {
            get;
            set;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Evaluaciones.Persona persona = (filterContext.Controller as Evaluaciones.Web.Controller).CurrentPersona;

            Evaluaciones.Membresia.Usuario usuario = (filterContext.Controller as Evaluaciones.Web.Controller).CurrentUsuario;

            Evaluaciones.Empresa empresa = (filterContext.Controller as Evaluaciones.Web.Controller).CurrentEmpresa;

            Evaluaciones.CentroCosto centroCosto = (filterContext.Controller as Evaluaciones.Web.Controller).CurrentCentroCosto;

            if (filterContext.Controller.ControllerContext.RequestContext.HttpContext.Request.UrlReferrer == null)
            {
                throw new Exception(CustomError.SinPermiso_403.ToString());
            }

            string controller = filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();

            Evaluaciones.Membresia.MenuItem menuItem = Evaluaciones.Membresia.MenuItem.Get(string.Format("/{0}/{1}/{2}", this.Area, controller, this.Root), false);

            #region Permisos Efectivos

            if (this.ActionType.Contains<Evaluaciones.Web.ActionType>(Evaluaciones.Web.ActionType.Access))
            {
                #region Acceder

                if (!Evaluaciones.Membresia.RolAccion.Exists(empresa, centroCosto, persona, menuItem, Evaluaciones.Membresia.Accion.Acceder))
                {
                    throw new Exception(CustomError.SinPermiso_403.ToString());
                }

                #endregion
            }
            else if (this.ActionType.Contains<Evaluaciones.Web.ActionType>(Evaluaciones.Web.ActionType.Accept))
            {
                #region Aceptar

                if (!Evaluaciones.Membresia.RolAccion.Exists(empresa, centroCosto, persona, menuItem, Evaluaciones.Membresia.Accion.Aceptar))
                {
                    throw new Exception("No tiene permisos para grabar");
                }

                #endregion
            }
            else if (this.ActionType.Contains<Evaluaciones.Web.ActionType>(Evaluaciones.Web.ActionType.Add))
            {
                #region Agregar

                if (!Evaluaciones.Membresia.RolAccion.Exists(empresa, centroCosto, persona, menuItem, Evaluaciones.Membresia.Accion.Agregar))
                {
                    throw new Exception("No tiene permisos para agregar");
                }

                #endregion
            }
            else if (this.ActionType.Contains<Evaluaciones.Web.ActionType>(Evaluaciones.Web.ActionType.Edit))
            {
                #region Editar

                if (!Evaluaciones.Membresia.RolAccion.Exists(empresa, centroCosto, persona, menuItem, Evaluaciones.Membresia.Accion.Editar))
                {
                    throw new Exception("No tiene permisos para editar");
                }

                #endregion
            }
            else if (this.ActionType.Contains<Evaluaciones.Web.ActionType>(Evaluaciones.Web.ActionType.Delete))
            {
                #region Eliminar

                if (!Evaluaciones.Membresia.RolAccion.Exists(empresa, centroCosto, persona, menuItem, Evaluaciones.Membresia.Accion.Eliminar))
                {
                    throw new Exception("No tiene permisos para eliminar");
                }

                #endregion
            }

            #endregion

            base.OnActionExecuting(filterContext);
        }
    }
}