using System;
using System.Data.Linq;
namespace Evaluaciones.Educacion
{
	[Serializable]
	public partial class TipoEducacion : IEquatable<TipoEducacion>
	{
		public TipoEducacion()
		{
		}

		public Int32 Codigo { get; set; }

		public String Nombre { get; set; }

		public void Attach()
		{
			Context.Instancia.TipoEducaciones.Attach(this);
		}

		public bool Equals(TipoEducacion other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Codigo.Equals(Codigo);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(TipoEducacion)) return false;
			return Equals((TipoEducacion)obj);
		}

		public override int GetHashCode()
		{
			return this.Codigo.GetHashCode();
		}
	}
}