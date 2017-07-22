using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Evaluaciones.Educacion
{
    public class Sector
    {
        public Guid Id
        {
            get;
            set;
        }

        public string Nombre
        {
            get;
            set;
        }

        public static IEnumerable<SelectListItem> DefaultItem
        {
            get
            {
                IEnumerable<SelectListItem> defaultItem = Enumerable.Repeat(new SelectListItem
                {
                    Value = "-1",
                    Text = "[Seleccione]"
                }, count: 1);

                return defaultItem;
            }
        }

        public static IEnumerable<SelectListItem> Sectores(int tipoEducacionCodigo, int gradoCodigo)
        {
            Evaluaciones.Educacion.TipoEducacion tipoEducacion = Evaluaciones.Educacion.TipoEducacion.Get(tipoEducacionCodigo);

            List<Evaluaciones.Educacion.Sector> sectores = Evaluaciones.Educacion.Sector.GetAll(tipoEducacionCodigo, gradoCodigo);

            SelectList lista = new SelectList(sectores, "Id", "Nombre");

            return Evaluaciones.Educacion.Sector.DefaultItem.Concat(lista);
        }

        public static List<Evaluaciones.Educacion.Sector> GetAll(int tipoEducacionCodigo, int gradoCodigo)
        {
            string url = string.Format("{0}/educacion/sectores", System.Configuration.ConfigurationManager.AppSettings["api"]);

            string parametros = string.Format("tipoEducacionCodigo={0}&gradoCodigo={1}&anioNumero={2}", tipoEducacionCodigo, gradoCodigo, DateTime.Today.Year);

            System.Net.WebRequest wr = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(string.Format("{0}?{1}", url, parametros));

            wr.Method = "GET";
            wr.ContentType = "application/x-www-form-urlencoded";

            System.IO.Stream newStream;

            // Obtiene la respuesta
            System.Net.WebResponse response = wr.GetResponse();

            // Stream con el contenido recibido del servidor
            newStream = response.GetResponseStream();

            System.IO.StreamReader reader = new System.IO.StreamReader(newStream);

            // Leemos el contenido
            string responseFromServer = reader.ReadToEnd();

            // Cerramos los streams
            reader.Close();
            newStream.Close();
            response.Close();

            List<Sector> sectores = new JavaScriptSerializer().Deserialize<List<Evaluaciones.Educacion.Sector>>(responseFromServer);

            return sectores;
        }
    }
}