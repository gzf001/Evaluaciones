using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class Referencia
    {
        public static Referencia Get(Guid id)
        {
            return Query.GetReferencias().SingleOrDefault<Referencia>(x => x.Id.Equals(id));
        }

        public static List<Referencia> GetAll()
		{
			return
				(
				from query in Query.GetReferencias()
				select query
				).ToList<Referencia>();
		}

        public static List<Referencia> GetAll(Evaluaciones.Educacion.Grado grado, Guid sectorId)
        {
            return
                (
                from query in Query.GetReferencias(grado, sectorId)
                orderby query.Codigo
                select query
                ).ToList<Referencia>();
        }
    }
}