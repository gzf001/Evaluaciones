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
            IEnumerable<SelectListItem> defaultItem = Enumerable.Repeat(new SelectListItem
            {
                Value = "-1",
                Text = "[Seleccione]"
            }, count: 1);

            short codigo;

            if (!short.TryParse(regionCodigo, out codigo))
            {
                return this.Json(defaultItem, JsonRequestBehavior.AllowGet);
            }

            Evaluaciones.Region region = Evaluaciones.Region.Get(codigo);

            List<Evaluaciones.Ciudad> ciudades = Evaluaciones.Ciudad.GetAll(region);

            SelectList selectList = new SelectList(ciudades, "Codigo", "Nombre");

            return this.Json(defaultItem.Concat(selectList), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult Comunas(string regionCodigo, string ciudadCodigo)
        {
            IEnumerable<SelectListItem> defaultItem = Enumerable.Repeat(new SelectListItem
            {
                Value = "-1",
                Text = "[Seleccione]"
            }, count: 1);

            short codigo;

            if (!short.TryParse(ciudadCodigo, out codigo))
            {
                return this.Json(defaultItem, JsonRequestBehavior.AllowGet);
            }

            Evaluaciones.Ciudad ciudad = Evaluaciones.Ciudad.Get(short.Parse(regionCodigo), codigo);

            List<Evaluaciones.Comuna> comunas = Evaluaciones.Comuna.GetAll(ciudad);

            SelectList selectList = new SelectList(comunas, "Codigo", "Nombre");

            return this.Json(defaultItem.Concat(selectList), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Centros de costo

        [Authorize]
        [HttpGet]
        public JsonResult CentrosCosto(Guid? empresaId)
        {
            IEnumerable<SelectListItem> defaultItem = Enumerable.Repeat(new SelectListItem
            {
                Value = "-1",
                Text = "[Seleccione]"
            }, count: 1);
            
            if (empresaId.HasValue)
            {
                Evaluaciones.Empresa empresa = Evaluaciones.Empresa.Get(empresaId.Value);

                List<Evaluaciones.CentroCosto> centroCosto = Evaluaciones.CentroCosto.GetAll(empresa);

                SelectList selectList = new SelectList(centroCosto, "Id", "Nombre");

                return this.Json(defaultItem.Concat(selectList), JsonRequestBehavior.AllowGet);
            }

            return this.Json(defaultItem, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}