using System;
using System.Data.Linq;
namespace Evaluaciones
{
	[Serializable]
	public partial class Feriado : IEquatable<Feriado>
	{
		public Feriado()
		{
			this.Id = Guid.NewGuid();
			this.calendario = default(EntityRef<Evaluaciones.Calendario>);
			this.centroCosto = default(EntityRef<Evaluaciones.CentroCosto>);
			this.empresa = default(EntityRef<Evaluaciones.Empresa>);
		}

		public Guid Id { get; set; }

		public Nullable<Guid> EmpresaId { get; set; }

		public Nullable<Guid> CentroCostoId { get; set; }

		public DateTime Fecha { get; set; }

		public String Nombre { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Calendario> calendario;
		public Evaluaciones.Calendario Calendario
		{
			get { return this.calendario.Entity; }
			set { this.calendario.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.CentroCosto> centroCosto;
		public Evaluaciones.CentroCosto CentroCosto
		{
			get { return this.centroCosto.Entity; }
			set { this.centroCosto.Entity = value; }
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
			Context.Instancia.Feriados.Attach(this);
		}

		public bool Equals(Feriado other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Feriado)) return false;
			return Equals((Feriado)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}