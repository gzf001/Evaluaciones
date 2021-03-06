using System;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class Evaluacion : IEquatable<Evaluacion>
	{
		public Evaluacion()
		{
			this.Id = Guid.NewGuid();
			this.anio = default(EntityRef<Evaluaciones.Anio>);
			this.tipoEvaluacion = default(EntityRef<Evaluaciones.Evaluacion.TipoEvaluacion>);
		}

		public Guid Id { get; set; }

		public Int32 AnioNumero { get; set; }

		public Guid SectorId { get; set; }

		public Int32 TipoEvaluacionCodigo { get; set; }

		public Int32 AmbitoCodigo { get; set; }

		public Nullable<Guid> SostenedorId { get; set; }

		public Nullable<Guid> EstablecimientoId { get; set; }

		public String Nombre { get; set; }

		public String Descripcion { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Anio> anio;
		public Evaluaciones.Anio Anio
		{
			get { return this.anio.Entity; }
			set { this.anio.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.TipoEvaluacion> tipoEvaluacion;
		public Evaluaciones.Evaluacion.TipoEvaluacion TipoEvaluacion
		{
			get { return this.tipoEvaluacion.Entity; }
			set { this.tipoEvaluacion.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Evaluaciones.Attach(this);
		}

		public bool Equals(Evaluacion other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Evaluacion)) return false;
			return Equals((Evaluacion)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}