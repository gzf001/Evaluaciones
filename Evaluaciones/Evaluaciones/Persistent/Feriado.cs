using System;
using System.Linq;
namespace Evaluaciones
{
	public partial class Feriado
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Feriado feriado = context.Feriados.SingleOrDefault<Feriado>(x => x == this);

			if (feriado == null)
			{
				feriado = new Feriado
				{
					Id = this.Id
				};

				context.Feriados.InsertOnSubmit(feriado);
			}

			feriado.EmpresaId = this.EmpresaId == default(Guid) ? null : this.EmpresaId;
			feriado.CentroCostoId = this.CentroCostoId == default(Guid) ? null : this.CentroCostoId;
			feriado.Fecha = this.Fecha;
			feriado.Nombre = this.Nombre;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Feriado feriado = context.Feriados.SingleOrDefault<Feriado>(x => x == this);

			if (feriado != null)
			{
				context.Feriados.DeleteOnSubmit(feriado);
			}
			PostDelete(context);
		}
	}
}