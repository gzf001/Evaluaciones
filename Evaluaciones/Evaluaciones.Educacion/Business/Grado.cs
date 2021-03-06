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

        public static IEnumerable<SelectListItem> Grados(int tipoEducacionCodigo, int gradoCodigo)
        {
            Evaluaciones.Educacion.TipoEducacion tipoEducacion = Evaluaciones.Educacion.TipoEducacion.Get(tipoEducacionCodigo);

            List<Evaluaciones.Educacion.Grado> grados = Evaluaciones.Educacion.Grado.GetAll(tipoEducacion).Where<Evaluaciones.Educacion.Grado>(x => x.Codigo <= gradoCodigo).ToList<Evaluaciones.Educacion.Grado>();

            SelectList lista = new SelectList(grados, "Codigo", "Nombre");

            return DefaultItem.Concat(lista);
        }

        public static Grado GetBaseCurricular()
        {
            return Query.GetGrados().Single<Evaluaciones.Educacion.Grado>(x => x.BaseCurricular);
        }

        public static Grado Get(int tipoEducacionCodigo, int codigo)
        {
            return Query.GetGrados().SingleOrDefault<Evaluaciones.Educacion.Grado>(x => x.TipoEducacionCodigo.Equals(tipoEducacionCodigo) && x.Codigo.Equals(codigo));
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