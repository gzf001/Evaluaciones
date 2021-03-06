using System;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class PreguntaVerdaderoFalso
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			PreguntaVerdaderoFalso preguntaVerdaderoFalso = context.PreguntaVerdaderoFalsos.SingleOrDefault<PreguntaVerdaderoFalso>(x => x == this);

			if (preguntaVerdaderoFalso == null)
			{
				preguntaVerdaderoFalso = new PreguntaVerdaderoFalso
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					PreguntaId = this.PreguntaId
				};

				context.PreguntaVerdaderoFalsos.InsertOnSubmit(preguntaVerdaderoFalso);
			}

			preguntaVerdaderoFalso.Respuesta = this.Respuesta;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			PreguntaVerdaderoFalso preguntaVerdaderoFalso = context.PreguntaVerdaderoFalsos.SingleOrDefault<PreguntaVerdaderoFalso>(x => x == this);

			if (preguntaVerdaderoFalso != null)
			{
				context.PreguntaVerdaderoFalsos.DeleteOnSubmit(preguntaVerdaderoFalso);
			}
			PostDelete(context);
		}
	}
}