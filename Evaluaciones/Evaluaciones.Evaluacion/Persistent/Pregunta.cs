using System;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class Pregunta
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Pregunta pregunta = context.Preguntas.SingleOrDefault<Pregunta>(x => x == this);

			if (pregunta == null)
			{
				pregunta = new Pregunta
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					Id = this.Id
				};

				context.Preguntas.InsertOnSubmit(pregunta);
			}

			pregunta.ReferenciaId = this.ReferenciaId == default(Guid) ? null : this.ReferenciaId;
			pregunta.DificultadCodigo = this.DificultadCodigo;
			pregunta.TipoReactivoCodigo = this.TipoReactivoCodigo;
			pregunta.TipoEvaluacionCodigo = this.TipoEvaluacionCodigo;
			pregunta.HabilidadCodigo = this.HabilidadCodigo;
			pregunta.Texto = this.Texto;
			pregunta.Privado = this.Privado;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Pregunta pregunta = context.Preguntas.SingleOrDefault<Pregunta>(x => x == this);

			if (pregunta != null)
			{
				context.Preguntas.DeleteOnSubmit(pregunta);
			}
			PostDelete(context);
		}
	}
}