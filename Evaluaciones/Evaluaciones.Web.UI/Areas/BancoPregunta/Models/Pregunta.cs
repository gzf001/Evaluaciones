using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Areas.BancoPregunta.Models
{
    public class Pregunta : Evaluaciones.Evaluacion.Pregunta
    {
        public Pregunta()
        {
            this.BaseCurricular = new Evaluaciones.Web.UI.Areas.BancoPregunta.Models.Pregunta.PreguntaBaseCurricular();
        }

        public Evaluaciones.Web.UI.Areas.BancoPregunta.Models.Pregunta.PreguntaBaseCurricular BaseCurricular
        {
            get;
            set;
        }

        public class PreguntaBaseCurricular : Evaluaciones.Evaluacion.PreguntaBaseCurricular
        {

        }
    }
}