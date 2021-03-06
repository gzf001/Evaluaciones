using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace Evaluaciones
{
    public partial class Nacionalidad : Evaluaciones.Default
    {
        public static IEnumerable<SelectListItem> Nacionalidades
        {
            get
            {
                SelectList lista = new SelectList(Evaluaciones.Nacionalidad.GetAll(), "Codigo", "Nombre");

                return DefaultItem.Concat(lista);
            }
        }

        public static Nacionalidad Chilena
        {
            get
            {
                return Evaluaciones.Nacionalidad.Get(1);
            }
        }

        public static Nacionalidad Get(Int16 codigo)
        {
            return Query.GetNacionalidades().SingleOrDefault<Nacionalidad>(x => x.Codigo == codigo);
        }

        public static List<Nacionalidad> GetAll()
        {
            return
                (
                from query in Query.GetNacionalidades()
                select query
                ).ToList<Nacionalidad>();
        }
    }
}