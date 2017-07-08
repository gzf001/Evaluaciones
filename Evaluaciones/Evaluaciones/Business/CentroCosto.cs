using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones
{
	public partial class CentroCosto
	{
        public static IEnumerable<SelectListItem> DefaultItem
        {
            get
            {
                IEnumerable<SelectListItem> defaultItem = Enumerable.Repeat(new SelectListItem
                {
                    Value = "-1",
                    Text = "[Seleccione]"
                }, count: 1);

                return defaultItem;
            }
        }

        public static CentroCosto Get(Guid empresaId, Guid id)
        {
            return Query.GetCentroCostos().SingleOrDefault<CentroCosto>(x => x.EmpresaId == empresaId && x.Id == id);
        }

        public static CentroCosto Get(int rbdCuerpo, int rbdDigito)
        {
            return Query.GetCentroCostos().SingleOrDefault<CentroCosto>(x => x.RbdCuerpo == rbdCuerpo && x.RbdDigito == rbdDigito);
        }

        public static List<CentroCosto> GetAll()
		{
			return
				(
				from query in Query.GetCentroCostos()
				select query
				).ToList<CentroCosto>();
		}

        public static List<CentroCosto> GetAll(Evaluaciones.Empresa empresa)
        {
            return
                (
                from query in Query.GetCentroCostos(empresa)
                orderby query.Nombre
                select query
                ).ToList<CentroCosto>();
        }
    }
}