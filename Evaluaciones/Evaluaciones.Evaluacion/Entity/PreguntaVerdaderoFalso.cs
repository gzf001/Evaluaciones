using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class PreguntaVerdaderoFalso : IEquatable<PreguntaVerdaderoFalso>
	{
		public PreguntaVerdaderoFalso()
		{
			this.pregunta = default(EntityRef<Evaluaciones.Evaluacion.Pregunta>);
		}

		public Guid PreguntaId { get; set; }

        [Display(Name = "Respuesta:")]
        public Boolean Respuesta { get; set; }

        [Display(Name = "Puntaje:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el puntaje")]
        public Int32 Puntaje { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.Pregunta> pregunta;
		public Evaluaciones.Evaluacion.Pregunta Pregunta
		{
			get { return this.pregunta.Entity; }
			set { this.pregunta.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.PreguntaVerdaderoFalsos.Attach(this);
		}

		public bool Equals(PreguntaVerdaderoFalso other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.PreguntaId.Equals(PreguntaId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(PreguntaVerdaderoFalso)) return false;
			return Equals((PreguntaVerdaderoFalso)obj);
		}

		public override int GetHashCode()
		{
			return this.PreguntaId.GetHashCode();
		}
	}
}