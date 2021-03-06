using System;
using System.Linq;
namespace Evaluaciones.Membresia
{
	public partial class CentroCostoAplicacion
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			CentroCostoAplicacion centroCostoAplicacion = context.CentroCostoAplicaciones.SingleOrDefault<CentroCostoAplicacion>(x => x == this);

			if (centroCostoAplicacion == null)
			{
				centroCostoAplicacion = new CentroCostoAplicacion
				{
					EmpresaId = this.EmpresaId,
					CentroCostoId = this.CentroCostoId,
					AplicacionId = this.AplicacionId
				};

				context.CentroCostoAplicaciones.InsertOnSubmit(centroCostoAplicacion);
			}

			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			CentroCostoAplicacion centroCostoAplicacion = context.CentroCostoAplicaciones.SingleOrDefault<CentroCostoAplicacion>(x => x == this);

			if (centroCostoAplicacion != null)
			{
				context.CentroCostoAplicaciones.DeleteOnSubmit(centroCostoAplicacion);
			}
			PostDelete(context);
		}
	}
}