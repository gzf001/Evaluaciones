using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Evaluaciones.Educacion.RecursoCurricular
{
    public class Aprendizaje : Evaluaciones.Educacion.RecursoCurricular.RecursoCurricular
    {
        public string Descripcion
        {
            get;
            set;
        }

        public static IEnumerable<SelectListItem> Aprendizajes(int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid unidadId)
        {
            List<Evaluaciones.Educacion.RecursoCurricular.Aprendizaje> aprendizaje = Evaluaciones.Educacion.RecursoCurricular.Aprendizaje.GetAll(tipoEducacionCodigo, gradoCodigo, DateTime.Today.Year, sectorId, unidadId);

            SelectList lista = new SelectList(aprendizaje, "Id", "Descripcion");

            return DefaultItem.Concat(lista);
        }

        public static List<Evaluaciones.Educacion.RecursoCurricular.Aprendizaje> GetAll(int tipoEducacionCodigo, int gradoCodigo, int anioNumero, Guid sectorId, Guid unidadId)
        {
            string url = string.Format("{0}/RecursoCurricular/Aprendizajes", System.Configuration.ConfigurationManager.AppSettings["api"]);

            string parametros = string.Format("tipoEducacionCodigo={0}&gradoCodigo={1}&anioNumero={2}&sectorId={3}&unidadId={4}", tipoEducacionCodigo, gradoCodigo, DateTime.Today.Year, sectorId, unidadId);

            Evaluaciones.Educacion.ConexionApi conexionApi = new Evaluaciones.Educacion.ConexionApi();

            string responseFromServer = conexionApi.Conectar(url, parametros, "GET");

            List<Evaluaciones.Educacion.RecursoCurricular.Aprendizaje> aprendizajes = new JavaScriptSerializer().Deserialize<List<Evaluaciones.Educacion.RecursoCurricular.Aprendizaje>>(responseFromServer);

            return aprendizajes;
        }
    }
}
