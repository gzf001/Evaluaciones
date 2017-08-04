using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Web.UI.Controllers
{
    public class EducacionController : Evaluaciones.Web.Controller
    {
        [HttpGet]
        public JsonResult TipoEducaciones(string tipoEducacion)
        {
            int tipoEducacionCodigo;

            if (int.TryParse(tipoEducacion, out tipoEducacionCodigo))
            {
                IEnumerable<SelectListItem> selectList = Evaluaciones.Educacion.TipoEducacion.GetTipoEducaciones(tipoEducacionCodigo);

                return this.Json(selectList, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        //[Authorize]
        [HttpGet]
        public JsonResult Grados(string tipoEducacion)
        {
            int tipoEducacionCodigo;

            if (int.TryParse(tipoEducacion, out tipoEducacionCodigo))
            {
                IEnumerable<SelectListItem> selectList = Evaluaciones.Educacion.Grado.Grados(tipoEducacionCodigo);

                return this.Json(selectList, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FiltroGrados(string tipoEducacion, string grado)
        {
            int tipoEducacionCodigo;
            int gradoCodigo;

            if (int.TryParse(tipoEducacion, out tipoEducacionCodigo) && int.TryParse(grado, out gradoCodigo))
            {
                IEnumerable<SelectListItem> selectList = Evaluaciones.Educacion.Grado.Grados(tipoEducacionCodigo, gradoCodigo);

                return this.Json(selectList, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Sectores(string tipoEducacion, string grado)
        {
            int tipoEducacionCodigo;
            int gradoCodigo;

            if (int.TryParse(tipoEducacion, out tipoEducacionCodigo) && int.TryParse(grado, out gradoCodigo))
            {
                IEnumerable<SelectListItem> sectores = Evaluaciones.Educacion.Sector.Sectores(tipoEducacionCodigo, gradoCodigo);

                return this.Json(sectores, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BaseCurricular()
        {
            return this.Json(Evaluaciones.Educacion.Grado.GetBaseCurricular(), JsonRequestBehavior.AllowGet);
        }
    }
}