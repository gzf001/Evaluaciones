using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Evaluaciones.Educacion.RecursoCurricular
{
    public class Contenido : Evaluaciones.Educacion.RecursoCurricular.RecursoCurricular
    {
        public Guid EjeId
        {
            get;
            set;
        }

        public string Descripcion
        {
            get;
            set;
        }

        public static IEnumerable<SelectListItem> Contenidos(int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid aprendizajeId, Guid ejeId)
        {
            List<Evaluaciones.Educacion.RecursoCurricular.Contenido> contenidos = Evaluaciones.Educacion.RecursoCurricular.Contenido.GetAll(tipoEducacionCodigo, gradoCodigo, DateTime.Today.Year, sectorId, aprendizajeId, ejeId);

            SelectList lista = new SelectList(contenidos, "Id", "Descripcion");

            return DefaultItem.Concat(lista);
        }

        public static List<Evaluaciones.Educacion.RecursoCurricular.Contenido> GetAll(int tipoEducacionCodigo, int gradoCodigo, int anioNumero, Guid sectorId, Guid aprendizajeId, Guid ejeId)
        {
            string url = string.Format("{0}/RecursoCurricular/Contenidos", System.Configuration.ConfigurationManager.AppSettings["api"]);

            string parametros = string.Format("tipoEducacionCodigo={0}&gradoCodigo={1}&anioNumero={2}&sectorId={3}&aprendizajeId={4}&ejeId={5}", tipoEducacionCodigo, gradoCodigo, DateTime.Today.Year, sectorId, aprendizajeId, ejeId);

            Evaluaciones.Educacion.ConexionApi conexionApi = new Evaluaciones.Educacion.ConexionApi();

            string responseFromServer = conexionApi.Conectar(url, parametros, "GET");

            List<Evaluaciones.Educacion.RecursoCurricular.Contenido> contenidos = new JavaScriptSerializer().Deserialize<List<Evaluaciones.Educacion.RecursoCurricular.Contenido>>(responseFromServer);

            return contenidos;
        }
    }
}