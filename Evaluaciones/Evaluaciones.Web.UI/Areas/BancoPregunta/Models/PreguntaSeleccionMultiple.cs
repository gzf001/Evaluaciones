using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Areas.BancoPregunta.Models
{
    public class PreguntaSeleccionMultiple : Evaluaciones.Evaluacion.PreguntaSeleccionMultiple
    {
        public List<Evaluaciones.Evaluacion.PreguntaSeleccionMultipleCorrecta> PreguntaSeleccionMultipleCorrectas
        {
            get;
            set;
        }
    }
}