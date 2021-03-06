using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones
{
	public partial class Region : Evaluaciones.Default
    {
        public static IEnumerable<SelectListItem> Regiones
        {
            get
            {
                SelectList lista = new SelectList(Evaluaciones.Region.GetAll(), "Codigo", "Nombre");

                return DefaultItem.Concat(lista);
            }
        }

        public static Region Get(int codigo)
        {
            return Query.GetRegiones().SingleOrDefault<Region>(x => x.Codigo == codigo);
        }

        public static List<Region> GetAll()
        {
            return
                (
                from query in Query.GetRegiones()
                select query
                ).ToList<Region>();
        }
    }
}