using System;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class PreguntaBaseCurricular
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			PreguntaBaseCurricular preguntaBaseCurricular = context.PreguntaBaseCurriculares.SingleOrDefault<PreguntaBaseCurricular>(x => x == this);

			if (preguntaBaseCurricular == null)
			{
				preguntaBaseCurricular = new PreguntaBaseCurricular
				{
					PreguntaId = this.PreguntaId
				};

				context.PreguntaBaseCurriculares.InsertOnSubmit(preguntaBaseCurricular);
			}

			preguntaBaseCurricular.TipoEducacionCodigo = this.TipoEducacionCodigo == default(Int32) ? null : this.TipoEducacionCodigo;
			preguntaBaseCurricular.GradoCodigo = this.GradoCodigo == default(Int32) ? null : this.GradoCodigo;
			preguntaBaseCurricular.AnioNumero = this.AnioNumero == default(Int32) ? null : this.AnioNumero;
			preguntaBaseCurricular.SectorId = this.SectorId == default(Guid) ? null : this.SectorId;
			preguntaBaseCurricular.UnidadId = this.UnidadId == default(Guid) ? null : this.UnidadId;
			preguntaBaseCurricular.EjeId = this.EjeId == default(Guid) ? null : this.EjeId;
			preguntaBaseCurricular.ObjetivoAprendizajeId = this.ObjetivoAprendizajeId == default(Guid) ? null : this.ObjetivoAprendizajeId;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			PreguntaBaseCurricular preguntaBaseCurricular = context.PreguntaBaseCurriculares.SingleOrDefault<PreguntaBaseCurricular>(x => x == this);

			if (preguntaBaseCurricular != null)
			{
				context.PreguntaBaseCurriculares.DeleteOnSubmit(preguntaBaseCurricular);
			}
			PostDelete(context);
		}
	}
}