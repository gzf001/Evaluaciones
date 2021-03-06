using System;
using System.Data.Linq;
namespace Evaluaciones.Membresia
{
	[Serializable]
	public partial class MenuItemAccion : IEquatable<MenuItemAccion>
	{
		public MenuItemAccion()
		{
			this.accion = default(EntityRef<Evaluaciones.Membresia.Accion>);
			this.menuItem = default(EntityRef<Evaluaciones.Membresia.MenuItem>);
		}

		public Guid AplicacionId { get; set; }

		public Guid MenuId { get; set; }

		public Guid MenuItemId { get; set; }

		public Int32 AccionCodigo { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Membresia.Accion> accion;
		public Evaluaciones.Membresia.Accion Accion
		{
			get { return this.accion.Entity; }
			set { this.accion.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Membresia.MenuItem> menuItem;
		public Evaluaciones.Membresia.MenuItem MenuItem
		{
			get { return this.menuItem.Entity; }
			set { this.menuItem.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.MenuItemAcciones.Attach(this);
		}

		public bool Equals(MenuItemAccion other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.AplicacionId.Equals(AplicacionId) && other.MenuId.Equals(MenuId) && other.MenuItemId.Equals(MenuItemId) && other.AccionCodigo.Equals(AccionCodigo);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(MenuItemAccion)) return false;
			return Equals((MenuItemAccion)obj);
		}

		public override int GetHashCode()
		{
			return this.AplicacionId.GetHashCode() ^ this.MenuId.GetHashCode() ^ this.MenuItemId.GetHashCode() ^ this.AccionCodigo.GetHashCode();
		}
	}
}