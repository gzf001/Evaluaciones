using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
namespace Evaluaciones.Educacion
{
	[Serializable]
	public partial class Grado : IEquatable<Grado>
	{
		public Grado()
		{
			this.siguienteGrado = default(EntityRef<Evaluaciones.Educacion.Grado>);
			this.siguienteTipoEducacion = default(EntityRef<Evaluaciones.Educacion.TipoEducacion>);
			this.tipoEducacion = default(EntityRef<Evaluaciones.Educacion.TipoEducacion>);
		}

        [Display(Name = "Tipo de educación:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el tipo de educación")]
        public Int32 TipoEducacionCodigo { get; set; }

        [Display(Name = "Grado:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el grado")]
        public Int32 Codigo { get; set; }

		public String Nombre { get; set; }

		public Nullable<Int32> SiguienteTipoEducacionCodigo { get; set; }

		public Nullable<Int32> SiguienteGradoCodigo { get; set; }

		public Boolean BaseCurricular { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Educacion.Grado> siguienteGrado;
		public Evaluaciones.Educacion.Grado SiguienteGrado
		{
			get { return this.siguienteGrado.Entity; }
			set { this.siguienteGrado.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Educacion.TipoEducacion> siguienteTipoEducacion;
		public Evaluaciones.Educacion.TipoEducacion SiguienteTipoEducacion
		{
			get { return this.siguienteTipoEducacion.Entity; }
			set { this.siguienteTipoEducacion.Entity = value; }
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
			Context.Instancia.Grados.Attach(this);
		}

		public bool Equals(Grado other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.TipoEducacionCodigo.Equals(TipoEducacionCodigo) && other.Codigo.Equals(Codigo);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Grado)) return false;
			return Equals((Grado)obj);
		}

		public override int GetHashCode()
		{
			return this.TipoEducacionCodigo.GetHashCode() ^ this.Codigo.GetHashCode();
		}
    }
}