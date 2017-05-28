using System;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class Referencia : IEquatable<Referencia>
	{
		public Referencia()
		{
			this.pregunta = default(EntityRef<Evaluaciones.Evaluacion.Pregunta>);
		}

		public Guid PreguntaId { get; set; }

		public String Texto { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.Pregunta> pregunta;
		public Evaluaciones.Evaluacion.Pregunta Pregunta
		{
			get { return this.pregunta.Entity; }
			set { this.pregunta.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Referencias.Attach(this);
		}

		public bool Equals(Referencia other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.PreguntaId.Equals(PreguntaId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Referencia)) return false;
			return Equals((Referencia)obj);
		}

		public override int GetHashCode()
		{
			return this.PreguntaId.GetHashCode();
		}
	}
}