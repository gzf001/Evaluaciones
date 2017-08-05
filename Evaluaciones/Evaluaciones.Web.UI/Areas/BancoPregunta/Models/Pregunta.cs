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
            this.BaseCurricular = new Evaluaciones.Evaluacion.PreguntaBaseCurricular();
        }

        public Evaluaciones.Evaluacion.PreguntaBaseCurricular BaseCurricular
        {
            get;
            set;
        }

        public Evaluaciones.Evaluacion.PreguntaRecursoCurricular RecursoCurricular
        {
            get;
            set;
        }

        public Evaluaciones.Evaluacion.Referencia PreguntaReferencia
        {
            get;
            set;
        }

        public Evaluaciones.Web.UI.Areas.BancoPregunta.Models.PreguntaAlernativa PreguntaAlternativa
        {
            get;
            set;
        }

        public Evaluaciones.Web.UI.Areas.BancoPregunta.Models.PreguntaVerdaderoFalso PreguntaVerdaderoFalso
        {
            get;
            set;
        }

        public Evaluaciones.Web.UI.Areas.BancoPregunta.Models.PreguntaSeleccionMultiple PreguntaSeleccionMultiple
        {
            get;
            set;
        }        
    }
}