using System;
using System.Data.Linq;
namespace Evaluaciones
{
	[Serializable]
	public partial class AnioMes : IEquatable<AnioMes>
	{
		public AnioMes()
		{
			this.anio = default(EntityRef<Evaluaciones.Anio>);
			this.mes = default(EntityRef<Evaluaciones.Mes>);
		}

		public Int32 AnioNumero { get; set; }

		public Int32 MesNumero { get; set; }

		public DateTime Inicio { get; set; }

		public DateTime Termino { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Anio> anio;
		public Evaluaciones.Anio Anio
		{
			get { return this.anio.Entity; }
			set { this.anio.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Mes> mes;
		public Evaluaciones.Mes Mes
		{
			get { return this.mes.Entity; }
			set { this.mes.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.AnioMeses.Attach(this);
		}

		public bool Equals(AnioMes other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.AnioNumero.Equals(AnioNumero) && other.MesNumero.Equals(MesNumero);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(AnioMes)) return false;
			return Equals((AnioMes)obj);
		}

		public override int GetHashCode()
		{
			return this.AnioNumero.GetHashCode() ^ this.MesNumero.GetHashCode();
		}
	}
}