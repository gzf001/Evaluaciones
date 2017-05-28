using System;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class PreguntaAlternativa : IEquatable<PreguntaAlternativa>
	{
		public PreguntaAlternativa()
		{
			this.pregunta = default(EntityRef<Evaluaciones.Evaluacion.Pregunta>);
		}

		public Guid PreguntaId { get; set; }

		public Char AlternativaCorrecta { get; set; }

		public String AlternativaA { get; set; }

		public String AlternativaB { get; set; }

		public String AlternativaC { get; set; }

		public String AlternativaD { get; set; }

		public String AlternativaE { get; set; }

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
			Context.Instancia.PreguntaAlternativas.Attach(this);
		}

		public bool Equals(PreguntaAlternativa other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.PreguntaId.Equals(PreguntaId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(PreguntaAlternativa)) return false;
			return Equals((PreguntaAlternativa)obj);
		}

		public override int GetHashCode()
		{
			return this.PreguntaId.GetHashCode();
		}
	}
}