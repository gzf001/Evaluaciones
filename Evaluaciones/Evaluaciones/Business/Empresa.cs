using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones
{
    public partial class Empresa : Evaluaciones.Default
    {
        public static IEnumerable<SelectListItem> Empresas
        {
            get
            {
                SelectList lista = new SelectList(Evaluaciones.Empresa.GetAll().Where<Evaluaciones.Empresa>(x => !x.Bloqueada), "Id", "RazonSocial");

                return DefaultItem.Concat(lista);
            }
        }

        public static Empresa Get(Guid id)
        {
            return Query.GetEmpresas().SingleOrDefault<Empresa>(x => x.Id == id);
        }

        public static Empresa Get(int rutCuerpo, char rutDigito)
        {
            return Query.GetEmpresas().SingleOrDefault<Empresa>(x => x.RutCuerpo.Equals(rutCuerpo) && x.RutDigito.Equals(rutDigito));
        }

        public static List<Empresa> GetAll()
        {
            return
                (
                from query in Query.GetEmpresas()
                select query
                ).ToList<Empresa>();
        }

        public static List<Empresa> GetAll(Evaluaciones.FindType findType, string filter)
        {
            return
                (
                from query in Query.GetEmpresas(findType, filter)
                orderby query.RazonSocial
                select query
                ).ToList<Empresa>();
        }
    }
}