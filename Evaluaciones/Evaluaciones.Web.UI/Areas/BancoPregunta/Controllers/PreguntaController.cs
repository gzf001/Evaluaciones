using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Web.UI.Areas.BancoPregunta.Controllers
{
    public class PreguntaController : Evaluaciones.Web.Controller
    {
        const string Area = "BancoPregunta";

        [Authorize]
        [HttpGet]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] {Evaluaciones.Web.ActionType.Access }, Root = "CrearPregunta", Area = Area)]
        public ActionResult CrearPregunta()
        {
            Evaluaciones.Web.UI.Areas.BancoPregunta.Models.Pregunta pregunta = new Evaluaciones.Web.UI.Areas.BancoPregunta.Models.Pregunta();

            return View(pregunta);
        }
    }
}