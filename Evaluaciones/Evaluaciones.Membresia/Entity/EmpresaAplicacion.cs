using System;
using System.Data.Linq;
namespace Evaluaciones.Membresia
{
	[Serializable]
	public partial class EmpresaAplicacion : IEquatable<EmpresaAplicacion>
	{
		public EmpresaAplicacion()
		{
			this.aplicacion = default(EntityRef<Evaluaciones.Membresia.Aplicacion>);
			this.empresa = default(EntityRef<Evaluaciones.Empresa>);
		}

		public Guid EmpresaId { get; set; }

		public Guid AplicacionId { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Membresia.Aplicacion> aplicacion;
		public Evaluaciones.Membresia.Aplicacion Aplicacion
		{
			get { return this.aplicacion.Entity; }
			set { this.aplicacion.Entity = value; }
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
			Context.Instancia.EmpresaAplicaciones.Attach(this);
		}

		public bool Equals(EmpresaAplicacion other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.EmpresaId.Equals(EmpresaId) && other.AplicacionId.Equals(AplicacionId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(EmpresaAplicacion)) return false;
			return Equals((EmpresaAplicacion)obj);
		}

		public override int GetHashCode()
		{
			return this.EmpresaId.GetHashCode() ^ this.AplicacionId.GetHashCode();
		}
	}
}