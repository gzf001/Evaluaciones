using System;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class EmpresaPregunta
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			EmpresaPregunta empresaPregunta = context.EmpresaPreguntas.SingleOrDefault<EmpresaPregunta>(x => x == this);

			if (empresaPregunta == null)
			{
				empresaPregunta = new EmpresaPregunta
				{
					EmpresaId = this.EmpresaId,
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					PreguntaId = this.PreguntaId,
					Id = this.Id
				};

				context.EmpresaPreguntas.InsertOnSubmit(empresaPregunta);
			}

			empresaPregunta.Fecha = this.Fecha;
			empresaPregunta.Detalle = this.Detalle;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			EmpresaPregunta empresaPregunta = context.EmpresaPreguntas.SingleOrDefault<EmpresaPregunta>(x => x == this);

			if (empresaPregunta != null)
			{
				context.EmpresaPreguntas.DeleteOnSubmit(empresaPregunta);
			}
			PostDelete(context);
		}
	}
}