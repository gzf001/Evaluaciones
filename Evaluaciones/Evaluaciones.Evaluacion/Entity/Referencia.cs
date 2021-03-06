using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace Evaluaciones.Evaluacion
{
	[Serializable]
	public partial class Referencia : IEquatable<Referencia>
	{
		public Referencia()
		{
			this.Id = Guid.NewGuid();
			this.grado = default(EntityRef<Evaluaciones.Educacion.Grado>);
			this.tipoEducacion = default(EntityRef<Evaluaciones.Educacion.TipoEducacion>);
		}

        [Display(Name = "Tipo de educación:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el tipo de educación")]
        public Int32 TipoEducacionCodigo { get; set; }

        [Display(Name = "Grado:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el grado")]
        public Int32 GradoCodigo { get; set; }

        [Display(Name = "Sector:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el sector")]
        public Guid SectorId { get; set; }

		public Guid Id { get; set; }

        [Display(Name = "Código:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un código de pregunta")]
        public Int32 Codigo { get; set; }

        [Display(Name = "Texto:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un texto")]
        public String Texto { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Educacion.Grado> grado;
		public Evaluaciones.Educacion.Grado Grado
		{
			get { return this.grado.Entity; }
			set { this.grado.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Educacion.TipoEducacion> tipoEducacion;
		public Evaluaciones.Educacion.TipoEducacion TipoEducacion
		{
			get { return this.tipoEducacion.Entity; }
			set { this.tipoEducacion.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.Referencias.Attach(this);
		}

		public bool Equals(Referencia other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.GradoCodigo.Equals(GradoCodigo) && other.SectorId.Equals(SectorId) && other.Id.Equals(Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Referencia)) return false;
			return Equals((Referencia)obj);
		}

		public override int GetHashCode()
		{
			return this.TipoEducacionCodigo.GetHashCode() ^ this.GradoCodigo.GetHashCode() ^ this.SectorId.GetHashCode() ^ this.Id.GetHashCode();
		}
	}
}