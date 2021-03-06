using System;
using System.Linq;
namespace Evaluaciones
{
	public partial class Persona
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Persona persona = context.Personas.SingleOrDefault<Persona>(x => x == this);

			if (persona == null)
			{
				persona = new Persona
				{
					Id = this.Id
				};

				context.Personas.InsertOnSubmit(persona);
			}

			persona.RunCuerpo = this.RunCuerpo;
			persona.RunDigito = this.RunDigito;
			persona.Nombres = this.Nombres;
			persona.ApellidoPaterno = this.ApellidoPaterno;
			persona.ApellidoMaterno = this.ApellidoMaterno;
			persona.Email = this.Email;
			persona.SexoCodigo = this.SexoCodigo;
			persona.FechaNacimiento = this.FechaNacimiento == default(DateTime) ? null : this.FechaNacimiento;
			persona.NacionalidadCodigo = this.NacionalidadCodigo == default(Int16) ? null : this.NacionalidadCodigo;
			persona.EstadoCivilCodigo = this.EstadoCivilCodigo == default(Int16) ? null : this.EstadoCivilCodigo;
			persona.NivelEducacionalCodigo = this.NivelEducacionalCodigo == default(Int32) ? null : this.NivelEducacionalCodigo;
			persona.RegionCodigo = this.RegionCodigo == default(Int16) ? null : this.RegionCodigo;
			persona.CiudadCodigo = this.CiudadCodigo == default(Int16) ? null : this.CiudadCodigo;
			persona.ComunaCodigo = this.ComunaCodigo == default(Int16) ? null : this.ComunaCodigo;
			persona.VillaPoblacion = this.VillaPoblacion;
			persona.Direccion = this.Direccion;
			persona.Telefono = this.Telefono == default(Int32) ? null : this.Telefono;
			persona.Celular = this.Celular == default(Int32) ? null : this.Celular;
			persona.Observaciones = this.Observaciones;
			persona.Ocupacion = this.Ocupacion;
			persona.TelefonoLaboral = this.TelefonoLaboral == default(Int32) ? null : this.TelefonoLaboral;
			persona.DireccionLaboral = this.DireccionLaboral;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Persona persona = context.Personas.SingleOrDefault<Persona>(x => x == this);

			if (persona != null)
			{
				context.Personas.DeleteOnSubmit(persona);
			}
			PostDelete(context);
		}
	}
}