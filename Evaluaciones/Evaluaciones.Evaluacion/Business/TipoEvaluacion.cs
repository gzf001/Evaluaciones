using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class TipoEvaluacion
	{
		public static List<TipoEvaluacion> GetAll()
		{
			return
				(
				from query in Query.GetTipoEvaluaciones()
				select query
				).ToList<TipoEvaluacion>();
		}
	}
}