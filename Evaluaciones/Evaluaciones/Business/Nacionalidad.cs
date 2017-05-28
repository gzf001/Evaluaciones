using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones
{
	public partial class Nacionalidad
	{
		public static List<Nacionalidad> GetAll()
		{
			return
				(
				from query in Query.GetNacionalidades()
				select query
				).ToList<Nacionalidad>();
		}
	}
}