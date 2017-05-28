using System;
using System.Data.Linq;
namespace Evaluaciones.Membresia
{
	[Serializable]
	public partial class RolAccion : IEquatable<RolAccion>
	{
		public RolAccion()
		{
			this.menuItemAccion = default(EntityRef<Evaluaciones.Membresia.MenuItemAccion>);
			this.rol = default(EntityRef<Evaluaciones.Membresia.Rol>);
		}

		public Guid RolId { get; set; }

		public Guid AplicacionId { get; set; }

		public Guid MenuId { get; set; }

		public Guid MenuItemId { get; set; }

		public Int32 AccionCodigo { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Membresia.MenuItemAccion> menuItemAccion;
		public Evaluaciones.Membresia.MenuItemAccion MenuItemAccion
		{
			get { return this.menuItemAccion.Entity; }
			set { this.menuItemAccion.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Membresia.Rol> rol;
		public Evaluaciones.Membresia.Rol Rol
		{
			get { return this.rol.Entity; }
			set { this.rol.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.RolAcciones.Attach(this);
		}

		public bool Equals(RolAccion other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.RolId.Equals(RolId) && other.AplicacionId.Equals(AplicacionId) && other.MenuId.Equals(MenuId) && other.MenuItemId.Equals(MenuItemId) && other.AccionCodigo.Equals(AccionCodigo);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(RolAccion)) return false;
			return Equals((RolAccion)obj);
		}

		public override int GetHashCode()
		{
			return this.RolId.GetHashCode() ^ this.AplicacionId.GetHashCode() ^ this.MenuId.GetHashCode() ^ this.MenuItemId.GetHashCode() ^ this.AccionCodigo.GetHashCode();
		}
	}
}