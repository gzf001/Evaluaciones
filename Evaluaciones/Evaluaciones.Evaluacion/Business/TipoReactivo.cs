using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class TipoReactivo
	{
		public static List<TipoReactivo> GetAll()
		{
			return
				(
				from query in Query.GetTipoReactivos()
				select query
				).ToList<TipoReactivo>();
		}
	}
}