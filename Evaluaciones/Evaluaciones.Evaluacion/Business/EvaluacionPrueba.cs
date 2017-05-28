using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class EvaluacionPrueba
	{
		public static List<EvaluacionPrueba> GetAll()
		{
			return
				(
				from query in Query.GetEvaluacionPruebas()
				select query
				).ToList<EvaluacionPrueba>();
		}
	}
}