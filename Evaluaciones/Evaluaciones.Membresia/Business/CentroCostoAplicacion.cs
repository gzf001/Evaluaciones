using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Membresia
{
	public partial class CentroCostoAplicacion
	{
		public static bool Exists(Evaluaciones.CentroCosto centroCosto, Aplicacion aplicacion)
		{
			return Query.GetCentroCostoAplicaciones(centroCosto, aplicacion).Any<CentroCostoAplicacion>();
		}

		public static CentroCostoAplicacion Get(Guid empresaId, Guid centroCostoId, Guid aplicacionId)
		{
			return Query.GetCentroCostoAplicaciones().SingleOrDefault<CentroCostoAplicacion>(x => x.EmpresaId == empresaId && x.CentroCostoId == centroCostoId && x.AplicacionId == aplicacionId);
		}

		public static List<CentroCostoAplicacion> GetAll()
		{
			return
				(
				from query in Query.GetCentroCostoAplicaciones()
				select query
				).ToList<CentroCostoAplicacion>();
		}
	}
}