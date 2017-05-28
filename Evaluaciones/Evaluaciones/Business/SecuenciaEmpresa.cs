using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones
{
	public partial class SecuenciaEmpresa
	{
		public static List<SecuenciaEmpresa> GetAll()
		{
			return
				(
				from query in Query.GetSecuenciaEmpresas()
				select query
				).ToList<SecuenciaEmpresa>();
		}
	}
}