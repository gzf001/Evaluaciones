using System;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class PreguntaSeleccionMultiple
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			PreguntaSeleccionMultiple preguntaSeleccionMultiple = context.PreguntaSeleccionMultiples.SingleOrDefault<PreguntaSeleccionMultiple>(x => x == this);

			if (preguntaSeleccionMultiple == null)
			{
				preguntaSeleccionMultiple = new PreguntaSeleccionMultiple
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					PreguntaId = this.PreguntaId
				};

				context.PreguntaSeleccionMultiples.InsertOnSubmit(preguntaSeleccionMultiple);
			}

			preguntaSeleccionMultiple.AlternativaA = this.AlternativaA;
			preguntaSeleccionMultiple.AlternativaB = this.AlternativaB;
			preguntaSeleccionMultiple.AlternativaC = this.AlternativaC;
			preguntaSeleccionMultiple.AlternativaD = this.AlternativaD;
			preguntaSeleccionMultiple.AlternativaE = this.AlternativaE;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			PreguntaSeleccionMultiple preguntaSeleccionMultiple = context.PreguntaSeleccionMultiples.SingleOrDefault<PreguntaSeleccionMultiple>(x => x == this);

			if (preguntaSeleccionMultiple != null)
			{
				context.PreguntaSeleccionMultiples.DeleteOnSubmit(preguntaSeleccionMultiple);
			}
			PostDelete(context);
		}
	}
}