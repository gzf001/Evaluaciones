using System;
using System.Data.Linq;
namespace Evaluaciones
{
	[Serializable]
	public partial class Anio : IEquatable<Anio>
	{
		public Anio()
		{
		}

		public Int32 Numero { get; set; }

		public String Nombre { get; set; }

		public Boolean Activo { get; set; }

		public void Attach()
		{
			Context.Instancia.Anios.Attach(this);
		}

		public bool Equals(Anio other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Numero.Equals(Numero);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Anio)) return false;
			return Equals((Anio)obj);
		}

		public override int GetHashCode()
		{
			return this.Numero.GetHashCode();
		}
	}
}