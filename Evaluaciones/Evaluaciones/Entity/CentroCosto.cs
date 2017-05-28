using System;
using System.Data.Linq;
namespace Evaluaciones
{
	[Serializable]
	public partial class CentroCosto : IEquatable<CentroCosto>
	{
		public CentroCosto()
		{
			this.Id = Guid.NewGuid();
			this.areaGeografica = default(EntityRef<Evaluaciones.AreaGeografica>);
			this.comuna = default(EntityRef<Evaluaciones.Comuna>);
			this.empresa = default(EntityRef<Evaluaciones.Empresa>);
		}

		public Guid EmpresaId { get; set; }

		public Guid Id { get; set; }

		public String Nombre { get; set; }

		public String Sigla { get; set; }

		public Int32 AreaGeograficaCodigo { get; set; }

		public Nullable<Int16> RegionCodigo { get; set; }

		public Nullable<Int16> CiudadCodigo { get; set; }

		public Nullable<Int16> ComunaCodigo { get; set; }

		public String Email { get; set; }

		public String Direccion { get; set; }

		public Nullable<Int32> Telefono1 { get; set; }

		public Nullable<Int32> Telefono2 { get; set; }

		public Nullable<Int32> Fax { get; set; }

		public Nullable<Int32> Celular { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.AreaGeografica> areaGeografica;
		public Evaluaciones.AreaGeografica AreaGeografica
		{
			get { return this.areaGeografica.Entity; }
			set { this.areaGeografica.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Comuna> comuna;
		public Evaluaciones.Comuna Comuna
		{
			get { return this.comuna.Entity; }
			set { this.comuna.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Empresa> empresa;
		public Evaluaciones.Empresa Empresa
		{
			get { return this.empresa.Entity; }
			set { this.empresa.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.CentroCostos.Attach(this);
		}

		public bool Equals(CentroCosto other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.EmpresaId.Equals(EmpresaId) && other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(CentroCosto)) return false;
			return Equals((CentroCosto)obj);
		}

		public override int GetHashCode()
		{
			return this.EmpresaId.GetHashCode() ^ this.Id.GetHashCode();
		}
	}
}