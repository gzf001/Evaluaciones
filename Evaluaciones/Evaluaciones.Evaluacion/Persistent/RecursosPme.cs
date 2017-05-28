using System;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class RecursosPme
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			RecursosPme recursosPme = context.RecursosPmes.SingleOrDefault<RecursosPme>(x => x == this);

			if (recursosPme == null)
			{
				recursosPme = new RecursosPme
				{
					PreguntaId = this.PreguntaId
				};

				context.RecursosPmes.InsertOnSubmit(recursosPme);
			}

			recursosPme.SectorId = this.SectorId;
			recursosPme.TipoEducacionCodigo = this.TipoEducacionCodigo == default(Int32) ? null : this.TipoEducacionCodigo;
			recursosPme.GradoCodigo = this.GradoCodigo == default(Int32) ? null : this.GradoCodigo;
			recursosPme.AprendizajeId = this.AprendizajeId == default(Guid) ? null : this.AprendizajeId;
			recursosPme.AprendizajeIndicadorId = this.AprendizajeIndicadorId == default(Guid) ? null : this.AprendizajeIndicadorId;
			recursosPme.HabilidadId = this.HabilidadId == default(Guid) ? null : this.HabilidadId;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			RecursosPme recursosPme = context.RecursosPmes.SingleOrDefault<RecursosPme>(x => x == this);

			if (recursosPme != null)
			{
				context.RecursosPmes.DeleteOnSubmit(recursosPme);
			}
			PostDelete(context);
		}
	}
}