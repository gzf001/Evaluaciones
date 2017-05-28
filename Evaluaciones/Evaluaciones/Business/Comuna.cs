using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones
{
	public partial class Comuna
	{
        public static IEnumerable<SelectListItem> DefaultItem
        {
            get
            {
                IEnumerable<SelectListItem> defaultItem = Enumerable.Repeat(new SelectListItem
                {
                    Value = "-1",
                    Text = "[Seleccione]"
                }, count: 1);

                return defaultItem;
            }
        }

        public static IEnumerable<SelectListItem> Comunas(short regionCodigo, short ciudadCodigo)
        {
            Evaluaciones.Ciudad ciudad = Evaluaciones.Ciudad.Get(regionCodigo, ciudadCodigo);

            List<Evaluaciones.Comuna> comunas = Evaluaciones.Comuna.GetAll(ciudad);

            SelectList lista = new SelectList(comunas, "Codigo", "Nombre");

            return Comuna.DefaultItem.Concat(lista);
        }

        public static Comuna Get(Int16 regionCodigo, int ciudadCodigo, int codigo)
        {
            return Query.GetComunas().SingleOrDefault<Comuna>(x => x.RegionCodigo == regionCodigo && x.CiudadCodigo == ciudadCodigo && x.Codigo == codigo);
        }

        public static Comuna Get(string nombre)
        {
            return Query.GetComunas().SingleOrDefault<Comuna>(x => x.Nombre == nombre);
        }

        public static List<Comuna> GetAll()
        {
            return
                (
                from query in Query.GetComunas()
                orderby query.Nombre
                select query
                ).ToList<Comuna>();
        }

        public static List<Comuna> GetAll(Ciudad ciudad)
        {
            return
                (
                from query in Query.GetComunas(ciudad)
                orderby query.Nombre
                select query
                ).ToList<Comuna>();
        }
    }
}