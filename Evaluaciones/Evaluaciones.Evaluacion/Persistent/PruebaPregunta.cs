using System;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class PruebaPregunta
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			PruebaPregunta pruebaPregunta = context.PruebaPreguntas.SingleOrDefault<PruebaPregunta>(x => x == this);

			if (pruebaPregunta == null)
			{
				pruebaPregunta = new PruebaPregunta
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					PruebaId = this.PruebaId,
					PreguntaTipoEducacionCodigo = this.PreguntaTipoEducacionCodigo,
					PreguntaGradoCodigo = this.PreguntaGradoCodigo,
					PreguntaId = this.PreguntaId
				};

				context.PruebaPreguntas.InsertOnSubmit(pruebaPregunta);
			}

			pruebaPregunta.Nula = this.Nula;
			pruebaPregunta.Orden = this.Orden;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			PruebaPregunta pruebaPregunta = context.PruebaPreguntas.SingleOrDefault<PruebaPregunta>(x => x == this);

			if (pruebaPregunta != null)
			{
				context.PruebaPreguntas.DeleteOnSubmit(pruebaPregunta);
			}
			PostDelete(context);
		}
	}
}