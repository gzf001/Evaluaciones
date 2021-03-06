using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace Evaluaciones
{
    public partial class NivelEducacional : Evaluaciones.Default
    {
        public static IEnumerable<SelectListItem> NivelesEducacionales
        {
            get
            {
               SelectList lista = new SelectList(Evaluaciones.NivelEducacional.GetAll(), "Codigo", "Nombre");

                return DefaultItem.Concat(lista);
            }
        }

        public static NivelEducacional Get(Int32 codigo)
        {
            return Query.GetNivelEducacionales().SingleOrDefault<NivelEducacional>(x => x.Codigo == codigo);
        }

        public static List<NivelEducacional> GetAll()
        {
            return
                (
                from query in Query.GetNivelEducacionales()
                select query
                ).ToList<NivelEducacional>();
        }
    }
}