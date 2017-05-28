using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class Referencia
	{
		public static List<Referencia> GetAll()
		{
			return
				(
				from query in Query.GetReferencias()
				select query
				).ToList<Referencia>();
		}
	}
}