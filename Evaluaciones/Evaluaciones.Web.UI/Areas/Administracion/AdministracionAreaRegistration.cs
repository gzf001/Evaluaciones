using System.Web.Mvc;

namespace Evaluaciones.Web.UI.Areas.Administracion
{
    public class AdministracionAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administracion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            #region Aplicaciones

            context.MapRoute(
               name: "Aplicaciones",
               url: "Administracion/Admin/Aplicaciones",
               defaults: new { area = "Administracion", controller = "Admin", action = "Aplicaciones" }
            );

            context.MapRoute(
               name: "GetAddAplicacion",
               url: "Administracion/Admin/AddAplicacion",
               defaults: new { area = "Administracion", controller = "Admin", action = "AddAplicacion" }
            );

            context.MapRoute(
               name: "GetEditAplicacion",
               url: "Administracion/Admin/EditAplicacion/{id}",
               defaults: new { area = "Administracion", controller = "Admin", action = "EditAplicacion", id = "" }
            );

            context.MapRoute(
                name: "GetAplicaciones",
                url: "Administracion/Admin/GetAplicaciones",
                defaults: new { area = "Administracion", controller = "Admin", action = "GetAplicaciones" }
            );

            context.MapRoute(
                name: "GetDeleteAplicacion",
                url: "Administracion/Admin/DeleteAplicacion",
                defaults: new { area = "Administracion", controller = "Admin", action = "DeleteAplicacion" }
            );

            #endregion

            #region Items de menú

            context.MapRoute(
                name: "ItemsMenu",
                url: "Administracion/Admin/ItemsMenu",
                defaults: new { area = "Administracion", controller = "Admin", action = "ItemsMenu" }
            );

            context.MapRoute(
                name: "GetEditItemMenu",
                url: "Administracion/Admin/GetItemMenu/{aplicacionId}/{itemId}",
                defaults: new { area = "Administracion", controller = "Admin", action = "EditItemMenu", aplicacionId = "", itemId = "" }
            );

            context.MapRoute(
                name: "GetItemsMenu",
                url: "Administracion/Admin/GetItemsMenu/{aplicacionId}",
                defaults: new { area = "Administracion", controller = "Admin", action = "GetItemsMenu", aplicacionId = "" }
            );

            context.MapRoute(
                name: "GetDeleteItemsMenu",
                url: "Administracion/Admin/DeleteItemsMenu/{aplicacionId}/{itemId}",
                defaults: new { area = "Administracion", controller = "Admin", action = "DeleteItemsMenu", aplicacionId = "", itemId = "" }
            );

            context.MapRoute(
                name: "GetOrder",
                url: "Administracion/Admin/GetOrder/{data}",
                defaults: new { area = "Administracion", controller = "Admin", action = "GetOrder", data = "" }
            );

            #endregion

            #region Roles

            context.MapRoute(
                name: "GetAddRol",
                url: "Administracion/Admin/AddRol/{ambitoCodigo}",
                defaults: new { area = "Administracion", controller = "Admin", action = "AddRol", ambitoCodigo = "" }
            );

            context.MapRoute(
                name: "GetEditRol",
                url: "Administracion/Admin/EditRol/{id}",
                defaults: new { area = "Administracion", controller = "Admin", action = "EditRol", id = "" }
            );

            context.MapRoute(
                name: "GetRoles",
                url: "Administracion/Admin/GetRoles/{ambitoCodigo}",
                defaults: new { area = "Administracion", controller = "Admin", action = "GetRoles", ambitoCodigo = "" }
            );

            context.MapRoute(
                name: "DeleteRol",
                url: "Administracion/Admin/DeleteRol/{id}",
                defaults: new { area = "Administracion", controller = "Admin", action = "DeleteRol", id = "" }
            );

            context.MapRoute(
                name: "GetPermissions",
                url: "Administracion/Admin/GetPermissions/{rolId}",
                defaults: new { area = "Administracion", controller = "Admin", action = "GetPermissions", rolId = "" }
            );

            context.MapRoute(
                name: "GetRolAccion",
                url: "Administracion/Admin/GetRolAccion/{rolId}/{aplicacionId}",
                defaults: new { area = "Administracion", controller = "Admin", action = "GetRolAccion", rolId = "", aplicacionId = "" }
            );

            #endregion

            #region Usuarios

            context.MapRoute(
                name: "GetUsuarios",
                url: "Administracion/Admin/GetAllUsuarios/{findType}/{filter}",
                defaults: new { area = "Administracion", controller = "Admin", action = "GetAllUsuarios", findType = "", filter = "" }
            );

            context.MapRoute(
                name: "GetUsuario",
                url: "Administracion/Admin/GetUsuarios/{run}",
                defaults: new { area = "Administracion", controller = "Admin", action = "GetUsuarios", run = "" }
            );

            context.MapRoute(
               name: "GetAddUsuario",
               url: "Administracion/Admin/GetUsuario",
               defaults: new { area = "Administracion", controller = "Admin", action = "AddUsuario" }
            );

            context.MapRoute(
               name: "GetEditUsuario",
               url: "Administracion/Admin/GetUsuario/{id}",
               defaults: new { area = "Administracion", controller = "Admin", action = "EditUsuario", id = "" }
            );

            context.MapRoute(
               name: "DeleteUsuario",
               url: "Administracion/Admin/DeleteUsuario/{id}",
               defaults: new { area = "Administracion", controller = "Admin", action = "DeleteUsuario", id = "" }
            );

            context.MapRoute(
               name: "Usuario",
               url: "Administracion/Admin/Usuario/{run}",
               defaults: new { area = "Administracion", controller = "Admin", action = "Usuario", run = "" }
            );

            context.MapRoute(
               name: "UsuarioRol",
               url: "Administracion/Admin/UsuarioRol/{personaId}/{ambitoCodigo}/{empresaId}/{centroCostoId}",
               defaults: new { area = "Administracion", controller = "Admin", action = "UsuarioRol", personaId = "", ambitoCodigo = "", empresaId = "", centroCostoId = "" }
            );

            context.MapRoute(
               name: "DeshabilitarUsuario",
               url: "Administracion/Admin/DeshabilitarUsuario/{usuarioId}",
               defaults: new { area = "Administracion", controller = "Admin", action = "DeshabilitarUsuario", usuarioId = "" }
            );

            context.MapRoute(
               name: "HabilitarUsuario",
               url: "Administracion/Admin/HabilitarUsuario/{usuarioId}",
               defaults: new { area = "Administracion", controller = "Admin", action = "HabilitarUsuario", usuarioId = "" }
            );
            #endregion

            #region Usuarios conectados

            context.MapRoute(
               name: "UsuarioConectados",
               url: "Administracion/Admin/CountUsuarioConectados",
               defaults: new { area = "Administracion", controller = "Admin", action = "CountUsuarioConectados" }
            );

            #endregion

            #region Empresas

            context.MapRoute(
               name: "GetEmpresas",
               url: "Administracion/Admin/GetAllEmpresas/{findType}/{filter}",
               defaults: new { area = "Administracion", controller = "Admin", action = "GetAllEmpresas", findType = "", filter = "" }
            );

            context.MapRoute(
                name: "GetEmpresa",
                url: "Administracion/Admin/GetEmpresas/{rut}",
                defaults: new { area = "Administracion", controller = "Admin", action = "GetEmpresas", rut = "" }
            );

            context.MapRoute(
               name: "GetAddEmpresa",
               url: "Administracion/Admin/AddEmpresa",
               defaults: new { area = "Administracion", controller = "Admin", action = "AddEmpresa" }
            );

            context.MapRoute(
               name: "GetEditEmpresa",
               url: "Administracion/Admin/GetEmpresa/{id}",
               defaults: new { area = "Administracion", controller = "Admin", action = "EditEmpresa", id = "" }
            );

            context.MapRoute(
               name: "Empresa",
               url: "Administracion/Admin/Empresa/{rut}",
               defaults: new { area = "Administracion", controller = "Admin", action = "Empresa", rut = "" }
            );

            context.MapRoute(
               name: "DeshabilitarEmpresa",
               url: "Administracion/Admin/DeshabilitarEmpresa/{empresaId}",
               defaults: new { area = "Administracion", controller = "Admin", action = "DeshabilitarEmpresa", empresaId = "" }
            );

            context.MapRoute(
               name: "HabilitarEmpresa",
               url: "Administracion/Admin/HabilitarEmpresa/{empresaId}",
               defaults: new { area = "Administracion", controller = "Admin", action = "HabilitarEmpresa", empresaId = "" }
            );
            #endregion

            #region CentroCosto

            context.MapRoute(
               name: "GetCentrosCosto",
               url: "Administracion/Admin/GetCentrosCosto/{empresaId}",
               defaults: new { area = "Administracion", controller = "Admin", action = "GetCentrosCosto", empresaId = "" }
            );

            context.MapRoute(
                name: "GetCentroCosto",
                url: "Administracion/Admin/CentroCosto/{empresaId}/{rbd}",
                defaults: new { area = "Administracion", controller = "Admin", action = "CentroCosto", empresaId = "", rbd = "" }
            );

            context.MapRoute(
               name: "GetEditCentroCosto",
               url: "Administracion/Admin/GetCentroCosto/{empresaId}/{centroCostoid}",
               defaults: new { area = "Administracion", controller = "Admin", action = "EditCentrosCosto", empresaId = "", centroCostoid = "" }
            );

            context.MapRoute(
               name: "GetAddCentroCosto",
               url: "Administracion/Admin/AddCentroCosto/{empresaId}",
               defaults: new { area = "Administracion", controller = "Admin", action = "AddCentroCosto", empresaId = "" }
            );

            #endregion

            context.MapRoute(
                "Administracion_default",
                "Administracion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}