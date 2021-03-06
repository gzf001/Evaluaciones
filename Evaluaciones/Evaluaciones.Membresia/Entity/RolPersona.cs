using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace Evaluaciones.Membresia
{
	[Serializable]
	public partial class RolPersona : IEquatable<RolPersona>
	{
		public RolPersona()
		{
			this.persona = default(EntityRef<Evaluaciones.Persona>);
			this.rol = default(EntityRef<Evaluaciones.Membresia.Rol>);
		}

        [Display(Name = "Rol:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el rol")]
        public Guid RolId { get; set; }

        [Display(Name = "Persona:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar la persona")]
        public Guid PersonaId { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Persona> persona;
		public Evaluaciones.Persona Persona
		{
			get { return this.persona.Entity; }
			set { this.persona.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Membresia.Rol> rol;
		public Evaluaciones.Membresia.Rol Rol
		{
			get { return this.rol.Entity; }
			set { this.rol.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.RolPersonas.Attach(this);
		}

		public bool Equals(RolPersona other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.RolId.Equals(RolId) && other.PersonaId.Equals(PersonaId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(RolPersona)) return false;
			return Equals((RolPersona)obj);
		}

		public override int GetHashCode()
		{
			return this.RolId.GetHashCode() ^ this.PersonaId.GetHashCode();
		}
    }
}