using System;
using System.Data.Linq;
namespace Evaluaciones
{
	[Serializable]
	public partial class Calendario : IEquatable<Calendario>
	{
		public Calendario()
		{
			this.anioMes = default(EntityRef<Evaluaciones.AnioMes>);
			this.dia = default(EntityRef<Evaluaciones.Dia>);
			this.semana = default(EntityRef<Evaluaciones.Semana>);
		}

		public DateTime Fecha { get; set; }

		public Int32 AnioNumero { get; set; }

		public Int32 MesNumero { get; set; }

		public Int32 SemanaNumero { get; set; }

		public Int16 DiaNumero { get; set; }

		public String Nombre { get; set; }

		public Boolean Festivo { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.AnioMes> anioMes;
		public Evaluaciones.AnioMes AnioMes
		{
			get { return this.anioMes.Entity; }
			set { this.anioMes.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Dia> dia;
		public Evaluaciones.Dia Dia
		{
			get { return this.dia.Entity; }
			set { this.dia.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Semana> semana;
		public Evaluaciones.Semana Semana
		{
			get { return this.semana.Entity; }
			set { this.semana.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Calendarios.Attach(this);
		}

		public bool Equals(Calendario other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Fecha.Equals(Fecha);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Calendario)) return false;
			return Equals((Calendario)obj);
		}

		public override int GetHashCode()
		{
			return this.Fecha.GetHashCode();
		}
	}
}