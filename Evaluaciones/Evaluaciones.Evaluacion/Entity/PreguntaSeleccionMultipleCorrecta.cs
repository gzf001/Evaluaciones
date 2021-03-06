using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class PreguntaSeleccionMultipleCorrecta : IEquatable<PreguntaSeleccionMultipleCorrecta>
	{
		public PreguntaSeleccionMultipleCorrecta()
		{
			this.preguntaSeleccionMultiple = default(EntityRef<Evaluaciones.Evaluacion.PreguntaSeleccionMultiple>);
		}

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid PreguntaId { get; set; }

		public Guid Id { get; set; }

        [Display(Name = "Alternativa correcta:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la alternativa correcta")]
        public Char Correcta { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.PreguntaSeleccionMultiple> preguntaSeleccionMultiple;
		public Evaluaciones.Evaluacion.PreguntaSeleccionMultiple PreguntaSeleccionMultiple
		{
			get { return this.preguntaSeleccionMultiple.Entity; }
			set { this.preguntaSeleccionMultiple.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.PreguntaSeleccionMultipleCorrectas.Attach(this);
		}

		public bool Equals(PreguntaSeleccionMultipleCorrecta other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.PreguntaId.Equals(PreguntaId) && other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(PreguntaSeleccionMultipleCorrecta)) return false;
			return Equals((PreguntaSeleccionMultipleCorrecta)obj);
		}

		public override int GetHashCode()
		{
			return this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.PreguntaId.GetHashCode() ^ this.Id.GetHashCode();
		}
	}
}