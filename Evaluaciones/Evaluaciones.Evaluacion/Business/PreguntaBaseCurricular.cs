using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class PreguntaBaseCurricular
	{
		public static List<PreguntaBaseCurricular> GetAll()
		{
			return
				(
				from query in Query.GetPreguntaBaseCurriculares()
				select query
				).ToList<PreguntaBaseCurricular>();
		}
	}
}