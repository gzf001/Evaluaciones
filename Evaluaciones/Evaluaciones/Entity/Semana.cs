using System;
using System.Data.Linq;
namespace Evaluaciones
{
	[Serializable]
	public partial class Semana : IEquatable<Semana>
	{
		public Semana()
		{
			this.anioMes = default(EntityRef<Evaluaciones.AnioMes>);
		}

		public Int32 AnioNumero { get; set; }

		public Int32 MesNumero { get; set; }

		public Int32 Numero { get; set; }

		public DateTime Inicio { get; set; }

		public DateTime Termino { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.AnioMes> anioMes;
		public Evaluaciones.AnioMes AnioMes
		{
			get { return this.anioMes.Entity; }
			set { this.anioMes.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Semanas.Attach(this);
		}

		public bool Equals(Semana other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.AnioNumero.Equals(AnioNumero) && other.MesNumero.Equals(MesNumero) && other.Numero.Equals(Numero);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Semana)) return false;
			return Equals((Semana)obj);
		}

		public override int GetHashCode()
		{
			return this.AnioNumero.GetHashCode() ^ this.MesNumero.GetHashCode() ^ this.Numero.GetHashCode();
		}
	}
}