using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Models
{
    public class Contenido : Evaluaciones.Web.UI.Models.RecursoCurricular
    {
        public class Contenidos
        {
            public List<Contenido> data
            {
                get;
                set;
            }
        }
    }
}