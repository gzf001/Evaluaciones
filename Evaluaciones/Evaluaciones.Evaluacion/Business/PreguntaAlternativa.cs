using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones.Evaluacion
{
	public partial class PreguntaAlternativa : Evaluaciones.Default
    {
        public static IEnumerable<SelectListItem> Alternativas
        {
            get
            {
                SelectList lista = new SelectList(Evaluaciones.Evaluacion.PreguntaAlternativa.Letras());

                return DefaultItem.Concat(lista);
            }
        }

        public static List<char> Letras()
        {
            List<char> chars = new List<char>();

            chars.Add('A');
            chars.Add('B');
            chars.Add('C');
            chars.Add('D');
            chars.Add('E');

            return chars;
        }

		public static List<PreguntaAlternativa> GetAll()
		{
			return
				(
				from query in Query.GetPreguntaAlternativas()
				select query
				).ToList<PreguntaAlternativa>();
		}
	}
}