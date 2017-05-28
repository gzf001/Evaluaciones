using System;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
	public partial class Prueba
	{
		partial void PreSave(Context context);
		partial void PostSave(Context context);
		partial void PreDelete(Context context);
		partial void PostDelete(Context context);

		public void Save(Context context)
		{
			PreSave(context);
			Prueba prueba = context.Pruebas.SingleOrDefault<Prueba>(x => x == this);

			if (prueba == null)
			{
				prueba = new Prueba
				{
					Id = this.Id
				};

				context.Pruebas.InsertOnSubmit(prueba);
			}

			prueba.TipoEducacionCodigo = this.TipoEducacionCodigo;
			prueba.GradoCodigo = this.GradoCodigo;
			prueba.SectorId = this.SectorId;
			prueba.PersonaId = this.PersonaId;
			prueba.Numero = this.Numero;
			prueba.Nombre = this.Nombre;
			prueba.Fecha = this.Fecha;
			prueba.Puntaje = this.Puntaje;
			prueba.UsoComunal = this.UsoComunal;
			prueba.Descripcion = this.Descripcion;
			PostSave(context);
		}

		public void Delete(Context context)
		{
			PreDelete(context);
			Prueba prueba = context.Pruebas.SingleOrDefault<Prueba>(x => x == this);

			if (prueba != null)
			{
				context.Pruebas.DeleteOnSubmit(prueba);
			}
			PostDelete(context);
		}
	}
}