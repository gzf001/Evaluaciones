using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class Prueba
	{
		public static List<Prueba> GetAll()
		{
			return
				(
				from query in Query.GetPruebas()
				select query
				).ToList<Prueba>();
		}
	}
}