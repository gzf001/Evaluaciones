using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones.Educacion
{
    public partial class TipoEducacion : Evaluaciones.Default
    {
        public static IEnumerable<SelectListItem> TipoEducaciones
        {
            get
            {
                SelectList lista = new SelectList(Evaluaciones.Educacion.TipoEducacion.GetAll(), "Codigo", "Nombre");

                return DefaultItem.Concat(lista);
            }
        }

        public static IEnumerable<SelectListItem> GetTipoEducaciones(int tipoEducacionCodigo)
        {
            SelectList lista = new SelectList(Evaluaciones.Educacion.TipoEducacion.GetAll().Where<Evaluaciones.Educacion.TipoEducacion>(x => x.Codigo <= tipoEducacionCodigo), "Codigo", "Nombre");

            return DefaultItem.Concat(lista);
        }

        public static TipoEducacion Get(int codigo)
        {
            return Query.GetTipoEducaciones().SingleOrDefault<TipoEducacion>(x => x.Codigo.Equals(codigo));
        }

        public static List<TipoEducacion> GetAll()
        {
            return
                (
                from query in Query.GetTipoEducaciones()
                select query
                ).ToList<TipoEducacion>();
        }
    }
}