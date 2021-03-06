using System;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class EvaluacionPrueba
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			EvaluacionPrueba evaluacionPrueba = context.EvaluacionPruebas.SingleOrDefault<EvaluacionPrueba>(x => x == this);

			if (evaluacionPrueba == null)
			{
				evaluacionPrueba = new EvaluacionPrueba
				{
					EvaluacionId = this.EvaluacionId,
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					PruebaId = this.PruebaId
				};

				context.EvaluacionPruebas.InsertOnSubmit(evaluacionPrueba);
			}

			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			EvaluacionPrueba evaluacionPrueba = context.EvaluacionPruebas.SingleOrDefault<EvaluacionPrueba>(x => x == this);

			if (evaluacionPrueba != null)
			{
				context.EvaluacionPruebas.DeleteOnSubmit(evaluacionPrueba);
			}
			PostDelete(context);
		}
	}
}