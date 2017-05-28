using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class Evaluacion
	{
		public static List<Evaluacion> GetAll()
		{
			return
				(
				from query in Query.GetEvaluaciones()
				select query
				).ToList<Evaluacion>();
		}
	}
}