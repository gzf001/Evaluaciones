using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class PreguntaRecursoCurricular
	{
		public static List<PreguntaRecursoCurricular> GetAll()
		{
			return
				(
				from query in Query.GetPreguntaRecursoCurriculares()
				select query
				).ToList<PreguntaRecursoCurricular>();
		}
	}
}