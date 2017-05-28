using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones
{
	public partial class Dia
	{
		public static List<Dia> GetAll()
		{
			return
				(
				from query in Query.GetDias()
				select query
				).ToList<Dia>();
		}
	}
}