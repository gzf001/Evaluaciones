using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Web.UI.Controllers
{
    public class RecursoCurricularController : Controller
    {
        [HttpGet]
        public JsonResult Aprendizajes(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string unidadId)
        {
            int t;
            int g;
            Guid s;
            Guid u;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u))
            {
                IEnumerable<SelectListItem> selectList = Evaluaciones.Educacion.RecursoCurricular.Aprendizaje.Aprendizajes(t, g, s, u);

                return this.Json(selectList, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Contenidos(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string aprendizajeId, string ejeId)
        {
            int t;
            int g;
            Guid s;
            Guid a;
            Guid e;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(aprendizajeId, out a) && Guid.TryParse(ejeId, out e))
            {
                IEnumerable<SelectListItem> selectList = Evaluaciones.Educacion.RecursoCurricular.Contenido.Contenidos(t, g, s, a, e);

                return this.Json(selectList, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Ejes(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string aprendizajeId)
        {
            int t;
            int g;
            Guid s;
            Guid a;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(aprendizajeId, out a))
            {
                IEnumerable<SelectListItem> selectList = Evaluaciones.Educacion.RecursoCurricular.Eje.Ejes(t, g, s, a);

                return this.Json(selectList, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Unidades(string tipoEducacionCodigo, string gradoCodigo, string sectorId)
        {
            int t;
            int g;
            Guid s;

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s))
            {
                IEnumerable<SelectListItem> selectList = Evaluaciones.Educacion.RecursoCurricular.Unidad.Unidades(t, g, s);

                return this.Json(selectList, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAprendizajes(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string unidadId)
        {
            int t;
            int g;
            Guid s;
            Guid u;

            Evaluaciones.Web.UI.Models.Aprendizaje.Aprendizajes aprendizajes = new Evaluaciones.Web.UI.Models.Aprendizaje.Aprendizajes();

            aprendizajes.data = new List<Evaluaciones.Web.UI.Models.Aprendizaje>();

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u))
            {
                foreach (Evaluaciones.Educacion.RecursoCurricular.Aprendizaje aprendizaje in Evaluaciones.Educacion.RecursoCurricular.Aprendizaje.GetAll(t, g, DateTime.Today.Year, s, u))
                {
                    aprendizajes.data.Add(new Evaluaciones.Web.UI.Models.Aprendizaje
                    {
                        Id = aprendizaje.Id,
                        Descripcion = aprendizaje.Descripcion
                    });
                }
            }

            return this.Json(aprendizajes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetContenidos(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string aprendizajeId, string ejeId)
        {
            int t;
            int g;
            Guid s;
            Guid a;
            Guid e;

            Evaluaciones.Web.UI.Models.Contenido.Contenidos contenidos = new Evaluaciones.Web.UI.Models.Contenido.Contenidos();

            contenidos.data = new List<Evaluaciones.Web.UI.Models.Contenido>();

            if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(aprendizajeId, out a) && Guid.TryParse(ejeId, out e))
            {
                foreach (Evaluaciones.Educacion.RecursoCurricular.Contenido contenido in Evaluaciones.Educacion.RecursoCurricular.Contenido.GetAll(t, g, DateTime.Today.Year, s, a, e))
                {
                    contenidos.data.Add(new Evaluaciones.Web.UI.Models.Contenido
                    {
                        Id = contenido.Id,
                        Descripcion = contenido.Descripcion
                    });
                }
            }

            return this.Json(contenidos, JsonRequestBehavior.AllowGet);
        }
    }
}