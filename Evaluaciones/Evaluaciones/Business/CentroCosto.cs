using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones
{
	public partial class CentroCosto
	{
        public static CentroCosto Get(Guid empresaId, Guid id)
        {
            return Query.GetCentroCostos().SingleOrDefault<CentroCosto>(x => x.EmpresaId == empresaId && x.Id == id);
        }

        public static List<CentroCosto> GetAll()
		{
			return
				(
				from query in Query.GetCentroCostos()
				select query
				).ToList<CentroCosto>();
		}
	}
}