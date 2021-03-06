using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones
{
	public partial class CentroCosto : Evaluaciones.Default
    {
        public static IEnumerable<SelectListItem> CentrosCosto(Guid empresaId)
        {
            Evaluaciones.Empresa empresa = Evaluaciones.Empresa.Get(empresaId);

            List<Evaluaciones.CentroCosto> centrosCosto = Evaluaciones.CentroCosto.GetAll(empresa);

            SelectList lista = new SelectList(centrosCosto, "Id", "Nombre");

            return DefaultItem.Concat(lista);
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