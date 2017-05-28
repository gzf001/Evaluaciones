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
            #region Ciudades y Comunas

            context.MapRoute(
                name: "Ciudades",
                url: "Administracion/Admin/Ciudades/{regionCodigo}",
                defaults: new { area = "Administracion", controller = "Admin", action = "Ciudades", regionCodigo = "" }
            );

            context.MapRoute(
                name: "Comunas",
                url: "Administracion/Admin/Comunas/{regionCodigo}/{ciudadCodigo}",
                defaults: new { area = "Administracion", controller = "Admin", action = "Comunas", regionCodigo = "", ciudadCodigo = "" }
            );

            #endregion

            #region Aplicaciones

            context.MapRoute(
                name: "GetAplicacion",
                url: "Administracion/Admin/GetAplicacion",
                defaults: new { area = "Administracion", controller = "Admin", action = "GetAplicacion" }
            );

            context.MapRoute(
                name: "GetAplicaciones",
                url: "Administracion/Admin/GetAplicaciones",
                defaults: new { area = "Administracion", controller = "Admin", action = "GetAplicaciones" }
            );

            context.MapRoute(
                name: "DeleteAplicacion",
                url: "Administracion/Admin/DeleteAplicacion",
                defaults: new { area = "Administracion", controller = "Admin", action = "DeleteAplicacion" }
            );

            #endregion

            #region Items de menú

            context.MapRoute(
                name: "GetItemMenu",
                url: "Administracion/Admin/GetItemMenu/{aplicacionId}/{itemId}",
                defaults: new { area = "Administracion", controller = "Admin", action = "GetItemMenu", aplicacionId = "", itemId = "" }
            );

            context.MapRoute(
                name: "GetItemsMenu",
                url: "Administracion/Admin/GetItemsMenu/{aplicacionId}",
                defaults: new { area = "Administracion", controller = "Admin", action = "GetItemsMenu", aplicacionId = "" }
            );

            context.MapRoute(
                name: "DeleteItemsMenu",
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
                name: "GetRol",
                url: "Administracion/Admin/GetRol/{id}",
                defaults: new { area = "Administracion", controller = "Admin", action = "GetRol", id = "" }
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
                url: "Administracion/Admin/GetPermissions/{id}",
                defaults: new { area = "Administracion", controller = "Admin", action = "GetPermissions", id = "" }
            );

            context.MapRoute(
                name: "GetRolAccion",
                url: "Administracion/Admin/GetRolAccion/{rolId}/{aplicacionId}",
                defaults: new { area = "Administracion", controller = "Admin", action = "GetRolAccion", rolId = "", aplicacionId = "" }
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