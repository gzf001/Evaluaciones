using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones.Membresia
{
	public partial class Aplicacion : Evaluaciones.Default
    {
        public static IEnumerable<SelectListItem> Aplicaciones
        {
            get
            {
                SelectList lista = new SelectList(Evaluaciones.Membresia.Aplicacion.GetAll(), "Id", "Nombre");

                return DefaultItem.Concat(lista);
            }
        }

        public static int Count(Evaluaciones.Empresa empresa)
		{
			return Query.GetAplicaciones(empresa).Count<Aplicacion>();
		}

        public static Aplicacion Get(Guid id)
		{
			return Query.GetAplicaciones().SingleOrDefault<Aplicacion>(x=>x.Id == id);
		}

		public static Aplicacion Get(string clave)
		{
			return Query.GetAplicaciones().SingleOrDefault<Aplicacion>(x => x.Clave == clave);
		}

        public static Aplicacion GetAplicacion(Guid menuItemId)
        {
            return Query.GetMenuItemes().Single<Evaluaciones.Membresia.MenuItem>(x => x.Id == menuItemId).Aplicacion;
        }

        public static List<Aplicacion> GetAll()
		{
			return
				(
				from query in Query.GetAplicaciones()
				orderby query.Orden
				select query
				).ToList<Aplicacion>();
		}

		public static List<Aplicacion> GetAll(Evaluaciones.CentroCosto centroCosto)
		{
			return
				(
				from query in Query.GetAplicaciones(centroCosto)
				orderby query.Orden
				select query
				).ToList<Aplicacion>();
		}

		public static List<Aplicacion> GetAll(Evaluaciones.Empresa empresa)
		{
			return
				(
				from query in Query.GetAplicaciones(empresa)
				orderby query.Orden
				select query
				).ToList<Aplicacion>();
		}

		public static List<Aplicacion> GetAll(Persona persona)
		{
			return
				(
				from query in Query.GetAplicaciones(persona)
				orderby query.Orden
				select query
				).ToList<Aplicacion>();
		}

		public static List<Aplicacion> GetAll(MenuItem menuItem)
		{
			return
				(
				from query in Query.GetAplicaciones(menuItem)
				orderby query.Orden
				select query
				).ToList<Aplicacion>();
		}
	}
}