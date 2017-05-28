using System;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class PreguntaBaseCurricular : IEquatable<PreguntaBaseCurricular>
	{
		public PreguntaBaseCurricular()
		{
			this.anio = default(EntityRef<Evaluaciones.Anio>);
			this.pregunta = default(EntityRef<Evaluaciones.Evaluacion.Pregunta>);
		}

		public Guid PreguntaId { get; set; }

		public Nullable<Int32> TipoEducacionCodigo { get; set; }

		public Nullable<Int32> GradoCodigo { get; set; }

		public Nullable<Int32> AnioNumero { get; set; }

		public Nullable<Guid> SectorId { get; set; }

		public Nullable<Guid> UnidadId { get; set; }

		public Nullable<Guid> EjeId { get; set; }

		public Nullable<Guid> ObjetivoAprendizajeId { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Anio> anio;
		public Evaluaciones.Anio Anio
		{
			get { return this.anio.Entity; }
			set { this.anio.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.Pregunta> pregunta;
		public Evaluaciones.Evaluacion.Pregunta Pregunta
		{
			get { return this.pregunta.Entity; }
			set { this.pregunta.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.PreguntaBaseCurriculares.Attach(this);
		}

		public bool Equals(PreguntaBaseCurricular other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.PreguntaId.Equals(PreguntaId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(PreguntaBaseCurricular)) return false;
			return Equals((PreguntaBaseCurricular)obj);
		}

		public override int GetHashCode()
		{
			return this.PreguntaId.GetHashCode();
		}
	}
}