using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones
{
	public partial class Calendario
	{
		public static List<Calendario> GetAll()
		{
			return
				(
				from query in Query.GetCalendarios()
				select query
				).ToList<Calendario>();
		}
	}
}