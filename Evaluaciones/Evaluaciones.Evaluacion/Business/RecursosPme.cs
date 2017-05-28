using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class RecursosPme
	{
		public static List<RecursosPme> GetAll()
		{
			return
				(
				from query in Query.GetRecursosPmes()
				select query
				).ToList<RecursosPme>();
		}
	}
}