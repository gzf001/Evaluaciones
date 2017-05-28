using System;
using System.Data.Linq;
namespace Evaluaciones.Membresia
{
	[Serializable]
	public partial class Auditoria : IEquatable<Auditoria>
	{
		public Auditoria()
		{
			this.Id = Guid.NewGuid();
			this.aplicacion = default(EntityRef<Evaluaciones.Membresia.Aplicacion>);
			this.menuItem = default(EntityRef<Evaluaciones.Membresia.MenuItem>);
			this.usuario = default(EntityRef<Evaluaciones.Membresia.Usuario>);
		}

		public Guid Id { get; set; }

		public Guid UsuarioId { get; set; }

		public Guid AplicacionId { get; set; }

		public Guid MenuId { get; set; }

		public Guid MenuItemId { get; set; }

		public String Actividad { get; set; }

		public DateTime Fecha { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Membresia.Aplicacion> aplicacion;
		public Evaluaciones.Membresia.Aplicacion Aplicacion
		{
			get { return this.aplicacion.Entity; }
			set { this.aplicacion.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Membresia.MenuItem> menuItem;
		public Evaluaciones.Membresia.MenuItem MenuItem
		{
			get { return this.menuItem.Entity; }
			set { this.menuItem.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Membresia.Usuario> usuario;
		public Evaluaciones.Membresia.Usuario Usuario
		{
			get { return this.usuario.Entity; }
			set { this.usuario.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Auditorias.Attach(this);
		}

		public bool Equals(Auditoria other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id) && other.UsuarioId.Equals(UsuarioId) && other.AplicacionId.Equals(AplicacionId) && other.MenuId.Equals(MenuId) && other.MenuItemId.Equals(MenuItemId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Auditoria)) return false;
			return Equals((Auditoria)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode() ^ this.UsuarioId.GetHashCode() ^ this.AplicacionId.GetHashCode() ^ this.MenuId.GetHashCode() ^ this.MenuItemId.GetHashCode();
		}
	}
}