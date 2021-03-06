using System;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class PreguntaAlternativa
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			PreguntaAlternativa preguntaAlternativa = context.PreguntaAlternativas.SingleOrDefault<PreguntaAlternativa>(x => x == this);

			if (preguntaAlternativa == null)
			{
				preguntaAlternativa = new PreguntaAlternativa
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					PreguntaId = this.PreguntaId
				};

				context.PreguntaAlternativas.InsertOnSubmit(preguntaAlternativa);
			}

			preguntaAlternativa.AlternativaCorrecta = this.AlternativaCorrecta;
			preguntaAlternativa.AlternativaA = this.AlternativaA;
			preguntaAlternativa.AlternativaB = this.AlternativaB;
			preguntaAlternativa.AlternativaC = this.AlternativaC;
			preguntaAlternativa.AlternativaD = this.AlternativaD;
			preguntaAlternativa.AlternativaE = this.AlternativaE;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			PreguntaAlternativa preguntaAlternativa = context.PreguntaAlternativas.SingleOrDefault<PreguntaAlternativa>(x => x == this);

			if (preguntaAlternativa != null)
			{
				context.PreguntaAlternativas.DeleteOnSubmit(preguntaAlternativa);
			}
			PostDelete(context);
		}
	}
}