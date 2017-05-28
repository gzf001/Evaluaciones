using System;
using System.Linq;
namespace Evaluaciones.Membresia
{
	public partial class Aplicacion
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Aplicacion aplicacion = context.Aplicaciones.SingleOrDefault<Aplicacion>(x => x == this);

			if (aplicacion == null)
			{
				aplicacion = new Aplicacion
				{
					Id = this.Id
				};

				context.Aplicaciones.InsertOnSubmit(aplicacion);
			}

			aplicacion.MenuId = this.MenuId == default(Guid) ? null : this.MenuId;
			aplicacion.MenuItemId = this.MenuItemId == default(Guid) ? null : this.MenuItemId;
			aplicacion.Nombre = this.Nombre;
			aplicacion.Clave = this.Clave;
			aplicacion.Orden = this.Orden;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Aplicacion aplicacion = context.Aplicaciones.SingleOrDefault<Aplicacion>(x => x == this);

			if (aplicacion != null)
			{
				context.Aplicaciones.DeleteOnSubmit(aplicacion);
			}
			PostDelete(context);
		}
	}
}