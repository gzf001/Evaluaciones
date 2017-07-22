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

        //[HttpGet]
        //public JsonResult Contenidos(string tipoEducacionCodigo, string gradoCodigo, string sectorId, string aprendizajeId, string ejeId)
        //{
        //    int t;
        //    int g;
        //    Guid s;
        //    Guid a;

        //    if (int.TryParse(tipoEducacionCodigo, out t) && int.TryParse(gradoCodigo, out g) && Guid.TryParse(sectorId, out s) && Guid.TryParse(unidadId, out u))
        //    {
        //        IEnumerable<SelectListItem> selectList = Evaluaciones.Educacion.RecursoCurricular.Aprendizaje.Aprendizajes(t, g, s, u);

        //        return this.Json(selectList, JsonRequestBehavior.AllowGet);
        //    }

        //    return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        //}
    }
}