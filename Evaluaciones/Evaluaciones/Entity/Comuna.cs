using System;
using System.Data.Linq;
namespace Evaluaciones
{
	[Serializable]
	public partial class Comuna : IEquatable<Comuna>
	{
		public Comuna()
		{
			this.ciudad = default(EntityRef<Evaluaciones.Ciudad>);
		}

		public Int16 RegionCodigo { get; set; }

		public Int16 CiudadCodigo { get; set; }

		public Int16 Codigo { get; set; }

		public String Nombre { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Ciudad> ciudad;
		public Evaluaciones.Ciudad Ciudad
		{
			get { return this.ciudad.Entity; }
			set { this.ciudad.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Comunas.Attach(this);
		}

		public bool Equals(Comuna other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.RegionCodigo.Equals(RegionCodigo) && other.CiudadCodigo.Equals(CiudadCodigo) && other.Codigo.Equals(Codigo);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Comuna)) return false;
			return Equals((Comuna)obj);
		}

		public override int GetHashCode()
		{
			return this.RegionCodigo.GetHashCode() ^ this.CiudadCodigo.GetHashCode() ^ this.Codigo.GetHashCode();
		}
	}
}