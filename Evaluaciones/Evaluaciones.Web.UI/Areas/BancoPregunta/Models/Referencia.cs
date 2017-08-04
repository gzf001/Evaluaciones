using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Areas.BancoPregunta.Models
{
    public class Referencia : Evaluaciones.Evaluacion.Referencia
    {
        public string Accion
        {
            get;
            set;
        }

        public class Referencias
        {
            public List<Referencia> data
            {
                get;
                set;
            }
        }
    }
}