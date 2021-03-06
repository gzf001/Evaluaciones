using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace Evaluaciones
{
    public partial class EstadoCivil : Evaluaciones.Default
    {
        public static IEnumerable<SelectListItem> EstadosCiviles
        {
            get
            {
                SelectList lista = new SelectList(Evaluaciones.EstadoCivil.GetAll(), "Codigo", "Nombre");

                return DefaultItem.Concat(lista);
            }
        }

        public static EstadoCivil Soltero
        {
            get
            {
                return EstadoCivil.Get(1);
            }
        }

        public static EstadoCivil Casado
        {
            get
            {
                return EstadoCivil.Get(2);
            }
        }

        public static EstadoCivil Viudo
        {
            get
            {
                return EstadoCivil.Get(3);
            }
        }

        public static EstadoCivil Get(Int16 codigo)
        {
            return Query.GetEstadoCiviles().SingleOrDefault<EstadoCivil>(x => x.Codigo == codigo);
        }

        public static List<EstadoCivil> GetAll()
        {
            return
                (
                from query in Query.GetEstadoCiviles()
                select query
                ).ToList<EstadoCivil>();
        }
    }
}