using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones.Evaluacion
{
	public partial class Dificultad
	{
        public static IEnumerable<SelectListItem> Dificultades
        {
            get
            {
                IEnumerable<SelectListItem> defaultItem = Enumerable.Repeat(new SelectListItem
                {
                    Value = "-1",
                    Text = "[Seleccione]"
                }, count: 1);

                SelectList lista = new SelectList(Evaluaciones.Evaluacion.Dificultad.GetAll(), "Codigo", "Nombre");

                return defaultItem.Concat(lista);
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