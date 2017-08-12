using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Web.UI.Controllers
{
    public class BaseCurricularController : Evaluaciones.Web.Controller
    {
        [HttpGet]
        public JsonResult Ejes(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string unidadId)
        {
            int t;
            int g;
            Guid s;
            Guid u;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u))
            {
                IEnumerable<SelectListItem> selectList = Evaluaciones.Educacion.BaseCurricular.Eje.Ejes(t, g, s, u);

                return this.Json(selectList, JsonRequestBehavior.AllowGet);
            }

            return this.Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObjetivosAprendizaje(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string unidadId, string ejeId)
        {
            int t;
            int g;
            Guid s;
            Guid u;
            Guid e;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u) && Guid.TryParse(ejeId, out e))
            {
                IEnumerable<SelectListItem> selectList = Evaluaciones.Educacion.BaseCurricular.ObjetivoAprendizaje.ObjetivosAprendizaje(t, g, s, u, e);

                return this.Json(selectList, JsonRequestBehavior.AllowGet);
            }

            return this.Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetObjetivosAprendizaje(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string unidadId, string ejeId)
        {
            int t;
            int g;
            Guid s;
            Guid u;
            Guid e;

            Evaluaciones.Web.UI.Models.ObjetivoAprendizaje.ObjetivosAprendizaje objetivosAprendizaje = new Evaluaciones.Web.UI.Models.ObjetivoAprendizaje.ObjetivosAprendizaje();

            objetivosAprendizaje.data = new List<Evaluaciones.Web.UI.Models.ObjetivoAprendizaje>();

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u) && Guid.TryParse(ejeId, out e))
            {
                foreach (Evaluaciones.Educacion.BaseCurricular.ObjetivoAprendizaje objetivoAprendizaje in Evaluaciones.Educacion.BaseCurricular.ObjetivoAprendizaje.GetAll(t, g, s, u, e))
                {
                    objetivosAprendizaje.data.Add(new Evaluaciones.Web.UI.Models.ObjetivoAprendizaje
                    {
                        Id = objetivoAprendizaje.Id,
                        Descripcion = objetivoAprendizaje.Descripcion
                    });
                }
            }

            return this.Json(objetivosAprendizaje, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Unidades(string tipoEducacionCodigo, string gradoCodigo, string sectorId)
        {
            int t;
            int g;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s))
            {
                IEnumerable<SelectListItem> selectList = Evaluaciones.Educacion.BaseCurricular.Unidad.Unidades(t, g, s);

                return this.Json(selectList, JsonRequestBehavior.AllowGet);
            }

            return this.Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}