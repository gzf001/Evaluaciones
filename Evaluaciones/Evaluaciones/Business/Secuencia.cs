using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones
{
	public partial class Secuencia
	{
		public static List<Secuencia> GetAll()
		{
			return
				(
				from query in Query.GetSecuencias()
				select query
				).ToList<Secuencia>();
		}
	}
}