using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Models
{
    public class Aprendizaje : Evaluaciones.Web.UI.Models.RecursoCurricular
    {
        public class Aprendizajes
        {
            public List<Aprendizaje> data
            {
                get;
                set;
            }
        }
    }
}