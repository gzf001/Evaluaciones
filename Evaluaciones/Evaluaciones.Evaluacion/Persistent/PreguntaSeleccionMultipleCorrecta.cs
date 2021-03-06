using System;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class PreguntaSeleccionMultipleCorrecta
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			PreguntaSeleccionMultipleCorrecta preguntaSeleccionMultipleCorrecta = context.PreguntaSeleccionMultipleCorrectas.SingleOrDefault<PreguntaSeleccionMultipleCorrecta>(x => x == this);

			if (preguntaSeleccionMultipleCorrecta == null)
			{
				preguntaSeleccionMultipleCorrecta = new PreguntaSeleccionMultipleCorrecta
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					PreguntaId = this.PreguntaId,
					Id = this.Id
				};

				context.PreguntaSeleccionMultipleCorrectas.InsertOnSubmit(preguntaSeleccionMultipleCorrecta);
			}

			preguntaSeleccionMultipleCorrecta.Correcta = this.Correcta;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			PreguntaSeleccionMultipleCorrecta preguntaSeleccionMultipleCorrecta = context.PreguntaSeleccionMultipleCorrectas.SingleOrDefault<PreguntaSeleccionMultipleCorrecta>(x => x == this);

			if (preguntaSeleccionMultipleCorrecta != null)
			{
				context.PreguntaSeleccionMultipleCorrectas.DeleteOnSubmit(preguntaSeleccionMultipleCorrecta);
			}
			PostDelete(context);
		}
	}
}