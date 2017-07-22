using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones.Educacion
{
	public partial class Grado
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

        public static IEnumerable<SelectListItem> Grados(int tipoEducacionCodigo)
        {
            Evaluaciones.Educacion.TipoEducacion tipoEducacion = Evaluaciones.Educacion.TipoEducacion.Get(tipoEducacionCodigo);

            List<Evaluaciones.Educacion.Grado> grados = Evaluaciones.Educacion.Grado.GetAll(tipoEducacion);

            SelectList lista = new SelectList(grados, "Codigo", "Nombre");

            return Ciudad.DefaultItem.Concat(lista);
        }

        public static List<Grado> GetAll()
		{
			return
				(
				from query in Query.GetGrados()
				select query
				).ToList<Grado>();
		}

        public static List<Grado> GetAll(Evaluaciones.Educacion.TipoEducacion tipoEducacion)
        {
            return
                (
                from query in Query.GetGrados(tipoEducacion)
                orderby query.Codigo
                select query
                ).ToList<Grado>();
        }
    }
}