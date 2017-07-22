using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones.Evaluacion
{
	public partial class Habilidad
	{
        public static IEnumerable<SelectListItem> Habilidades
        {
            get
            {
                IEnumerable<SelectListItem> defaultItem = Enumerable.Repeat(new SelectListItem
                {
                    Value = "-1",
                    Text = "[Seleccione]"
                }, count: 1);

                SelectList lista = new SelectList(Evaluaciones.Evaluacion.Habilidad.GetAll(), "Codigo", "Nombre");

                return defaultItem.Concat(lista);
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