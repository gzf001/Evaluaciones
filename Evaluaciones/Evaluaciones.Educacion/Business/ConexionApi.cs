using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluaciones.Educacion
{
    public class ConexionApi
    {
        /// <summary>
        /// Retorna un string de resultado proveniente de la conexión a la API de Insignia. El parámetro método corresponde a un verbo http (GET, POST, etc)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parametros"></param>
        /// <param name="metodo"></param>
        /// <returns></returns>
        public string Conectar(string url, string parametros, string metodo)
        {
            System.Net.WebRequest wr = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(string.Format("{0}?{1}", url, parametros));

            wr.Method = metodo.ToUpper();
            wr.ContentType = "application/x-www-form-urlencoded";

            string responseFromServer;

            using (System.Net.WebResponse response = wr.GetResponse())
            {
                using (System.IO.Stream stream = response.GetResponseStream())
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                    {
                        responseFromServer = reader.ReadToEnd();
                    }
                }
            }

            return responseFromServer;
        }
    }
}
