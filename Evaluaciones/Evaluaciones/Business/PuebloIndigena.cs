using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones
{
	public partial class PuebloIndigena
	{
		public static List<PuebloIndigena> GetAll()
		{
			return
				(
				from query in Query.GetPuebloIndigenas()
				select query
				).ToList<PuebloIndigena>();
		}
	}
}