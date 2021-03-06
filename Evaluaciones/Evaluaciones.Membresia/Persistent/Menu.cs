using System;
using System.Linq;
namespace Evaluaciones.Membresia
{
	public partial class Menu
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Menu menu = context.Menus.SingleOrDefault<Menu>(x => x == this);

			if (menu == null)
			{
				menu = new Menu
				{
					Id = this.Id
				};

				context.Menus.InsertOnSubmit(menu);
			}

			menu.Nombre = this.Nombre;
			menu.Clave = this.Clave;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Menu menu = context.Menus.SingleOrDefault<Menu>(x => x == this);

			if (menu != null)
			{
				context.Menus.DeleteOnSubmit(menu);
			}
			PostDelete(context);
		}
	}
}