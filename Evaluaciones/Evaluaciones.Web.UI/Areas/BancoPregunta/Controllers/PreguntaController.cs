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
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Access }, Root = "CrearPregunta", Area = Area)]
        public ActionResult CrearPregunta()
        {
            Evaluaciones.Web.UI.Areas.BancoPregunta.Models.Pregunta pregunta = new Evaluaciones.Web.UI.Areas.BancoPregunta.Models.Pregunta();

            return View(pregunta);
        }

        [Authorize]
        [HttpPost]
        [Evaluaciones.Web.Authorization(ActionType = new Evaluaciones.Web.ActionType[] { Evaluaciones.Web.ActionType.Accept }, Root = "CrearPregunta", Area = Area)]
        public JsonResult CrearPregunta(Evaluaciones.Web.UI.Areas.BancoPregunta.Models.Pregunta model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Json("500");
            }

            using (Evaluaciones.Evaluacion.Context context = new Evaluaciones.Evaluacion.Context())
            {
                Evaluaciones.Evaluacion.Pregunta pregunta = new Evaluaciones.Evaluacion.Pregunta
                {
                    ReferenciaId = default(Guid),
                    SectorId = Guid.NewGuid(),
                    TipoEducacionCodigo = 110,
                    GradoCodigo = 1,
                    DificultadCodigo = 1,
                    TipoReactivoCodigo = 1,
                    TipoEvaluacionCodigo = 1,
                    HabilidadCodigo = 1,
                    Texto = "Texto",
                    Privado = false
                };

                pregunta.Save(context);

                context.SubmitChanges();

                pregunta.Attach();
            }

            return this.Json("200");
        }

        [Authorize]
        [HttpGet]
        public JsonResult Referencias(string tipoEducacion, string grado, string sector)
        {
            Evaluaciones.Web.UI.Areas.BancoPregunta.Models.Referencia.Referencias referencias = new Evaluaciones.Web.UI.Areas.BancoPregunta.Models.Referencia.Referencias();

            referencias.data = new List<Evaluaciones.Web.UI.Areas.BancoPregunta.Models.Referencia>();

            int tipoEducacionCodigo;
            int gradoCodigo;
            Guid sectorId;

            if (int.TryParse(tipoEducacion, out tipoEducacionCodigo) && int.TryParse(grado, out gradoCodigo) && Guid.TryParse(sector, out sectorId))
            {
                Evaluaciones.Educacion.Grado g = Evaluaciones.Educacion.Grado.Get(tipoEducacionCodigo, gradoCodigo);

                foreach (Evaluaciones.Evaluacion.Referencia referencia in Evaluaciones.Evaluacion.Referencia.GetAll(g, sectorId))
                {
                    referencias.data.Add(new Evaluaciones.Web.UI.Areas.BancoPregunta.Models.Referencia
                    {
                        Codigo = referencia.Codigo,
                        Texto = referencia.Texto.Length > 100 ? string.Format("{0}...", referencia.Texto.Substring(0, 100)) : referencia.Texto,
                        Accion = Evaluaciones.Helpers.ActionLinkExtension.ActionLink(null, null, string.Empty, string.Empty, string.Empty, Evaluaciones.Helpers.TypeButton.OtherAction, referencia.Id, "btn btn-success btn-xs btn-flat", "fa-search-plus", "Ver referencia", null, this).ToString()
                    });
                }
            }

            return this.Json(referencias, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetReferencia(string id)
        {
            Guid referenciaId;

            if (Guid.TryParse(id, out referenciaId))
            {
                Evaluaciones.Evaluacion.Referencia referencia = Evaluaciones.Evaluacion.Referencia.Get(referenciaId);

                return this.Json(referencia, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
    }
}