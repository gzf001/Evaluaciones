using System;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class EmpresaPregunta : IEquatable<EmpresaPregunta>
	{
		public EmpresaPregunta()
		{
			this.Id = Guid.NewGuid();
			this.empresa = default(EntityRef<Evaluaciones.Empresa>);
			this.pregunta = default(EntityRef<Evaluaciones.Evaluacion.Pregunta>);
		}

		public Guid EmpresaId { get; set; }

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid PreguntaId { get; set; }

		public Guid Id { get; set; }

		public DateTime Fecha { get; set; }

		public String Detalle { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Empresa> empresa;
		public Evaluaciones.Empresa Empresa
		{
			get { return this.empresa.Entity; }
			set { this.empresa.Entity = value; }
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
			Context.Instancia.EmpresaPreguntas.Attach(this);
		}

		public bool Equals(EmpresaPregunta other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.EmpresaId.Equals(EmpresaId) && other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.PreguntaId.Equals(PreguntaId) && other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(EmpresaPregunta)) return false;
			return Equals((EmpresaPregunta)obj);
		}

		public override int GetHashCode()
		{
			return this.EmpresaId.GetHashCode() ^ this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.PreguntaId.GetHashCode() ^ this.Id.GetHashCode();
		}
	}
}