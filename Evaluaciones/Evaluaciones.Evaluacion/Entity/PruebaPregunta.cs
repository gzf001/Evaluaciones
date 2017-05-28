using System;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class PruebaPregunta : IEquatable<PruebaPregunta>
	{
		public PruebaPregunta()
		{
			this.pregunta = default(EntityRef<Evaluaciones.Evaluacion.Pregunta>);
			this.prueba = default(EntityRef<Evaluaciones.Evaluacion.Prueba>);
		}

		public Guid PruebaId { get; set; }

		public Guid PreguntaId { get; set; }

		public Boolean Nula { get; set; }

		public Int32 Orden { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.Pregunta> pregunta;
		public Evaluaciones.Evaluacion.Pregunta Pregunta
		{
			get { return this.pregunta.Entity; }
			set { this.pregunta.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.Prueba> prueba;
		public Evaluaciones.Evaluacion.Prueba Prueba
		{
			get { return this.prueba.Entity; }
			set { this.prueba.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.PruebaPreguntas.Attach(this);
		}

		public bool Equals(PruebaPregunta other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.PruebaId.Equals(PruebaId) && other.PreguntaId.Equals(PreguntaId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(PruebaPregunta)) return false;
			return Equals((PruebaPregunta)obj);
		}

		public override int GetHashCode()
		{
			return this.PruebaId.GetHashCode() ^ this.PreguntaId.GetHashCode();
		}
	}
}