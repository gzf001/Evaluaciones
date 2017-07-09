using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Educacion
{
	public partial class TipoEducacion
	{
		public static List<TipoEducacion> GetAll()
		{
			return
				(
				from query in Query.GetTipoEducaciones()
				select query
				).ToList<TipoEducacion>();
		}
	}
}