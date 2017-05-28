using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones
{
	public partial class SecuenciaCentroCosto
	{
		public static List<SecuenciaCentroCosto> GetAll()
		{
			return
				(
				from query in Query.GetSecuenciaCentroCostos()
				select query
				).ToList<SecuenciaCentroCosto>();
		}
	}
}