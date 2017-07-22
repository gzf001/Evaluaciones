using System;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class PreguntaRecursoCurricular
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			PreguntaRecursoCurricular preguntaRecursoCurricular = context.PreguntaRecursoCurriculares.SingleOrDefault<PreguntaRecursoCurricular>(x => x == this);

			if (preguntaRecursoCurricular == null)
			{
				preguntaRecursoCurricular = new PreguntaRecursoCurricular
				{
					PreguntaId = this.PreguntaId
				};

				context.PreguntaRecursoCurriculares.InsertOnSubmit(preguntaRecursoCurricular);
			}

			preguntaRecursoCurricular.TipoEducacionCodigo = this.TipoEducacionCodigo == default(Int32) ? null : this.TipoEducacionCodigo;
			preguntaRecursoCurricular.GradoCodigo = this.GradoCodigo == default(Int32) ? null : this.GradoCodigo;
			preguntaRecursoCurricular.SectorId = this.SectorId;
			preguntaRecursoCurricular.UnidadId = this.UnidadId;
			preguntaRecursoCurricular.AprendizajeId = this.AprendizajeId;
			preguntaRecursoCurricular.EjeId = this.EjeId;
			preguntaRecursoCurricular.ContenidoId = this.ContenidoId;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			PreguntaRecursoCurricular preguntaRecursoCurricular = context.PreguntaRecursoCurriculares.SingleOrDefault<PreguntaRecursoCurricular>(x => x == this);

			if (preguntaRecursoCurricular != null)
			{
				context.PreguntaRecursoCurriculares.DeleteOnSubmit(preguntaRecursoCurricular);
			}
			PostDelete(context);
		}
	}
}