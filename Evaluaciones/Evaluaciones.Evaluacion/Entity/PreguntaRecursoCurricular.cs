using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class PreguntaRecursoCurricular : IEquatable<PreguntaRecursoCurricular>
	{
		public PreguntaRecursoCurricular()
		{
			this.pregunta = default(EntityRef<Evaluaciones.Evaluacion.Pregunta>);
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

		public Guid PreguntaId { get; set; }

        [Display(Name = "Unidad:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar la unidad")]
        public Guid UnidadId { get; set; }

        [Display(Name = "Aprendizaje:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el aprendizaje")]
        public Guid AprendizajeId { get; set; }

        [Display(Name = "Eje:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el eje")] 
        public Guid EjeId { get; set; }

        [Display(Name = "Contenido:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el contenido")]
        public Guid ContenidoId { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.Pregunta> pregunta;
		public Evaluaciones.Evaluacion.Pregunta Pregunta
		{
			get { return this.pregunta.Entity; }
			set { this.pregunta.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.PreguntaRecursoCurriculares.Attach(this);
		}

		public bool Equals(PreguntaRecursoCurricular other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.PreguntaId.Equals(PreguntaId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(PreguntaRecursoCurricular)) return false;
			return Equals((PreguntaRecursoCurricular)obj);
		}

		public override int GetHashCode()
		{
			return this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.PreguntaId.GetHashCode();
		}
	}
}