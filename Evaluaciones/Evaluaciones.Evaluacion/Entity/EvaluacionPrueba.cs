using System;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class EvaluacionPrueba : IEquatable<EvaluacionPrueba>
	{
		public EvaluacionPrueba()
		{
			this.evaluacion = default(EntityRef<Evaluaciones.Evaluacion.Evaluacion>);
			this.prueba = default(EntityRef<Evaluaciones.Evaluacion.Prueba>);
		}

		public Guid EvaluacionId { get; set; }

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid PruebaId { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Evaluacion.Evaluacion> evaluacion;
		public Evaluaciones.Evaluacion.Evaluacion Evaluacion
		{
			get { return this.evaluacion.Entity; }
			set { this.evaluacion.Entity = value; }
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
			Context.Instancia.EvaluacionPruebas.Attach(this);
		}

		public bool Equals(EvaluacionPrueba other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.EvaluacionId.Equals(EvaluacionId) && other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.PruebaId.Equals(PruebaId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(EvaluacionPrueba)) return false;
			return Equals((EvaluacionPrueba)obj);
		}

		public override int GetHashCode()
		{
			return this.EvaluacionId.GetHashCode() ^ this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.PruebaId.GetHashCode();
		}
	}
}