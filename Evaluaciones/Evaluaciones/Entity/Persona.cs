using System;
using System.Data.Linq;
namespace Evaluaciones
{
	[Serializable]
	public partial class Persona : IEquatable<Persona>
	{
		public Persona()
		{
			this.Id = Guid.NewGuid();
			this.comuna = default(EntityRef<Evaluaciones.Comuna>);
			this.estadoCivil = default(EntityRef<Evaluaciones.EstadoCivil>);
			this.nacionalidad = default(EntityRef<Evaluaciones.Nacionalidad>);
			this.nivelEducacional = default(EntityRef<Evaluaciones.NivelEducacional>);
			this.sexo = default(EntityRef<Evaluaciones.Sexo>);
		}

		public Guid Id { get; set; }

		public String Run { get; set; }

		public Int32 RunCuerpo { get; set; }

		public Char RunDigito { get; set; }

		public String Nombre { get; set; }

		public String Nombres { get; set; }

		public String ApellidoPaterno { get; set; }

		public String ApellidoMaterno { get; set; }

		public String Email { get; set; }

		public Int16 SexoCodigo { get; set; }

		public Nullable<DateTime> FechaNacimiento { get; set; }

		public Nullable<Int16> NacionalidadCodigo { get; set; }

		public Nullable<Int16> EstadoCivilCodigo { get; set; }

		public Nullable<Int32> NivelEducacionalCodigo { get; set; }

		public Nullable<Int16> RegionCodigo { get; set; }

		public Nullable<Int16> CiudadCodigo { get; set; }

		public Nullable<Int16> ComunaCodigo { get; set; }

		public String VillaPoblacion { get; set; }

		public String Direccion { get; set; }

		public Nullable<Int32> Telefono { get; set; }

		public Nullable<Int32> Celular { get; set; }

		public String Observaciones { get; set; }

		public String Ocupacion { get; set; }

		public Nullable<Int32> TelefonoLaboral { get; set; }

		public String DireccionLaboral { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Comuna> comuna;
		public Evaluaciones.Comuna Comuna
		{
			get { return this.comuna.Entity; }
			set { this.comuna.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.EstadoCivil> estadoCivil;
		public Evaluaciones.EstadoCivil EstadoCivil
		{
			get { return this.estadoCivil.Entity; }
			set { this.estadoCivil.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Nacionalidad> nacionalidad;
		public Evaluaciones.Nacionalidad Nacionalidad
		{
			get { return this.nacionalidad.Entity; }
			set { this.nacionalidad.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.NivelEducacional> nivelEducacional;
		public Evaluaciones.NivelEducacional NivelEducacional
		{
			get { return this.nivelEducacional.Entity; }
			set { this.nivelEducacional.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Sexo> sexo;
		public Evaluaciones.Sexo Sexo
		{
			get { return this.sexo.Entity; }
			set { this.sexo.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Personas.Attach(this);
		}

		public bool Equals(Persona other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Persona)) return false;
			return Equals((Persona)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}