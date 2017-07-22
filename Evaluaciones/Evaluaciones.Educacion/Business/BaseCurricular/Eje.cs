using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Evaluaciones.Educacion.BaseCurricular
{
    public class Eje : Evaluaciones.Default
    {
        public string Nombre
        {
            get;
            set;
        }

        public int Numero
        {
            get;
            set;
        }

        public int TipoEducacionCodigo
        {
            get;
            set;
        }

        public string GradoCodigo
        {
            get;
            set;
        }

        public string AnioNumero
        {
            get;
            set;
        }

        public Guid SectorId
        {
            get;
            set;
        }

        public Guid Id
        {
            get;
            set;
        }

        public static IEnumerable<SelectListItem> Ejes(int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid unidadId)
        {
            List<Evaluaciones.Educacion.BaseCurricular.Eje> ejes = Evaluaciones.Educacion.BaseCurricular.Eje.GetAll(tipoEducacionCodigo, gradoCodigo, sectorId, unidadId);

            SelectList lista = new SelectList(ejes, "Id", "Nombre");

            return DefaultItem.Concat(lista);
        }

        public static List<Evaluaciones.Educacion.BaseCurricular.Eje> GetAll(int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid unidadId)
        {
            string url = string.Format("{0}/BaseCurricular/ejes", System.Configuration.ConfigurationManager.AppSettings["api"]);

            string parametros = string.Format("tipoEducacionCodigo={0}&gradoCodigo={1}&anioNumero={2}&sectorId={3}&unidadId={4}", tipoEducacionCodigo, gradoCodigo, DateTime.Today.Year, sectorId, unidadId);

            Evaluaciones.Educacion.ConexionApi conexionApi = new Evaluaciones.Educacion.ConexionApi();

            string responseFromServer = conexionApi.Conectar(url, parametros, "GET");

            List<Evaluaciones.Educacion.BaseCurricular.Eje> ejes = new JavaScriptSerializer().Deserialize<List<Evaluaciones.Educacion.BaseCurricular.Eje>>(responseFromServer);

            return ejes;
        }
    }
}
