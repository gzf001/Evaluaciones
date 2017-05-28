using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones
{
	public partial class Semana
	{
		public static List<Semana> GetAll()
		{
			return
				(
				from query in Query.GetSemanas()
				select query
				).ToList<Semana>();
		}
	}
}