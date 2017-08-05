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

		public Guid PreguntaId { get; set; }

		public Guid Id { get; set; }

        [Display(Name = "Alternativa correcta:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la alternativa correcta")]
        public Char Correcta { get; set; }

        [Display(Name = "Puntaje:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el puntaje")]
        public Int32 Puntaje { get; set; }

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
			return other.PreguntaId.Equals(PreguntaId) && other.Id.Equals(Id);
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
			return this.PreguntaId.GetHashCode() ^ this.Id.GetHashCode();
		}
	}
}