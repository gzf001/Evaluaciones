using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones.Educacion
{
	public partial class TipoEducacion
	{
        public static IEnumerable<SelectListItem> TipoEducaciones
        {
            get
            {
                IEnumerable<SelectListItem> defaultItem = Enumerable.Repeat(new SelectListItem
                {
                    Value = "-1",
                    Text = "[Seleccione]"
                }, count: 1);

                SelectList lista = new SelectList(Evaluaciones.Educacion.TipoEducacion.GetAll(), "Codigo", "Nombre");

                return defaultItem.Concat(lista);
            }
        }

        public static TipoEducacion Get(int codigo)
        {
            return Query.GetTipoEducaciones().SingleOrDefault<TipoEducacion>(x => x.Codigo.Equals(codigo));
        }

        public static List<TipoEducacion> GetAll()
		{
			return
				(
				from query in Query.GetTipoEducaciones()
				select query
				).ToList<TipoEducacion>();
		}
	}
}