using System;
using System.Linq;
namespace Evaluaciones.Membresia
{
	public partial class RolPersonaEmpresa
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			RolPersonaEmpresa rolPersonaEmpresa = context.RolPersonaEmpresas.SingleOrDefault<RolPersonaEmpresa>(x => x == this);

			if (rolPersonaEmpresa == null)
			{
				rolPersonaEmpresa = new RolPersonaEmpresa
				{
					RolId = this.RolId,
					PersonaId = this.PersonaId,
					EmpresaId = this.EmpresaId
				};

				context.RolPersonaEmpresas.InsertOnSubmit(rolPersonaEmpresa);
			}

			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			RolPersonaEmpresa rolPersonaEmpresa = context.RolPersonaEmpresas.SingleOrDefault<RolPersonaEmpresa>(x => x == this);

			if (rolPersonaEmpresa != null)
			{
				context.RolPersonaEmpresas.DeleteOnSubmit(rolPersonaEmpresa);
			}
			PostDelete(context);
		}
	}
}