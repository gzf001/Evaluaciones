using System;
using System.Data.Linq;
namespace Evaluaciones.Membresia
{
	[Serializable]
	public partial class PerfilUsuario : IEquatable<PerfilUsuario>
	{
		public PerfilUsuario()
		{
			this.perfil = default(EntityRef<Evaluaciones.Membresia.Perfil>);
			this.usuario = default(EntityRef<Evaluaciones.Membresia.Usuario>);
		}

		public Int32 PerfilCodigo { get; set; }

		public Guid UsuarioId { get; set; }

		public String Valor { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Membresia.Perfil> perfil;
		public Evaluaciones.Membresia.Perfil Perfil
		{
			get { return this.perfil.Entity; }
			set { this.perfil.Entity = value; }
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
			Context.Instancia.PerfilUsuarios.Attach(this);
		}

		public bool Equals(PerfilUsuario other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.PerfilCodigo.Equals(PerfilCodigo) && other.UsuarioId.Equals(UsuarioId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(PerfilUsuario)) return false;
			return Equals((PerfilUsuario)obj);
		}

		public override int GetHashCode()
		{
			return this.PerfilCodigo.GetHashCode() ^ this.UsuarioId.GetHashCode();
		}
	}
}