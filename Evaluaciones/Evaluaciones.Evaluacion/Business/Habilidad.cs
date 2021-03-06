using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones.Evaluacion
{
	public partial class Habilidad : Evaluaciones.Default
    {
        public static IEnumerable<SelectListItem> Habilidades
        {
            get
            {
                SelectList lista = new SelectList(Evaluaciones.Evaluacion.Habilidad.GetAll(), "Codigo", "Nombre");

                return DefaultItem.Concat(lista);
            }
        }

        public static List<Habilidad> GetAll()
		{
			return
				(
				from query in Query.GetHabilidades()
				select query
				).ToList<Habilidad>();
		}
	}
}