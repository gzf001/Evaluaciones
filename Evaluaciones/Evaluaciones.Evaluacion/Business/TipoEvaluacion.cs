using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones.Evaluacion
{
    public partial class TipoEvaluacion : Evaluaciones.Default
    {
        public static IEnumerable<SelectListItem> TiposEvaluaciones
        {
            get
            {
                SelectList lista = new SelectList(Evaluaciones.Evaluacion.TipoEvaluacion.GetAll(), "Codigo", "Nombre");

                return DefaultItem.Concat(lista);
            }
        }

        public static List<TipoEvaluacion> GetAll()
        {
            return
                (
                from query in Query.GetTipoEvaluaciones()
                select query
                ).ToList<TipoEvaluacion>();
        }
    }
}