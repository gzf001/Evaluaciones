using System;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Tipo de educación:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el tipo de educación")]
        public Int32 TipoEducacionCodigo { get; set; }

        [Display(Name = "Grado:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el grado")]
        public Int32 GradoCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid PreguntaId { get; set; }

		public Int32 AnioNumero { get; set; }

        [Display(Name = "Unidad:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar la unidad")]
        public Guid UnidadId { get; set; }

        [Display(Name = "Eje:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el eje")]
        public Guid EjeId { get; set; }

        [Display(Name = "Objetivo de aprendizaje:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el objetivo de aprendizaje")]
        public Guid ObjetivoAprendizajeId { get; set; }

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
			return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.PreguntaId.Equals(PreguntaId);
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
			return this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.PreguntaId.GetHashCode();
		}
	}
}