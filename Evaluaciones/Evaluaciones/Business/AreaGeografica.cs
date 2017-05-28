using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones
{
	public partial class AreaGeografica
	{
		public static List<AreaGeografica> GetAll()
		{
			return
				(
				from query in Query.GetAreaGeograficas()
				select query
				).ToList<AreaGeografica>();
		}
	}
}