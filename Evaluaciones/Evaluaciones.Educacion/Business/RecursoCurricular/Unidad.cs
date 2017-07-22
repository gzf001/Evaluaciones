using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Evaluaciones.Educacion.RecursoCurricular
{
    public class Unidad : Evaluaciones.Educacion.RecursoCurricular.RecursoCurricular
    {
        public static IEnumerable<SelectListItem> Unidades(int tipoEducacionCodigo, int gradoCodigo, int anioNumero, Guid sectorId)
        {
            List<Evaluaciones.Educacion.RecursoCurricular.Unidad> unidades = Evaluaciones.Educacion.RecursoCurricular.Unidad.GetAll(tipoEducacionCodigo, gradoCodigo, DateTime.Today.Year, sectorId);

            SelectList lista = new SelectList(unidades, "Id", "Nombre");

            return DefaultItem.Concat(lista);
        }

        public static List<Evaluaciones.Educacion.RecursoCurricular.Unidad> GetAll(int tipoEducacionCodigo, int gradoCodigo, int anioNumero, Guid sectorId)
        {
            string url = string.Format("{0}/RecursoCurricular/Unidades", System.Configuration.ConfigurationManager.AppSettings["api"]);

            string parametros = string.Format("tipoEducacionCodigo={0}&gradoCodigo={1}&anioNumero={2}&sectorId={3}", tipoEducacionCodigo, gradoCodigo, DateTime.Today.Year, sectorId);

            Evaluaciones.Educacion.ConexionApi conexionApi = new Evaluaciones.Educacion.ConexionApi();

            string responseFromServer = conexionApi.Conectar(url, parametros, "GET");

            List<Evaluaciones.Educacion.RecursoCurricular.Unidad> unidades = new JavaScriptSerializer().Deserialize<List<Evaluaciones.Educacion.RecursoCurricular.Unidad>>(responseFromServer);

            return unidades;
        }
    }
}