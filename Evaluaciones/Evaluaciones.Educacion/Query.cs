using System;
using System.Linq;
namespace Evaluaciones.Educacion
{
	internal static class Query
	{
		#region Grado
		internal static IQueryable<Grado> GetGrados()
		{
			return
				from grado in Context.Instancia.Grados
				select grado;
		}
		#endregion

		#region TipoEducacion
		internal static IQueryable<TipoEducacion> GetTipoEducaciones()
		{
			return
				from tipoEducacion in Context.Instancia.TipoEducaciones
				select tipoEducacion;
		}
		#endregion
	}
}