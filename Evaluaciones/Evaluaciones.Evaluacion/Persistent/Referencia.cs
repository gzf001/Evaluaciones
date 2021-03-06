using System;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class Referencia
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Referencia referencia = context.Referencias.SingleOrDefault<Referencia>(x => x == this);

			if (referencia == null)
			{
				referencia = new Referencia
				{
					TipoEducacionCodigo = this.TipoEducacionCodigo,
					GradoCodigo = this.GradoCodigo,
					SectorId = this.SectorId,
					Id = this.Id
				};

				context.Referencias.InsertOnSubmit(referencia);
			}

			referencia.Texto = this.Texto;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Referencia referencia = context.Referencias.SingleOrDefault<Referencia>(x => x == this);

			if (referencia != null)
			{
				context.Referencias.DeleteOnSubmit(referencia);
			}
			PostDelete(context);
		}
	}
}