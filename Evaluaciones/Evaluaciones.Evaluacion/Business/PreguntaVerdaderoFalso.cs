using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class PreguntaVerdaderoFalso
	{
		public static List<PreguntaVerdaderoFalso> GetAll()
		{
			return
				(
				from query in Query.GetPreguntaVerdaderoFalsos()
				select query
				).ToList<PreguntaVerdaderoFalso>();
		}
	}
}