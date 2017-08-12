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
                return this.Json(this.GetError());
            }

            using (Evaluaciones.Evaluacion.Context context = new Evaluaciones.Evaluacion.Context())
            {
                Evaluaciones.Evaluacion.Pregunta pregunta = new Evaluaciones.Evaluacion.Pregunta
                {
                    TipoEducacionCodigo = model.TipoEducacionCodigo,
                    GradoCodigo = model.GradoCodigo,
                    SectorId = model.SectorId,
                    Id = model.Id,
                    ReferenciaId = model.ReferenciaId,
                    DificultadCodigo = model.DificultadCodigo,
                    TipoReactivoCodigo = model.TipoReactivoCodigo,
                    TipoEvaluacionCodigo = model.TipoEvaluacionCodigo,
                    HabilidadCodigo = model.HabilidadCodigo,
                    Texto = model.Texto,
                    Privado = model.Privado
                };

                pregunta.Save(context);

                new Evaluaciones.Evaluacion.PreguntaBaseCurricular
                {
                    TipoEducacionCodigo = model.BaseCurricular.TipoEducacionCodigo,
                    GradoCodigo = model.BaseCurricular.GradoCodigo,
                    SectorId = model.SectorId,
                    PreguntaId = model.BaseCurricular.PreguntaId,
                    AnioNumero = model.BaseCurricular.AnioNumero,
                    UnidadId = model.BaseCurricular.UnidadId,
                    EjeId = model.BaseCurricular.EjeId,
                    ObjetivoAprendizajeId = model.BaseCurricular.ObjetivoAprendizajeId
                }.Save(context);

                new Evaluaciones.Evaluacion.PreguntaAlternativa
                {
                    TipoEducacionCodigo = model.PreguntaAlternativa.TipoEducacionCodigo,
                    GradoCodigo = model.PreguntaAlternativa.GradoCodigo,
                    SectorId = model.PreguntaAlternativa.SectorId,
                    PreguntaId = model.PreguntaAlternativa.PreguntaId,
                    AlternativaCorrecta = model.PreguntaAlternativa.AlternativaCorrecta,
                    AlternativaA = model.PreguntaAlternativa.AlternativaA,
                    AlternativaB = model.PreguntaAlternativa.AlternativaB,
                    AlternativaC = model.PreguntaAlternativa.AlternativaC,
                    AlternativaD = model.PreguntaAlternativa.AlternativaD,
                    AlternativaE = model.PreguntaAlternativa.AlternativaE
                }.Save(context);

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