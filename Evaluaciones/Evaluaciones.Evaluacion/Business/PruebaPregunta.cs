using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class PruebaPregunta
	{
		public static List<PruebaPregunta> GetAll()
		{
			return
				(
				from query in Query.GetPruebaPreguntas()
				select query
				).ToList<PruebaPregunta>();
		}
	}
}