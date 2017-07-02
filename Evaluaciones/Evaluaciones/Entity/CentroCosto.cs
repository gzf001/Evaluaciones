using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace Evaluaciones
{
	[Serializable]
	public partial class CentroCosto : IEquatable<CentroCosto>
	{
		public CentroCosto()
		{
			this.Id = Guid.NewGuid();
			this.areaGeografica = default(EntityRef<Evaluaciones.AreaGeografica>);
			this.comuna = default(EntityRef<Evaluaciones.Comuna>);
			this.empresa = default(EntityRef<Evaluaciones.Empresa>);
		}

        [Display(Name = "Empresa:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar la empresa")]
        public Guid EmpresaId { get; set; }

		public Guid Id { get; set; }

        [Display(Name = "Nombre:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el nombre")]
        public String Nombre { get; set; }

        [Display(Name = "Sigla:")]
        public String Sigla { get; set; }

        [Display(Name = "Área geográfica:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el área geográfica")]
        public Int32 AreaGeograficaCodigo { get; set; }

        [Display(Name = "Región:")]
        public Nullable<Int16> RegionCodigo { get; set; }

        [Display(Name = "Ciudad:")]
        public Nullable<Int16> CiudadCodigo { get; set; }

        [Display(Name = "Comuna:")]
        public Nullable<Int16> ComunaCodigo { get; set; }

        [Display(Name = "Email:")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$", ErrorMessage = "Mail incorrecto")]
        public String Email { get; set; }

        [Display(Name = "Dirección:")]
        [DataType(DataType.MultilineText)]
        public String Direccion { get; set; }

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

		[NonSerialized]
		private EntityRef<Evaluaciones.AreaGeografica> areaGeografica;
		public Evaluaciones.AreaGeografica AreaGeografica
		{
			get { return this.areaGeografica.Entity; }
			set { this.areaGeografica.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Comuna> comuna;
		public Evaluaciones.Comuna Comuna
		{
			get { return this.comuna.Entity; }
			set { this.comuna.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Empresa> empresa;
		public Evaluaciones.Empresa Empresa
		{
			get { return this.empresa.Entity; }
			set { this.empresa.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.CentroCostos.Attach(this);
		}

		public bool Equals(CentroCosto other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.EmpresaId.Equals(EmpresaId) && other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(CentroCosto)) return false;
			return Equals((CentroCosto)obj);
		}

		public override int GetHashCode()
		{
			return this.EmpresaId.GetHashCode() ^ this.Id.GetHashCode();
		}
	}
}