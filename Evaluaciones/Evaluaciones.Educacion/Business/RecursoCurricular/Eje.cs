using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Evaluaciones.Educacion.RecursoCurricular
{
    public class Eje : Evaluaciones.Educacion.RecursoCurricular.RecursoCurricular
    {
        public static IEnumerable<SelectListItem> Ejes(int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid aprendizajeId)
        {
            List<Evaluaciones.Educacion.RecursoCurricular.Eje> ejes = Evaluaciones.Educacion.RecursoCurricular.Eje.GetAll(tipoEducacionCodigo, gradoCodigo, DateTime.Today.Year, sectorId, aprendizajeId);

            SelectList lista = new SelectList(ejes, "Id", "Nombre");

            return DefaultItem.Concat(lista);
        }

        public static List<Evaluaciones.Educacion.RecursoCurricular.Eje> GetAll(int tipoEducacionCodigo, int gradoCodigo, int anioNumero, Guid sectorId, Guid aprendizajeId)
        {
            string url = string.Format("{0}/RecursoCurricular/Ejes", System.Configuration.ConfigurationManager.AppSettings["api"]);

            string parametros = string.Format("tipoEducacionCodigo={0}&gradoCodigo={1}&anioNumero={2}&sectorId={3}&aprendizajeId={4}", tipoEducacionCodigo, gradoCodigo, DateTime.Today.Year, sectorId, aprendizajeId);

            Evaluaciones.Educacion.ConexionApi conexionApi = new Evaluaciones.Educacion.ConexionApi();

            string responseFromServer = conexionApi.Conectar(url, parametros, "GET");

            List<Evaluaciones.Educacion.RecursoCurricular.Eje> ejes = new JavaScriptSerializer().Deserialize<List<Evaluaciones.Educacion.RecursoCurricular.Eje>>(responseFromServer);

            return ejes;
        }
    }
}