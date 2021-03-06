using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Membresia
{
	public partial class EmpresaAplicacion
	{
		public static bool Exists(Evaluaciones.Empresa empresa, Aplicacion aplicacion)
		{
			return Query.GetEmpresaAplicaciones(empresa, aplicacion).Any<EmpresaAplicacion>();
		}

		public static EmpresaAplicacion Get(Guid empresaId, Guid aplicacionId)
		{
			return Query.GetEmpresaAplicaciones().SingleOrDefault<EmpresaAplicacion>(x => x.EmpresaId == empresaId && x.AplicacionId == aplicacionId);
		}

		public static List<EmpresaAplicacion> GetAll()
		{
			return
				(
				from query in Query.GetEmpresaAplicaciones()
				select query
				).ToList<EmpresaAplicacion>();
		}
	}
}