using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class PreguntaSeleccionMultiple : IEquatable<PreguntaSeleccionMultiple>
	{
		public PreguntaSeleccionMultiple()
		{
			this.pregunta = default(EntityRef<Evaluaciones.Evaluacion.Pregunta>);
		}

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid PreguntaId { get; set; }

        [Display(Name = "Alternativa A:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un texto para la alternativa A")]
        [DataType(DataType.Text)]
        public String AlternativaA { get; set; }

        [Display(Name = "Alternativa B:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un texto para la alternativa B")]
        [DataType(DataType.Text)]
        public String AlternativaB { get; set; }

        [Display(Name = "Alternativa C:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un texto para la alternativa C")]
        [DataType(DataType.Text)]
        public String AlternativaC { get; set; }

        [Display(Name = "Alternativa D:")]
        [DataType(DataType.Text)]
        public String AlternativaD { get; set; }

        [Display(Name = "Alternativa E:")]
        [DataType(DataType.Text)]
        public String AlternativaE { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.Pregunta> pregunta;
		public Evaluaciones.Evaluacion.Pregunta Pregunta
		{
			get { return this.pregunta.Entity; }
			set { this.pregunta.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.PreguntaSeleccionMultiples.Attach(this);
		}

		public bool Equals(PreguntaSeleccionMultiple other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.PreguntaId.Equals(PreguntaId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(PreguntaSeleccionMultiple)) return false;
			return Equals((PreguntaSeleccionMultiple)obj);
		}

		public override int GetHashCode()
		{
			return this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.PreguntaId.GetHashCode();
		}
	}
}