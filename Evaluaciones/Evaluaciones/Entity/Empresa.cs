using System;
using System.Data.Linq;
namespace Evaluaciones
{
	[Serializable]
	public partial class Empresa : IEquatable<Empresa>
	{
		public Empresa()
		{
			this.Id = Guid.NewGuid();
			this.comuna = default(EntityRef<Evaluaciones.Comuna>);
		}

		public Guid Id { get; set; }

		public String Rut { get; set; }

		public Int32 RutCuerpo { get; set; }

		public Char RutDigito { get; set; }

		public String RazonSocial { get; set; }

		public Nullable<Int16> RegionCodigo { get; set; }

		public Nullable<Int16> CiudadCodigo { get; set; }

		public Nullable<Int16> ComunaCodigo { get; set; }

		public String Direccion { get; set; }

		public String Email { get; set; }

		public String PaginaWeb { get; set; }

		public Nullable<Int32> Telefono1 { get; set; }

		public Nullable<Int32> Telefono2 { get; set; }

		public Nullable<Int32> Fax { get; set; }

		public Nullable<Int32> Celular { get; set; }

		public Boolean Bloqueada { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Comuna> comuna;
		public Evaluaciones.Comuna Comuna
		{
			get { return this.comuna.Entity; }
			set { this.comuna.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Empresas.Attach(this);
		}

		public bool Equals(Empresa other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Empresa)) return false;
			return Equals((Empresa)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}