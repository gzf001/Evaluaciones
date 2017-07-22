using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones.Evaluacion
{
    public partial class TipoEvaluacion
    {
        public static IEnumerable<SelectListItem> TiposEvaluaciones
        {
            get
            {
                IEnumerable<SelectListItem> defaultItem = Enumerable.Repeat(new SelectListItem
                {
                    Value = "-1",
                    Text = "[Seleccione]"
                }, count: 1);

                SelectList lista = new SelectList(Evaluaciones.Evaluacion.TipoEvaluacion.GetAll(), "Codigo", "Nombre");

                return defaultItem.Concat(lista);
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