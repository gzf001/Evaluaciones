using System;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class RecursosPme : IEquatable<RecursosPme>
	{
		public RecursosPme()
		{
			this.pregunta = default(EntityRef<Evaluaciones.Evaluacion.Pregunta>);
		}

		public Guid PreguntaId { get; set; }

		public Guid SectorId { get; set; }

		public Nullable<Int32> TipoEducacionCodigo { get; set; }

		public Nullable<Int32> GradoCodigo { get; set; }

		public Nullable<Guid> AprendizajeId { get; set; }

		public Nullable<Guid> AprendizajeIndicadorId { get; set; }

		public Nullable<Guid> HabilidadId { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.Pregunta> pregunta;
		public Evaluaciones.Evaluacion.Pregunta Pregunta
		{
			get { return this.pregunta.Entity; }
			set { this.pregunta.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.RecursosPmes.Attach(this);
		}

		public bool Equals(RecursosPme other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.PreguntaId.Equals(PreguntaId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(RecursosPme)) return false;
			return Equals((RecursosPme)obj);
		}

		public override int GetHashCode()
		{
			return this.PreguntaId.GetHashCode();
		}
	}
}