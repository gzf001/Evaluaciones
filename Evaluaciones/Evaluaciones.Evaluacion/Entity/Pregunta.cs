using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Linq;
using System.Web.Mvc;

namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class Pregunta : IEquatable<Pregunta>
	{
		public Pregunta()
		{
			this.Id = Guid.NewGuid();
			this.dificultad = default(EntityRef<Evaluaciones.Evaluacion.Dificultad>);
			this.grado = default(EntityRef<Evaluaciones.Educacion.Grado>);
			this.habilidad = default(EntityRef<Evaluaciones.Evaluacion.Habilidad>);
			this.referencia = default(EntityRef<Evaluaciones.Evaluacion.Referencia>);
			this.tipoEducacion = default(EntityRef<Evaluaciones.Educacion.TipoEducacion>);
			this.tipoEvaluacion = default(EntityRef<Evaluaciones.Evaluacion.TipoEvaluacion>);
			this.tipoReactivo = default(EntityRef<Evaluaciones.Evaluacion.TipoReactivo>);
		}

        [Display(Name = "Tipo de educación:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el tipo de educación")]
        public Int32 TipoEducacionCodigo { get; set; }

        [Display(Name = "Grado:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el grado")]
        public Int32 GradoCodigo { get; set; }

        [Display(Name = "Sector:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el sector")]
        public Guid SectorId { get; set; }

		public Guid Id { get; set; }

		public Nullable<Guid> ReferenciaId { get; set; }

        [Display(Name = "Código:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un código de pregunta")]
        public Int32 Codigo { get; set; }

        [Display(Name = "Dificultad:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar la dificultad")]
        [Range(1, 1000, ErrorMessage = "Debe seleccionar la dificultad")]
        public Int32 DificultadCodigo { get; set; }

        [Display(Name = "Tipo de reactivo:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el tipo de reactivo")]
        public Int32 TipoReactivoCodigo { get; set; }

        [Display(Name = "Tipo de evaluación:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el tipo de evaluación")]
        public Int32 TipoEvaluacionCodigo { get; set; }

        [Display(Name = "Habilidad:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar la habilidad")]
        public Int32 HabilidadCodigo { get; set; }

        [Display(Name = "Texto:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un texto")]
        [AllowHtml]
        public String Texto { get; set; }

        [Display(Name = "Privado:")]
        public Boolean Privado { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.Dificultad> dificultad;
		public Evaluaciones.Evaluacion.Dificultad Dificultad
		{
			get { return this.dificultad.Entity; }
			set { this.dificultad.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Educacion.Grado> grado;
		public Evaluaciones.Educacion.Grado Grado
		{
			get { return this.grado.Entity; }
			set { this.grado.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.Habilidad> habilidad;
		public Evaluaciones.Evaluacion.Habilidad Habilidad
		{
			get { return this.habilidad.Entity; }
			set { this.habilidad.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.Referencia> referencia;
		public Evaluaciones.Evaluacion.Referencia Referencia
		{
			get { return this.referencia.Entity; }
			set { this.referencia.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Educacion.TipoEducacion> tipoEducacion;
		public Evaluaciones.Educacion.TipoEducacion TipoEducacion
		{
			get { return this.tipoEducacion.Entity; }
			set { this.tipoEducacion.Entity = value; }
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
			return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.Id.Equals(Id);
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
			return this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.Id.GetHashCode();
		}
	}
}