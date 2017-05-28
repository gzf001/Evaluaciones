using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones
{
	public partial class Mes
	{
		public static List<Mes> GetAll()
		{
			return
				(
				from query in Query.GetMeses()
				select query
				).ToList<Mes>();
		}
	}
}