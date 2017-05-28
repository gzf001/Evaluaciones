using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class PreguntaSeleccionMultipleCorrecta
	{
		public static List<PreguntaSeleccionMultipleCorrecta> GetAll()
		{
			return
				(
				from query in Query.GetPreguntaSeleccionMultipleCorrectas()
				select query
				).ToList<PreguntaSeleccionMultipleCorrecta>();
		}
	}
}