using System;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class Prueba : IEquatable<Prueba>
	{
		public Prueba()
		{
			this.Id = Guid.NewGuid();
			this.grado = default(EntityRef<Evaluaciones.Educacion.Grado>);
			this.persona = default(EntityRef<Evaluaciones.Persona>);
		}

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid Id { get; set; }

		public Guid PersonaId { get; set; }

		public Int32 Numero { get; set; }

		public String Nombre { get; set; }

		public DateTime Fecha { get; set; }

		public Int32 Puntaje { get; set; }

		public Boolean UsoComunal { get; set; }

		public String Descripcion { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Educacion.Grado> grado;
		public Evaluaciones.Educacion.Grado Grado
		{
			get { return this.grado.Entity; }
			set { this.grado.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Persona> persona;
		public Evaluaciones.Persona Persona
		{
			get { return this.persona.Entity; }
			set { this.persona.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Pruebas.Attach(this);
		}

		public bool Equals(Prueba other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Prueba)) return false;
			return Equals((Prueba)obj);
		}

		public override int GetHashCode()
		{
			return this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.Id.GetHashCode();
		}
	}
}