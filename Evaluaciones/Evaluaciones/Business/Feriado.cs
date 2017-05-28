using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones
{
	public partial class Feriado
	{
		public static List<Feriado> GetAll()
		{
			return
				(
				from query in Query.GetFeriados()
				select query
				).ToList<Feriado>();
		}
	}
}