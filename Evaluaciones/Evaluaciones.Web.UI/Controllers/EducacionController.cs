using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Web.UI.Controllers
{
    public class EducacionController : Evaluaciones.Web.Controller
    {
        //[Authorize]
        [HttpGet]
        public JsonResult Grados(string tipoEducacion)
        {
            int tipoEducacionCodigo;

            IEnumerable<SelectListItem> defaultItem = Enumerable.Repeat(new SelectListItem
            {
                Value = "-1",
                Text = "[Seleccione]"
            }, count: 1);

            if (int.TryParse(tipoEducacion, out tipoEducacionCodigo))
            {
                Evaluaciones.Educacion.TipoEducacion t = Evaluaciones.Educacion.TipoEducacion.Get(tipoEducacionCodigo);

                List<Evaluaciones.Educacion.Grado> grados = Evaluaciones.Educacion.Grado.GetAll(t);

                SelectList selectList = new SelectList(grados, "Codigo", "Nombre");

                return this.Json(defaultItem.Concat(selectList), JsonRequestBehavior.AllowGet);
            }

            return this.Json(defaultItem, JsonRequestBehavior.AllowGet);
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
    }
}