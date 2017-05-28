using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones
{
	public partial class AnioMes
	{
		public static List<AnioMes> GetAll()
		{
			return
				(
				from query in Query.GetAnioMeses()
				select query
				).ToList<AnioMes>();
		}
	}
}