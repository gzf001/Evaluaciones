using System;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class Evaluacion
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Evaluacion evaluacion = context.Evaluaciones.SingleOrDefault<Evaluacion>(x => x == this);

			if (evaluacion == null)
			{
				evaluacion = new Evaluacion
				{
					Id = this.Id
				};

				context.Evaluaciones.InsertOnSubmit(evaluacion);
			}

			evaluacion.AnioNumero = this.AnioNumero;
			evaluacion.SectorId = this.SectorId;
			evaluacion.TipoEvaluacionCodigo = this.TipoEvaluacionCodigo;
			evaluacion.AmbitoCodigo = this.AmbitoCodigo;
			evaluacion.SostenedorId = this.SostenedorId == default(Guid) ? null : this.SostenedorId;
			evaluacion.EstablecimientoId = this.EstablecimientoId == default(Guid) ? null : this.EstablecimientoId;
			evaluacion.Nombre = this.Nombre;
			evaluacion.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Evaluacion evaluacion = context.Evaluaciones.SingleOrDefault<Evaluacion>(x => x == this);

			if (evaluacion != null)
			{
				context.Evaluaciones.DeleteOnSubmit(evaluacion);
			}
			PostDelete(context);
		}
	}
}