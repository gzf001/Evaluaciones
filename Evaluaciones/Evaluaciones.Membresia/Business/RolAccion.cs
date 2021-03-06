using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Membresia
{
	public partial class RolAccion
    {
        public static bool Exists(Rol rol, MenuItem menuItem, Accion accion)
        {
            return Query.GetRolAcciones(rol, menuItem).Any<Evaluaciones.Membresia.RolAccion>(x => x.Accion.Equals(accion));
        }

        public static bool Exists(Evaluaciones.Empresa empresa, Evaluaciones.CentroCosto centroCosto, Evaluaciones.Persona persona, MenuItem menuItem, Accion accion)
		{
            return Query.GetRolAcciones(empresa, centroCosto, persona, menuItem).Any<RolAccion>(x => x.MenuItemAccion.Accion.Equals(accion));
		}

        public static RolAccion Get(Guid rolId, Guid aplicacionId, Guid menuId, Guid menuItemId, Int32 accionCodigo)
		{
			return Query.GetRolAcciones().SingleOrDefault<RolAccion>(x => x.RolId == rolId && x.AplicacionId == aplicacionId && x.MenuId == menuId && x.MenuItemId == menuItemId && x.AccionCodigo == accionCodigo);
		}

		public static List<RolAccion> GetAll()
		{
			return
				(
				from query in Query.GetRolAcciones()
				select query
				).ToList<RolAccion>();
		}

        public static List<RolAccion> GetAll(Aplicacion aplicacion, Rol rol)
        {
            return
                (
                from query in Query.GetRolAcciones(aplicacion, rol)
                select query
                ).ToList<RolAccion>();
        }

        public static List<RolAccion> GetAll(Evaluaciones.Empresa empresa, Evaluaciones.CentroCosto centroCosto, Persona persona)
        {
            return
                (
                from query in Query.GetRolAcciones(empresa, centroCosto, persona)
                orderby query.Rol.AmbitoCodigo, query.Rol.Clave, query.Rol.Nombre
                select query
                ).ToList<RolAccion>();
        }

        public static List<RolAccion> GetAll(Rol rol)
		{
			return Query.GetRolAcciones(rol).ToList<RolAccion>();
		}
	}
}