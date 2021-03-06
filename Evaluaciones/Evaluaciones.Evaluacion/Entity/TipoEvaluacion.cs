using System;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class TipoEvaluacion : IEquatable<TipoEvaluacion>
	{
		public TipoEvaluacion()
		{
		}

		public Int32 Codigo { get; set; }

		public String Nombre { get; set; }

		public void Attach()
		{
			Context.Instancia.TipoEvaluaciones.Attach(this);
		}

		public bool Equals(TipoEvaluacion other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Codigo.Equals(Codigo);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(TipoEvaluacion)) return false;
			return Equals((TipoEvaluacion)obj);
		}

		public override int GetHashCode()
		{
			return this.Codigo.GetHashCode();
		}
	}
}