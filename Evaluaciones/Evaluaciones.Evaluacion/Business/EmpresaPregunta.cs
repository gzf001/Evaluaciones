using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class EmpresaPregunta
	{
		public static List<EmpresaPregunta> GetAll()
		{
			return
				(
				from query in Query.GetEmpresaPreguntas()
				select query
				).ToList<EmpresaPregunta>();
		}
	}
}