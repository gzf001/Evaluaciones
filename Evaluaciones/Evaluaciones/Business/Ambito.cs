using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones
{
    public partial class Ambito : Evaluaciones.Default
    {
        public static Ambito Aplicacion
        {
            get
            {
                return Get(1);
            }
        }

        public static Ambito Empresa
        {
            get
            {
                return Get(2);
            }
        }

        public static Ambito CentroCosto
        {
            get
            {
                return Get(3);
            }
        }

        public static IEnumerable<SelectListItem> Ambitos
        {
            get
            {
                SelectList lista = new SelectList(Evaluaciones.Ambito.GetAll(), "Codigo", "Nombre");

                return DefaultItem.Concat(lista);
            }
        }

        public static Ambito Get(int codigo)
        {
            return Query.GetAmbitos().SingleOrDefault<Ambito>(x => x.Codigo == codigo);
        }

        public static List<Ambito> GetAll()
        {
            return
                (
                from query in Query.GetAmbitos()
                select query
                ).ToList<Ambito>();
        }
    }
}