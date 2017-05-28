using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class Pregunta
	{
		public static List<Pregunta> GetAll()
		{
			return
				(
				from query in Query.GetPreguntas()
				select query
				).ToList<Pregunta>();
		}
	}
}