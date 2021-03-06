using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using FluentLinqToSql;
namespace Evaluaciones.Evaluacion
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
					if (HttpContext.Current.Items["Evaluacion_Context"] == null)
					{
						HttpContext.Current.Items["Evaluacion_Context"] = new Context();
					}

					return (Context)HttpContext.Current.Items["Evaluacion_Context"];
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
				.AddFromAssemblyContaining<Evaluaciones.Context>()
				.AddFromAssemblyContaining<Evaluaciones.Educacion.Context>()
				.AddFromAssemblyContaining<Evaluaciones.Evaluacion.Context>()
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

			if (HostingEnvironment.IsHosted) HttpContext.Current.Items["Evaluacion_Context"] = null;
			else Context.instancia = null;
		}

		#region Tables
		public Table<Evaluaciones.Evaluacion.Dificultad> Dificultades
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.Dificultad>(); }
		}

		public Table<Evaluaciones.Evaluacion.EmpresaPregunta> EmpresaPreguntas
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.EmpresaPregunta>(); }
		}

		public Table<Evaluaciones.Evaluacion.Evaluacion> Evaluaciones
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.Evaluacion>(); }
		}

		public Table<Evaluaciones.Evaluacion.EvaluacionPrueba> EvaluacionPruebas
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.EvaluacionPrueba>(); }
		}

		public Table<Evaluaciones.Evaluacion.Habilidad> Habilidades
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.Habilidad>(); }
		}

		public Table<Evaluaciones.Evaluacion.Pregunta> Preguntas
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.Pregunta>(); }
		}

		public Table<Evaluaciones.Evaluacion.PreguntaAlternativa> PreguntaAlternativas
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.PreguntaAlternativa>(); }
		}

		public Table<Evaluaciones.Evaluacion.PreguntaBaseCurricular> PreguntaBaseCurriculares
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.PreguntaBaseCurricular>(); }
		}

		public Table<Evaluaciones.Evaluacion.PreguntaRecursoCurricular> PreguntaRecursoCurriculares
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.PreguntaRecursoCurricular>(); }
		}

		public Table<Evaluaciones.Evaluacion.PreguntaSeleccionMultiple> PreguntaSeleccionMultiples
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.PreguntaSeleccionMultiple>(); }
		}

		public Table<Evaluaciones.Evaluacion.PreguntaSeleccionMultipleCorrecta> PreguntaSeleccionMultipleCorrectas
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.PreguntaSeleccionMultipleCorrecta>(); }
		}

		public Table<Evaluaciones.Evaluacion.PreguntaVerdaderoFalso> PreguntaVerdaderoFalsos
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.PreguntaVerdaderoFalso>(); }
		}

		public Table<Evaluaciones.Evaluacion.Prueba> Pruebas
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.Prueba>(); }
		}

		public Table<Evaluaciones.Evaluacion.PruebaPregunta> PruebaPreguntas
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.PruebaPregunta>(); }
		}

		public Table<Evaluaciones.Evaluacion.RecursosPme> RecursosPmes
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.RecursosPme>(); }
		}

		public Table<Evaluaciones.Evaluacion.Referencia> Referencias
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.Referencia>(); }
		}

		public Table<Evaluaciones.Evaluacion.TipoEvaluacion> TipoEvaluaciones
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.TipoEvaluacion>(); }
		}

		public Table<Evaluaciones.Evaluacion.TipoReactivo> TipoReactivos
		{
			get { return this.GetTable<Evaluaciones.Evaluacion.TipoReactivo>(); }
		}
		#endregion

		#region Views
		#endregion

		#region StoredProcedures
		#endregion

		#region Functions
		#endregion

		#region Configuracion
		public class DificultadConfiguration : Mapping<Evaluaciones.Evaluacion.Dificultad>
		{
			public DificultadConfiguration()
			{
				this.Named("Evaluacion.Dificultad");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class EmpresaPreguntaConfiguration : Mapping<Evaluaciones.Evaluacion.EmpresaPregunta>
		{
			public EmpresaPreguntaConfiguration()
			{
				this.Named("Evaluacion.EmpresaPregunta");

				this.Map<Guid>(x => x.EmpresaId).Named("EmpresaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.PreguntaId).Named("PreguntaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<DateTime>(x => x.Fecha).Named("Fecha").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Detalle).Named("Detalle").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Empresa>(x => x.Empresa).ThisKey("EmpresaId").OtherKey("Id").Storage("empresa");
				this.HasOne<Evaluaciones.Evaluacion.Pregunta>(x => x.Pregunta).ThisKey("TipoEducacionCodigo,GradoCodigo,SectorId,PreguntaId").OtherKey("TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("pregunta");
			}
		}

		public class EvaluacionConfiguration : Mapping<Evaluaciones.Evaluacion.Evaluacion>
		{
			public EvaluacionConfiguration()
			{
				this.Named("Evaluacion.Evaluacion");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnioNumero).Named("AnioNumero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Int32>(x => x.TipoEvaluacionCodigo).Named("TipoEvaluacionCodigo").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Int32>(x => x.AmbitoCodigo).Named("AmbitoCodigo").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Nullable<Guid>>(x => x.SostenedorId).Named("SostenedorId").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Guid>>(x => x.EstablecimientoId).Named("EstablecimientoId").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Anio>(x => x.Anio).ThisKey("AnioNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<Evaluaciones.Evaluacion.TipoEvaluacion>(x => x.TipoEvaluacion).ThisKey("TipoEvaluacionCodigo").OtherKey("Codigo").Storage("tipoEvaluacion");
			}
		}

		public class EvaluacionPruebaConfiguration : Mapping<Evaluaciones.Evaluacion.EvaluacionPrueba>
		{
			public EvaluacionPruebaConfiguration()
			{
				this.Named("Evaluacion.EvaluacionPrueba");

				this.Map<Guid>(x => x.EvaluacionId).Named("EvaluacionId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.PruebaId).Named("PruebaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<Evaluaciones.Evaluacion.Evaluacion>(x => x.Evaluacion).ThisKey("EvaluacionId").OtherKey("Id").Storage("evaluacion");
				this.HasOne<Evaluaciones.Evaluacion.Prueba>(x => x.Prueba).ThisKey("TipoEducacionCodigo,GradoCodigo,SectorId,PruebaId").OtherKey("TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("prueba");
			}
		}

		public class HabilidadConfiguration : Mapping<Evaluaciones.Evaluacion.Habilidad>
		{
			public HabilidadConfiguration()
			{
				this.Named("Evaluacion.Habilidad");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class PreguntaConfiguration : Mapping<Evaluaciones.Evaluacion.Pregunta>
		{
			public PreguntaConfiguration()
			{
				this.Named("Evaluacion.Pregunta");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Nullable<Guid>>(x => x.ReferenciaId).Named("ReferenciaId").UpdateCheck(UpdateCheck.Never);
				this.Map<Int32>(x => x.Codigo).Named("Codigo").UpdateCheck(UpdateCheck.Never).NotNull().DbGenerated();
				this.Map<Int32>(x => x.DificultadCodigo).Named("DificultadCodigo").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Int32>(x => x.TipoReactivoCodigo).Named("TipoReactivoCodigo").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Int32>(x => x.TipoEvaluacionCodigo).Named("TipoEvaluacionCodigo").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Int32>(x => x.HabilidadCodigo).Named("HabilidadCodigo").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Texto).Named("Texto").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Boolean>(x => x.Privado).Named("Privado").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Evaluacion.Dificultad>(x => x.Dificultad).ThisKey("DificultadCodigo").OtherKey("Codigo").Storage("dificultad");
				this.HasOne<Evaluaciones.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<Evaluaciones.Evaluacion.Habilidad>(x => x.Habilidad).ThisKey("HabilidadCodigo").OtherKey("Codigo").Storage("habilidad");
				this.HasOne<Evaluaciones.Evaluacion.Referencia>(x => x.Referencia).ThisKey("TipoEducacionCodigo,GradoCodigo,SectorId,ReferenciaId").OtherKey("TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("referencia");
				this.HasOne<Evaluaciones.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
				this.HasOne<Evaluaciones.Evaluacion.TipoEvaluacion>(x => x.TipoEvaluacion).ThisKey("TipoEvaluacionCodigo").OtherKey("Codigo").Storage("tipoEvaluacion");
				this.HasOne<Evaluaciones.Evaluacion.TipoReactivo>(x => x.TipoReactivo).ThisKey("TipoReactivoCodigo").OtherKey("Codigo").Storage("tipoReactivo");
			}
		}

		public class PreguntaAlternativaConfiguration : Mapping<Evaluaciones.Evaluacion.PreguntaAlternativa>
		{
			public PreguntaAlternativaConfiguration()
			{
				this.Named("Evaluacion.PreguntaAlternativa");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.PreguntaId).Named("PreguntaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Char>(x => x.AlternativaCorrecta).Named("AlternativaCorrecta").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.AlternativaA).Named("AlternativaA").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.AlternativaB).Named("AlternativaB").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.AlternativaC).Named("AlternativaC").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.AlternativaD).Named("AlternativaD").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.AlternativaE).Named("AlternativaE").UpdateCheck(UpdateCheck.Never);

				this.HasOne<Evaluaciones.Evaluacion.Pregunta>(x => x.Pregunta).ThisKey("TipoEducacionCodigo,GradoCodigo,SectorId,PreguntaId").OtherKey("TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("pregunta");
			}
		}

		public class PreguntaBaseCurricularConfiguration : Mapping<Evaluaciones.Evaluacion.PreguntaBaseCurricular>
		{
			public PreguntaBaseCurricularConfiguration()
			{
				this.Named("Evaluacion.PreguntaBaseCurricular");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.PreguntaId).Named("PreguntaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnioNumero).Named("AnioNumero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Guid>(x => x.UnidadId).Named("UnidadId").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Guid>(x => x.EjeId).Named("EjeId").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Guid>(x => x.ObjetivoAprendizajeId).Named("ObjetivoAprendizajeId").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Anio>(x => x.Anio).ThisKey("AnioNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<Evaluaciones.Evaluacion.Pregunta>(x => x.Pregunta).ThisKey("TipoEducacionCodigo,GradoCodigo,SectorId,PreguntaId").OtherKey("TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("pregunta");
			}
		}

		public class PreguntaRecursoCurricularConfiguration : Mapping<Evaluaciones.Evaluacion.PreguntaRecursoCurricular>
		{
			public PreguntaRecursoCurricularConfiguration()
			{
				this.Named("Evaluacion.PreguntaRecursoCurricular");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.PreguntaId).Named("PreguntaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.UnidadId).Named("UnidadId").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Guid>(x => x.AprendizajeId).Named("AprendizajeId").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Guid>(x => x.EjeId).Named("EjeId").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Guid>(x => x.ContenidoId).Named("ContenidoId").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Evaluacion.Pregunta>(x => x.Pregunta).ThisKey("TipoEducacionCodigo,GradoCodigo,SectorId,PreguntaId").OtherKey("TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("pregunta");
			}
		}

		public class PreguntaSeleccionMultipleConfiguration : Mapping<Evaluaciones.Evaluacion.PreguntaSeleccionMultiple>
		{
			public PreguntaSeleccionMultipleConfiguration()
			{
				this.Named("Evaluacion.PreguntaSeleccionMultiple");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.PreguntaId).Named("PreguntaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.AlternativaA).Named("AlternativaA").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.AlternativaB).Named("AlternativaB").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.AlternativaC).Named("AlternativaC").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.AlternativaD).Named("AlternativaD").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.AlternativaE).Named("AlternativaE").UpdateCheck(UpdateCheck.Never);

				this.HasOne<Evaluaciones.Evaluacion.Pregunta>(x => x.Pregunta).ThisKey("TipoEducacionCodigo,GradoCodigo,SectorId,PreguntaId").OtherKey("TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("pregunta");
			}
		}

		public class PreguntaSeleccionMultipleCorrectaConfiguration : Mapping<Evaluaciones.Evaluacion.PreguntaSeleccionMultipleCorrecta>
		{
			public PreguntaSeleccionMultipleCorrectaConfiguration()
			{
				this.Named("Evaluacion.PreguntaSeleccionMultipleCorrecta");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.PreguntaId).Named("PreguntaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Char>(x => x.Correcta).Named("Correcta").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Evaluacion.PreguntaSeleccionMultiple>(x => x.PreguntaSeleccionMultiple).ThisKey("TipoEducacionCodigo,GradoCodigo,SectorId,PreguntaId").OtherKey("TipoEducacionCodigo,GradoCodigo,SectorId,PreguntaId").Storage("preguntaSeleccionMultiple");
			}
		}

		public class PreguntaVerdaderoFalsoConfiguration : Mapping<Evaluaciones.Evaluacion.PreguntaVerdaderoFalso>
		{
			public PreguntaVerdaderoFalsoConfiguration()
			{
				this.Named("Evaluacion.PreguntaVerdaderoFalso");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.PreguntaId).Named("PreguntaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Boolean>(x => x.Respuesta).Named("Respuesta").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Evaluacion.Pregunta>(x => x.Pregunta).ThisKey("TipoEducacionCodigo,GradoCodigo,SectorId,PreguntaId").OtherKey("TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("pregunta");
			}
		}

		public class PruebaConfiguration : Mapping<Evaluaciones.Evaluacion.Prueba>
		{
			public PruebaConfiguration()
			{
				this.Named("Evaluacion.Prueba");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.PersonaId).Named("PersonaId").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull().DbGenerated();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<DateTime>(x => x.Fecha).Named("Fecha").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Int32>(x => x.Puntaje).Named("Puntaje").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Boolean>(x => x.UsoComunal).Named("UsoComunal").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Descripcion).Named("Descripcion").UpdateCheck(UpdateCheck.Never);

				this.HasOne<Evaluaciones.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<Evaluaciones.Persona>(x => x.Persona).ThisKey("PersonaId").OtherKey("Id").Storage("persona");
			}
		}

		public class PruebaPreguntaConfiguration : Mapping<Evaluaciones.Evaluacion.PruebaPregunta>
		{
			public PruebaPreguntaConfiguration()
			{
				this.Named("Evaluacion.PruebaPregunta");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.PruebaId).Named("PruebaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.PreguntaTipoEducacionCodigo).Named("PreguntaTipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.PreguntaGradoCodigo).Named("PreguntaGradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.PreguntaId).Named("PreguntaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Boolean>(x => x.Nula).Named("Nula").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Int32>(x => x.Orden).Named("Orden").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Evaluacion.Pregunta>(x => x.Pregunta).ThisKey("PreguntaTipoEducacionCodigo,PreguntaGradoCodigo,SectorId,PreguntaId").OtherKey("TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("pregunta");
				this.HasOne<Evaluaciones.Evaluacion.Prueba>(x => x.Prueba).ThisKey("TipoEducacionCodigo,GradoCodigo,SectorId,PruebaId").OtherKey("TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("prueba");
			}
		}

		public class RecursosPmeConfiguration : Mapping<Evaluaciones.Evaluacion.RecursosPme>
		{
			public RecursosPmeConfiguration()
			{
				this.Named("Evaluacion.RecursosPme");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.PreguntaId).Named("PreguntaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.AprendizajeId).Named("AprendizajeId").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Nullable<Guid>>(x => x.AprendizajeIndicadorId).Named("AprendizajeIndicadorId").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Guid>>(x => x.HabilidadId).Named("HabilidadId").UpdateCheck(UpdateCheck.Never);

				this.HasOne<Evaluaciones.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<Evaluaciones.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
				this.HasOne<Evaluaciones.Evaluacion.Pregunta>(x => x.Pregunta).ThisKey("TipoEducacionCodigo,GradoCodigo,SectorId,PreguntaId").OtherKey("TipoEducacionCodigo,GradoCodigo,SectorId,Id").Storage("pregunta");
			}
		}

		public class ReferenciaConfiguration : Mapping<Evaluaciones.Evaluacion.Referencia>
		{
			public ReferenciaConfiguration()
			{
				this.Named("Evaluacion.Referencia");

				this.Map<Int32>(x => x.TipoEducacionCodigo).Named("TipoEducacionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.GradoCodigo).Named("GradoCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.SectorId).Named("SectorId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Codigo).Named("Codigo").UpdateCheck(UpdateCheck.Never).NotNull().DbGenerated();
				this.Map<String>(x => x.Texto).Named("Texto").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Educacion.Grado>(x => x.Grado).ThisKey("TipoEducacionCodigo,GradoCodigo").OtherKey("TipoEducacionCodigo,Codigo").Storage("grado");
				this.HasOne<Evaluaciones.Educacion.TipoEducacion>(x => x.TipoEducacion).ThisKey("TipoEducacionCodigo").OtherKey("Codigo").Storage("tipoEducacion");
			}
		}

		public class TipoEvaluacionConfiguration : Mapping<Evaluaciones.Evaluacion.TipoEvaluacion>
		{
			public TipoEvaluacionConfiguration()
			{
				this.Named("Evaluacion.TipoEvaluacion");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class TipoReactivoConfiguration : Mapping<Evaluaciones.Evaluacion.TipoReactivo>
		{
			public TipoReactivoConfiguration()
			{
				this.Named("Evaluacion.TipoReactivo");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}
		#endregion
	}
}