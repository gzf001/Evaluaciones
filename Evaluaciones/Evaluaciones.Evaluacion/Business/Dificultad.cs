using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones.Evaluacion
{
	public partial class Dificultad : Evaluaciones.Default
	{
        public static IEnumerable<SelectListItem> Dificultades
        {
            get
            {
                SelectList lista = new SelectList(Evaluaciones.Evaluacion.Dificultad.GetAll(), "Codigo", "Nombre");

                return DefaultItem.Concat(lista);
            }
        }

        public static List<Dificultad> GetAll()
		{
			return
				(
				from query in Query.GetDificultades()
				select query
				).ToList<Dificultad>();
		}
	}
}