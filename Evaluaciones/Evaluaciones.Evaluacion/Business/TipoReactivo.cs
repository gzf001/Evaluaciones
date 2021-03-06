using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones.Evaluacion
{
	public partial class TipoReactivo : Evaluaciones.Default
	{
        public static IEnumerable<SelectListItem> TiposReactivos
        {
            get
            {
                SelectList lista = new SelectList(Evaluaciones.Evaluacion.TipoReactivo.GetAll(), "Codigo", "Nombre");

                return DefaultItem.Concat(lista);
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