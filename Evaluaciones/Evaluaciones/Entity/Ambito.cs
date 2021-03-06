using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace Evaluaciones
{
	[Serializable]
	public partial class Ambito : IEquatable<Ambito>
	{
		public Ambito()
		{
		}

		public Int32 Codigo { get; set; }

        [Display(Name = "Ámbito:")]
        public String Nombre { get; set; }

		public void Attach()
		{
			Context.Instancia.Ambitos.Attach(this);
		}

		public bool Equals(Ambito other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Codigo.Equals(Codigo);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Ambito)) return false;
			return Equals((Ambito)obj);
		}

		public override int GetHashCode()
		{
			return this.Codigo.GetHashCode();
		}
	}
}