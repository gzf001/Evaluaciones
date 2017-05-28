using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class Dificultad
	{
		public static List<Dificultad> GetAll()
		{
			return
				(
				from query in Query.GetDificultades()
				select query
				).ToList<Dificultad>();
		}
	}
}