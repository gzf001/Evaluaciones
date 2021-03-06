using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace Evaluaciones
{
	[Serializable]
	public partial class Empresa : IEquatable<Empresa>
	{
		public Empresa()
		{
			this.Id = Guid.NewGuid();
			this.comuna = default(EntityRef<Evaluaciones.Comuna>);
		}

		public Guid Id { get; set; }

        [Display(Name = "R.U.T.:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el R.U.T.")]
        public String Rut { get; set; }

		public Int32 RutCuerpo { get; set; }

		public Char RutDigito { get; set; }

        [Display(Name = "Razón social:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la razón social")]
        public String RazonSocial { get; set; }

        [Display(Name = "Región:")]
        public Nullable<Int16> RegionCodigo { get; set; }

        [Display(Name = "Ciudad:")]
        public Nullable<Int16> CiudadCodigo { get; set; }

        [Display(Name = "Comuna:")]
        public Nullable<Int16> ComunaCodigo { get; set; }

        [Display(Name = "Dirección:")]
        [DataType(DataType.MultilineText)]
        public String Direccion { get; set; }

        [Display(Name = "Email:")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$", ErrorMessage = "Mail incorrecto")]
        public String Email { get; set; }

        [Display(Name = "Sitio WEB:")]
        public String PaginaWeb { get; set; }

        [Display(Name = "Teléfono 1:")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "El número del teléfono 1 es erróneo")]
        [Range(0, 999999999999, ErrorMessage = "El número del teléfono 1 es erróneo")]
        public Nullable<Int32> Telefono1 { get; set; }

        [Display(Name = "Teléfono 2:")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "El número del teléfono 2 es erróneo")]
        [Range(0, 999999999999, ErrorMessage = "El número del teléfono 2 es erróneo")]
        public Nullable<Int32> Telefono2 { get; set; }

        [Display(Name = "Fax:")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "El número de fax es erróneo")]
        [Range(0, 999999999999, ErrorMessage = "El número de fax es erróneo")]
        public Nullable<Int32> Fax { get; set; }

        [Display(Name = "Teléfono celular:")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "El número de celular es erróneo")]
        [Range(0, 999999999999, ErrorMessage = "El número de celular es erróneo")]
        public Nullable<Int32> Celular { get; set; }

        [Display(Name = "Bloqueada")]
        public Boolean Bloqueada { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Comuna> comuna;
		public Evaluaciones.Comuna Comuna
		{
			get { return this.comuna.Entity; }
			set { this.comuna.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Empresas.Attach(this);
		}

		public bool Equals(Empresa other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Empresa)) return false;
			return Equals((Empresa)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}