using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Evaluaciones.Educacion.BaseCurricular
{
    public class Unidad : Evaluaciones.Default
    {
        public string Nombre
        {
            get;
            set;
        }

        public string Proposito
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

        public static IEnumerable<SelectListItem> Unidades(int tipoEducacionCodigo, int gradoCodigo, Guid sectorId)
        {
            List<Evaluaciones.Educacion.BaseCurricular.Unidad> unidades = Evaluaciones.Educacion.BaseCurricular.Unidad.GetAll(tipoEducacionCodigo, gradoCodigo, sectorId);

            SelectList lista = new SelectList(unidades, "Id", "Nombre");

            return DefaultItem.Concat(lista);
        }

        public static List<Evaluaciones.Educacion.BaseCurricular.Unidad> GetAll(int tipoEducacionCodigo, int gradoCodigo, Guid sectorId)
        {
            string url = string.Format("{0}/BaseCurricular/ejes", System.Configuration.ConfigurationManager.AppSettings["api"]);

            string parametros = string.Format("tipoEducacionCodigo={0}&gradoCodigo={1}&anioNumero={2}&sectorId={3}", tipoEducacionCodigo, gradoCodigo, DateTime.Today.Year, sectorId);

            Evaluaciones.Educacion.ConexionApi conexionApi = new Evaluaciones.Educacion.ConexionApi();

            string responseFromServer = conexionApi.Conectar(url, parametros, "GET");

            List<Evaluaciones.Educacion.BaseCurricular.Unidad> unidades = new JavaScriptSerializer().Deserialize<List<Evaluaciones.Educacion.BaseCurricular.Unidad>>(responseFromServer);

            return unidades;
        }
    }
}
