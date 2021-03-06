using System;
using System.Linq;
namespace Evaluaciones
{
	public partial class Empresa
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Empresa empresa = context.Empresas.SingleOrDefault<Empresa>(x => x == this);

			if (empresa == null)
			{
				empresa = new Empresa
				{
					Id = this.Id
				};

				context.Empresas.InsertOnSubmit(empresa);
			}

			empresa.RutCuerpo = this.RutCuerpo;
			empresa.RutDigito = this.RutDigito;
			empresa.RazonSocial = this.RazonSocial;
			empresa.RegionCodigo = this.RegionCodigo == default(Int16) ? null : this.RegionCodigo;
			empresa.CiudadCodigo = this.CiudadCodigo == default(Int16) ? null : this.CiudadCodigo;
			empresa.ComunaCodigo = this.ComunaCodigo == default(Int16) ? null : this.ComunaCodigo;
			empresa.Direccion = this.Direccion;
			empresa.Email = this.Email;
			empresa.PaginaWeb = this.PaginaWeb;
			empresa.Telefono1 = this.Telefono1 == default(Int32) ? null : this.Telefono1;
			empresa.Telefono2 = this.Telefono2 == default(Int32) ? null : this.Telefono2;
			empresa.Fax = this.Fax == default(Int32) ? null : this.Fax;
			empresa.Celular = this.Celular == default(Int32) ? null : this.Celular;
			empresa.Bloqueada = this.Bloqueada;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Empresa empresa = context.Empresas.SingleOrDefault<Empresa>(x => x == this);

			if (empresa != null)
			{
				context.Empresas.DeleteOnSubmit(empresa);
			}
			PostDelete(context);
		}
	}
}