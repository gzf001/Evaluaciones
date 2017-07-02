using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones
{
    public partial class Empresa
    {
        public static IEnumerable<SelectListItem> Empresas
        {
            get
            {
                IEnumerable<SelectListItem> defaultItem = Enumerable.Repeat(new SelectListItem
                {
                    Value = "-1",
                    Text = "[Seleccione]"
                }, count: 1);

                SelectList lista = new SelectList(Evaluaciones.Empresa.GetAll().Where<Evaluaciones.Empresa>(x => !x.Bloqueada), "Id", "RazonSocial");

                return defaultItem.Concat(lista);
            }
        }

        public static Empresa Get(Guid id)
        {
            return Query.GetEmpresas().SingleOrDefault<Empresa>(x => x.Id == id);
        }

        public static List<Empresa> GetAll()
        {
            return
                (
                from query in Query.GetEmpresas()
                select query
                ).ToList<Empresa>();
        }
    }
}