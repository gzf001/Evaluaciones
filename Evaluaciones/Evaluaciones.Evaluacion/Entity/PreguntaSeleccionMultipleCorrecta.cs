using System;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class PreguntaSeleccionMultipleCorrecta : IEquatable<PreguntaSeleccionMultipleCorrecta>
	{
		public PreguntaSeleccionMultipleCorrecta()
		{
			this.pregunta = default(EntityRef<Evaluaciones.Evaluacion.Pregunta>);
		}

		public Guid PreguntaId { get; set; }

		public Char Correcta { get; set; }

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
			Context.Instancia.PreguntaSeleccionMultipleCorrectas.Attach(this);
		}

		public bool Equals(PreguntaSeleccionMultipleCorrecta other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.PreguntaId.Equals(PreguntaId);
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
			return this.PreguntaId.GetHashCode();
		}
	}
}