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
			this.persona = default(EntityRef<Evaluaciones.Persona>);
		}

		public Guid Id { get; set; }

		public Int32 TipoEducacionCodigo { get; set; }

		public Int32 GradoCodigo { get; set; }

		public Guid SectorId { get; set; }

		public Guid PersonaId { get; set; }

		public Int32 Numero { get; set; }

		public String Nombre { get; set; }

		public DateTime Fecha { get; set; }

		public Int32 Puntaje { get; set; }

		public Boolean UsoComunal { get; set; }

		public String Descripcion { get; set; }

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
			return other.Id.Equals(Id);
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
			return this.Id.GetHashCode();
		}
	}
}