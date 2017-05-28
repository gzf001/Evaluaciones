using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class PreguntaAlternativa
	{
		public static List<PreguntaAlternativa> GetAll()
		{
			return
				(
				from query in Query.GetPreguntaAlternativas()
				select query
				).ToList<PreguntaAlternativa>();
		}
	}
}