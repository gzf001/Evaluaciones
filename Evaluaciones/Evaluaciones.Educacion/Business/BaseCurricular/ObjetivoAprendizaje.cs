using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Evaluaciones.Educacion.BaseCurricular
{
    public class ObjetivoAprendizaje : Evaluaciones.Default
    {
        public Guid EjeId
        {
            get;
            set;
        }

        public int Numero
        {
            get;
            set;
        }

        public string Descripcion
        {
            get;
            set;
        }

        public int TipoEducacionCodigo
        {
            get;
            set;
        }

        public int GradoCodigo
        {
            get;
            set;
        }

        public int AnioNumero
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

        public static IEnumerable<SelectListItem> ObjetivosAprendizaje(int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid unidadId, Guid ejeId)
        {
            List<Evaluaciones.Educacion.BaseCurricular.ObjetivoAprendizaje> ejes = Evaluaciones.Educacion.BaseCurricular.ObjetivoAprendizaje.GetAll(tipoEducacionCodigo, gradoCodigo, sectorId, unidadId, ejeId);

            SelectList lista = new SelectList(ejes, "Id", "Descripcion");

            return DefaultItem.Concat(lista);
        }

        public static List<Evaluaciones.Educacion.BaseCurricular.ObjetivoAprendizaje> GetAll(int tipoEducacionCodigo, int gradoCodigo, Guid sectorId, Guid unidadId, Guid ejeId)
        {
            string url = string.Format("{0}/BaseCurricular/ObjetivoAprendizaje", System.Configuration.ConfigurationManager.AppSettings["api"]);

            string parametros = string.Format("tipoEducacionCodigo={0}&gradoCodigo={1}&anioNumero={2}&sectorId={3}&unidadId={4}&ejeId={5}", tipoEducacionCodigo, gradoCodigo, DateTime.Today.Year, sectorId, unidadId, ejeId);

            Evaluaciones.Educacion.ConexionApi conexionApi = new Evaluaciones.Educacion.ConexionApi();

            string responseFromServer = conexionApi.Conectar(url, parametros, "GET");

            List<Evaluaciones.Educacion.BaseCurricular.ObjetivoAprendizaje> objetivosAprendizaje = new JavaScriptSerializer().Deserialize<List<Evaluaciones.Educacion.BaseCurricular.ObjetivoAprendizaje>>(responseFromServer);

            return objetivosAprendizaje;
        }
    }
}
