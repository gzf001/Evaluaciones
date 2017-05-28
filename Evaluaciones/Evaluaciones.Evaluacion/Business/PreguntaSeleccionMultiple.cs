using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class PreguntaSeleccionMultiple
	{
		public static List<PreguntaSeleccionMultiple> GetAll()
		{
			return
				(
				from query in Query.GetPreguntaSeleccionMultiples()
				select query
				).ToList<PreguntaSeleccionMultiple>();
		}
	}
}