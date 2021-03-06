using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using FluentLinqToSql;
namespace Evaluaciones.Educacion
{
	public partial class Context : Evaluaciones.Context
	{
		private static readonly MappingSource mappingSource;
		private static Context instancia;

		#region Singleton
		public new static Context Instancia
		{
			get
			{
				if (HostingEnvironment.IsHosted)
				{
					if (HttpContext.Current.Items["Educacion_Context"] == null)
					{
						HttpContext.Current.Items["Educacion_Context"] = new Context();
					}

					return (Context)HttpContext.Current.Items["Educacion_Context"];
				}
				else
				{
					if (instancia == null) instancia = new Context { DeferredLoadingEnabled = true };

					return instancia;
				}
			}
		}
		#endregion

		static Context()
		{
			mappingSource = new FluentMappingSource("Evaluaciones")
				.AddFromAssemblyContaining<Evaluaciones.Educacion.Context>()
				.CreateMappingSource();

			instancia = new Context
			{
				DeferredLoadingEnabled = true
			};
		}

		public Context() : base(mappingSource) { }

		public Context(MappingSource mappingSource) : base(mappingSource) { }

		public override void SubmitChanges(ConflictMode failureMode)
		{
			base.SubmitChanges(failureMode);

			if (HostingEnvironment.IsHosted) HttpContext.Current.Items["Educacion_Context"] = null;
			else Context.instancia = null;
		}

		#region Tables
		public Table<Evaluaciones.Educacion.Grado> Grados
		{
			get { return this.GetTable<Evaluaciones.Educacion.Grado>(); }
		}

		public Table<Evaluaciones.Educacion.TipoEducacion> TipoEducaciones
		{
			get { return this.GetTable<Evaluaciones.Educacion.TipoEducacion>(); }
		}
		#endregion

		#region Views
		#endregion

		#region StoredProcedures
		#endregion

		#region Functions
		#endregion

		#region Configuracion
		public class GradoConfiguration : Mapping<Evaluaciones.Educacion.Grado>
		{
			public GradoConfiguration()
			{
				this.Named("Educacion.Grado");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Nullable<Int32>>(x => x.SiguienteTipoEducacionCodigo).Named("SiguienteTipoEducacionCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.SiguienteGradoCodigo).Named("SiguienteGradoCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Boolean>(x => x.BaseCurricular).Named("BaseCurricular").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Educacion.Grado>(x => x.SiguienteGrado).ThisKey("SiguienteTipoEducacionCodigo,SiguienteGradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("siguienteGrado");
				this.HasOne<Evaluaciones.Educacion.TipoEducacion>(x => x.SiguienteTipoEducacion).ThisKey("SiguienteTipoEducacionCodigo").OtherKey("Codigo").Storage("siguienteTipoEducacion");
				this.HasOne<Evaluaciones.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
			}
		}

		public class TipoEducacionConfiguration : Mapping<Evaluaciones.Educacion.TipoEducacion>
		{
			public TipoEducacionConfiguration()
			{
				this.Named("Educacion.TipoEducacion");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}
		#endregion
	}
}