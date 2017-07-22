using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones.Evaluacion
{
	public partial class TipoReactivo
	{
        public static IEnumerable<SelectListItem> TiposReactivos
        {
            get
            {
                IEnumerable<SelectListItem> defaultItem = Enumerable.Repeat(new SelectListItem
                {
                    Value = "-1",
                    Text = "[Seleccione]"
                }, count: 1);

                SelectList lista = new SelectList(Evaluaciones.Evaluacion.TipoReactivo.GetAll(), "Codigo", "Nombre");

                return defaultItem.Concat(lista);
            }
        }

        public static List<TipoReactivo> GetAll()
		{
			return
				(
				from query in Query.GetTipoReactivos()
				select query
				).ToList<TipoReactivo>();
		}
	}
}