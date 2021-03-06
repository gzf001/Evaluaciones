using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace Evaluaciones.Membresia
{
    [Serializable]
    public partial class Aplicacion : IEquatable<Aplicacion>
    {
        public Aplicacion()
        {
            this.Id = Guid.NewGuid();
            this.inicio = default(EntityRef<Evaluaciones.Membresia.MenuItem>);
        }

        public Guid Id { get; set; }

        public Nullable<Guid> MenuId { get; set; }

        public Nullable<Guid> MenuItemId { get; set; }

        [Display(Name = "Aplicación:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre es requerido")]
        [MaxLength(60, ErrorMessage = "El máximo son 60 caracteres")]
        public String Nombre { get; set; }

        [Display(Name = "Clave:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La clave es requerida")]
        [MaxLength(50, ErrorMessage = "El máximo son 50 caracteres")]
        public String Clave { get; set; }

        [Display(Name = "Fa-Icon:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El ícono es requerido")]
        [MaxLength(10, ErrorMessage = "El máximo son 10 caracteres")]
        public String Fa_Icon { get; set; }

        [Display(Name = "Orden:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El orden es requerido")]
        public Byte Orden { get; set; }

        [NonSerialized]
        private EntityRef<Evaluaciones.Membresia.MenuItem> inicio;
        public Evaluaciones.Membresia.MenuItem Inicio
        {
            get { return this.inicio.Entity; }
            set { this.inicio.Entity = value; }
        }

        public void Attach()
        {
            Context.Instancia.Aplicaciones.Attach(this);
        }

        public bool Equals(Aplicacion other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Aplicacion)) return false;
            return Equals((Aplicacion)obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}