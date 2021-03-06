using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones
{
	public partial class Anio
	{
        public static Anio Get(int numero)
        {
            return Query.GetAnios().SingleOrDefault<Anio>(x => x.Numero == numero);
        }

        public static List<Anio> GetAll()
		{
			return
				(
				from query in Query.GetAnios()
				select query
				).ToList<Anio>();
		}
	}
}