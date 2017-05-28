using System.Collections.Generic;
using System.Linq;

namespace Evaluaciones.Membresia
{
	public class CentroCosto
    {      
		public static List<Evaluaciones.CentroCosto> GetAll(Evaluaciones.Persona persona, Evaluaciones.Empresa empresa, Aplicacion aplicacion)
		{
			return
				(
				from query in Query.GetCentroCostos(persona, empresa, aplicacion)
				orderby query.Nombre
				select query
				).ToList<Evaluaciones.CentroCosto>();
		}
    }
}
