using System;
using System.Data.Linq;
namespace Evaluaciones.Membresia
{
	[Serializable]
	public partial class CentroCostoAplicacion : IEquatable<CentroCostoAplicacion>
	{
		public CentroCostoAplicacion()
		{
			this.aplicacion = default(EntityRef<Evaluaciones.Membresia.Aplicacion>);
			this.centroCosto = default(EntityRef<Evaluaciones.CentroCosto>);
			this.empresaAplicacion = default(EntityRef<Evaluaciones.Membresia.EmpresaAplicacion>);
		}

		public Guid EmpresaId { get; set; }

		public Guid CentroCostoId { get; set; }

		public Guid AplicacionId { get; set; }

		[NonSerialized]
		private EntityRef<Evaluaciones.Membresia.Aplicacion> aplicacion;
		public Evaluaciones.Membresia.Aplicacion Aplicacion
		{
			get { return this.aplicacion.Entity; }
			set { this.aplicacion.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.CentroCosto> centroCosto;
		public Evaluaciones.CentroCosto CentroCosto
		{
			get { return this.centroCosto.Entity; }
			set { this.centroCosto.Entity = value; }
		}

		[NonSerialized]
		private EntityRef<Evaluaciones.Membresia.EmpresaAplicacion> empresaAplicacion;
		public Evaluaciones.Membresia.EmpresaAplicacion EmpresaAplicacion
		{
			get { return this.empresaAplicacion.Entity; }
			set { this.empresaAplicacion.Entity = value; }
		}

		public void Attach()
		{
			Context.Instancia.CentroCostoAplicaciones.Attach(this);
		}

		public bool Equals(CentroCostoAplicacion other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.EmpresaId.Equals(EmpresaId) && other.CentroCostoId.Equals(CentroCostoId) && other.AplicacionId.Equals(AplicacionId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(CentroCostoAplicacion)) return false;
			return Equals((CentroCostoAplicacion)obj);
		}

		public override int GetHashCode()
		{
			return this.EmpresaId.GetHashCode() ^ this.CentroCostoId.GetHashCode() ^ this.AplicacionId.GetHashCode();
		}
	}
}