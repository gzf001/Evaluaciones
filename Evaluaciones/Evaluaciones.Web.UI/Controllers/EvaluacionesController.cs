using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Web.UI.Controllers
{
    public class EvaluacionesController : Evaluaciones.Web.Controller
    {
        #region Ciudades y comunas

        [Authorize]
        [HttpGet]
        public JsonResult Ciudades(string regionCodigo)
        {
            short codigo;

            if (short.TryParse(regionCodigo, out codigo))
            {
                IEnumerable<SelectListItem> selectList = Evaluaciones.Ciudad.Ciudades(codigo);

                return this.Json(selectList, JsonRequestBehavior.AllowGet); 
            }

            return this.Json(Enumerable.Repeat(new SelectListItem
            {
                Value = "-1",
                Text = "[Seleccione]"
            }, count: 1), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult Comunas(string regionCodigo, string ciudadCodigo)
        {
            short c;
            short r;

            if (short.TryParse(regionCodigo, out r) && short.TryParse(ciudadCodigo, out c))
            {
                IEnumerable<SelectListItem> selectList = Evaluaciones.Comuna.Comunas(r, c);

                return this.Json(selectList, JsonRequestBehavior.AllowGet);
            }

            return this.Json(Enumerable.Repeat(new SelectListItem
            {
                Value = "-1",
                Text = "[Seleccione]"
            }, count: 1), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Centros de costo

        [Authorize]
        [HttpGet]
        public JsonResult CentrosCosto(Guid? empresaId)
        {
            if (empresaId.HasValue)
            {
                IEnumerable<SelectListItem> selectList = Evaluaciones.CentroCosto.CentrosCosto(empresaId.Value);

                return this.Json(selectList, JsonRequestBehavior.AllowGet);
            }

            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}