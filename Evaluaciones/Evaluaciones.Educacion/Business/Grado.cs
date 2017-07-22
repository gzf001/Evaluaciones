using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones.Educacion
{
	public partial class Grado : Evaluaciones.Default
	{
        public static IEnumerable<SelectListItem> Grados(int tipoEducacionCodigo)
        {
            Evaluaciones.Educacion.TipoEducacion tipoEducacion = Evaluaciones.Educacion.TipoEducacion.Get(tipoEducacionCodigo);

            List<Evaluaciones.Educacion.Grado> grados = Evaluaciones.Educacion.Grado.GetAll(tipoEducacion);

            SelectList lista = new SelectList(grados, "Codigo", "Nombre");

            return DefaultItem.Concat(lista);
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