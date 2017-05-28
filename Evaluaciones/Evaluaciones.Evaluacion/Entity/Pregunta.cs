using System;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class Pregunta : IEquatable<Pregunta>
	{
		public Pregunta()
		{
			this.Id = Guid.NewGuid();
			this.dificultad = default(EntityRef<Evaluaciones.Evaluacion.Dificultad>);
			this.habilidad = default(EntityRef<Evaluaciones.Evaluacion.Habilidad>);
			this.tipoEvaluacion = default(EntityRef<Evaluaciones.Evaluacion.TipoEvaluacion>);
			this.tipoReactivo = default(EntityRef<Evaluaciones.Evaluacion.TipoReactivo>);
		}

		public Guid Id { get; set; }

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Int32 Codigo { get; set; }

		public Int32 DificultadCodigo { get; set; }

		public Int32 TipoReactivoCodigo { get; set; }

		public Int32 TipoEvaluacionCodigo { get; set; }

		public Int32 HabilidadCodigo { get; set; }

		public String Texto { get; set; }

		public Boolean UsoComunal { get; set; }

		public Boolean Privado { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.Dificultad> dificultad;
		public Evaluaciones.Evaluacion.Dificultad Dificultad
		{
			get { return this.dificultad.Entity; }
			set { this.dificultad.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.Habilidad> habilidad;
		public Evaluaciones.Evaluacion.Habilidad Habilidad
		{
			get { return this.habilidad.Entity; }
			set { this.habilidad.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.TipoEvaluacion> tipoEvaluacion;
		public Evaluaciones.Evaluacion.TipoEvaluacion TipoEvaluacion
		{
			get { return this.tipoEvaluacion.Entity; }
			set { this.tipoEvaluacion.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.TipoReactivo> tipoReactivo;
		public Evaluaciones.Evaluacion.TipoReactivo TipoReactivo
		{
			get { return this.tipoReactivo.Entity; }
			set { this.tipoReactivo.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Preguntas.Attach(this);
		}

		public bool Equals(Pregunta other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Pregunta)) return false;
			return Equals((Pregunta)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}