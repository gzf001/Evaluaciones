using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace Evaluaciones.Membresia
{
    [Serializable]
    public partial class Usuario : IEquatable<Usuario>
    {
        public Usuario()
        {
            this.persona = default(EntityRef<Evaluaciones.Persona>);
        }

        public Guid Id { get; set; }

        [Display(Name = "Contraseña:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la contraseña")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name = "Aprobado")]
        public Boolean Aprobado { get; set; }

        [Display(Name = "Bloqueado")]
        public Boolean Bloqueado { get; set; }

        [Display(Name = "Fecha de creación:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la fecha de creación")]
        [DataType(DataType.DateTime)]
        public DateTime Creacion { get; set; }

        [Display(Name = "Fecha de última actividad:")]
        [DataType(DataType.DateTime)]
        public DateTime UltimaActividad { get; set; }

        [Display(Name = "Fecha de último acceso:")]
        [DataType(DataType.DateTime)]
        public DateTime UltimoAcceso { get; set; }

        [Display(Name = "Fecha de último cambio de contraseña:")]
        [DataType(DataType.DateTime)]
        public Nullable<DateTime> UltimoCambioPassword { get; set; }

        [Display(Name = "Fecha de último desbloqueo:")]
        [DataType(DataType.DateTime)]
        public Nullable<DateTime> UltimoDesbloqueo { get; set; }

        [Display(Name = "Número de intentos fallidos:")]
        public Int16 NumeroIntentosFallidos { get; set; }

        [Display(Name = "Fecha de intento fallido:")]
        [DataType(DataType.DateTime)]
        public Nullable<DateTime> FechaIntentoFallido { get; set; }

        [NonSerialized]
        private EntityRef<Evaluaciones.Persona> persona;
        public Evaluaciones.Persona Persona
        {
            get { return this.persona.Entity; }
            set { this.persona.Entity = value; }
        }

        public void Attach()
        {
            Context.Instancia.Usuarios.Attach(this);
        }

        public bool Equals(Usuario other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Usuario)) return false;
            return Equals((Usuario)obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}