using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using FluentLinqToSql;
namespace Evaluaciones
{
	public partial class Context : DataContext
	{
		protected static readonly string connectionString = ConfigurationManager.ConnectionStrings["Evaluaciones"].ConnectionString;
		private static readonly MappingSource mappingSource;
		private static Context instancia;

		#region Singleton
		public static Context Instancia
		{
			get
			{
				if (HostingEnvironment.IsHosted)
				{
					if (HttpContext.Current.Items["Evaluaciones_Context"] == null)
					{
						HttpContext.Current.Items["Evaluaciones_Context"] = new Context();
					}

					return (Context)HttpContext.Current.Items["Evaluaciones_Context"];
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
				.CreateMappingSource();

			instancia = new Context
			{
				DeferredLoadingEnabled = true
			};
		}

		public Context() : base(connectionString, mappingSource) { }

		public Context(MappingSource mappingSource) : base(connectionString, mappingSource) { }

		public override void SubmitChanges(ConflictMode failureMode)
		{
			base.SubmitChanges(failureMode);

			if (HostingEnvironment.IsHosted) HttpContext.Current.Items["Evaluaciones_Context"] = null;
			else Context.instancia = null;
		}

		#region Tables
		public Table<Evaluaciones.Ambito> Ambitos
		{
			get { return this.GetTable<Evaluaciones.Ambito>(); }
		}

		public Table<Evaluaciones.Anio> Anios
		{
			get { return this.GetTable<Evaluaciones.Anio>(); }
		}

		public Table<Evaluaciones.AnioMes> AnioMeses
		{
			get { return this.GetTable<Evaluaciones.AnioMes>(); }
		}

		public Table<Evaluaciones.AreaGeografica> AreaGeograficas
		{
			get { return this.GetTable<Evaluaciones.AreaGeografica>(); }
		}

		public Table<Evaluaciones.Calendario> Calendarios
		{
			get { return this.GetTable<Evaluaciones.Calendario>(); }
		}

		public Table<Evaluaciones.CentroCosto> CentroCostos
		{
			get { return this.GetTable<Evaluaciones.CentroCosto>(); }
		}

		public Table<Evaluaciones.Ciudad> Ciudades
		{
			get { return this.GetTable<Evaluaciones.Ciudad>(); }
		}

		public Table<Evaluaciones.Comuna> Comunas
		{
			get { return this.GetTable<Evaluaciones.Comuna>(); }
		}

		public Table<Evaluaciones.Dia> Dias
		{
			get { return this.GetTable<Evaluaciones.Dia>(); }
		}

		public Table<Evaluaciones.Empresa> Empresas
		{
			get { return this.GetTable<Evaluaciones.Empresa>(); }
		}

		public Table<Evaluaciones.EstadoCivil> EstadoCiviles
		{
			get { return this.GetTable<Evaluaciones.EstadoCivil>(); }
		}

		public Table<Evaluaciones.Feriado> Feriados
		{
			get { return this.GetTable<Evaluaciones.Feriado>(); }
		}

		public Table<Evaluaciones.Mes> Meses
		{
			get { return this.GetTable<Evaluaciones.Mes>(); }
		}

		public Table<Evaluaciones.Nacionalidad> Nacionalidades
		{
			get { return this.GetTable<Evaluaciones.Nacionalidad>(); }
		}

		public Table<Evaluaciones.NivelEducacional> NivelEducacionales
		{
			get { return this.GetTable<Evaluaciones.NivelEducacional>(); }
		}

		public Table<Evaluaciones.Persona> Personas
		{
			get { return this.GetTable<Evaluaciones.Persona>(); }
		}

		public Table<Evaluaciones.PuebloIndigena> PuebloIndigenas
		{
			get { return this.GetTable<Evaluaciones.PuebloIndigena>(); }
		}

		public Table<Evaluaciones.Region> Regiones
		{
			get { return this.GetTable<Evaluaciones.Region>(); }
		}

		public Table<Evaluaciones.Secuencia> Secuencias
		{
			get { return this.GetTable<Evaluaciones.Secuencia>(); }
		}

		public Table<Evaluaciones.SecuenciaCentroCosto> SecuenciaCentroCostos
		{
			get { return this.GetTable<Evaluaciones.SecuenciaCentroCosto>(); }
		}

		public Table<Evaluaciones.SecuenciaEmpresa> SecuenciaEmpresas
		{
			get { return this.GetTable<Evaluaciones.SecuenciaEmpresa>(); }
		}

		public Table<Evaluaciones.Semana> Semanas
		{
			get { return this.GetTable<Evaluaciones.Semana>(); }
		}

		public Table<Evaluaciones.Sexo> Sexos
		{
			get { return this.GetTable<Evaluaciones.Sexo>(); }
		}
		#endregion

		#region Views
		#endregion

		#region StoredProcedures
		#endregion

		#region Functions
		public String FormatInt(Int32 Number)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, (MethodInfo)MethodInfo.GetCurrentMethod(), Number);

			return (String)result.ReturnValue;
		}

		public Nullable<Char> GetRunDigito(Int32 RunCuerpo)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, (MethodInfo)MethodInfo.GetCurrentMethod(), RunCuerpo);

			return (Nullable<Char>)result.ReturnValue;
		}
		#endregion

		#region Configuracion
		public class AmbitoConfiguration : Mapping<Evaluaciones.Ambito>
		{
			public AmbitoConfiguration()
			{
				this.Named("dbo.Ambito");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class AnioConfiguration : Mapping<Evaluaciones.Anio>
		{
			public AnioConfiguration()
			{
				this.Named("dbo.Anio");

				this.Map<Int32>(x => x.Numero).Named("Numero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Boolean>(x => x.Activo).Named("Activo").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class AnioMesConfiguration : Mapping<Evaluaciones.AnioMes>
		{
			public AnioMesConfiguration()
			{
				this.Named("dbo.AnioMes");

				this.Map<Int32>(x => x.AnioNumero).Named("AnioNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.MesNumero).Named("MesNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<DateTime>(x => x.Inicio).Named("Inicio").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<DateTime>(x => x.Termino).Named("Termino").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Anio>(x => x.Anio).ThisKey("AnioNumero").OtherKey("Numero").Storage("anio");
				this.HasOne<Evaluaciones.Mes>(x => x.Mes).ThisKey("MesNumero").OtherKey("Numero").Storage("mes");
			}
		}

		public class AreaGeograficaConfiguration : Mapping<Evaluaciones.AreaGeografica>
		{
			public AreaGeograficaConfiguration()
			{
				this.Named("dbo.AreaGeografica");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class CalendarioConfiguration : Mapping<Evaluaciones.Calendario>
		{
			public CalendarioConfiguration()
			{
				this.Named("dbo.Calendario");

				this.Map<DateTime>(x => x.Fecha).Named("Fecha").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AnioNumero).Named("AnioNumero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Int32>(x => x.MesNumero).Named("MesNumero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Int32>(x => x.SemanaNumero).Named("SemanaNumero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Int16>(x => x.DiaNumero).Named("DiaNumero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Boolean>(x => x.Festivo).Named("Festivo").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.AnioMes>(x => x.AnioMes).ThisKey("AnioNumero,MesNumero").OtherKey("AnioNumero,MesNumero").Storage("anioMes");
				this.HasOne<Evaluaciones.Dia>(x => x.Dia).ThisKey("DiaNumero").OtherKey("Numero").Storage("dia");
				this.HasOne<Evaluaciones.Semana>(x => x.Semana).ThisKey("AnioNumero,MesNumero,SemanaNumero").OtherKey("AnioNumero,MesNumero,Numero").Storage("semana");
			}
		}

		public class CentroCostoConfiguration : Mapping<Evaluaciones.CentroCosto>
		{
			public CentroCostoConfiguration()
			{
				this.Named("dbo.CentroCosto");

				this.Map<Guid>(x => x.EmpresaId).Named("EmpresaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Rbd).Named("Rbd").UpdateCheck(UpdateCheck.Never).DbGenerated();
				this.Map<Int32>(x => x.RbdCuerpo).Named("RbdCuerpo").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Int32>(x => x.RbdDigito).Named("RbdDigito").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Sigla).Named("Sigla").UpdateCheck(UpdateCheck.Never);
				this.Map<Int32>(x => x.AreaGeograficaCodigo).Named("AreaGeograficaCodigo").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Nullable<Int16>>(x => x.RegionCodigo).Named("RegionCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int16>>(x => x.CiudadCodigo).Named("CiudadCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int16>>(x => x.ComunaCodigo).Named("ComunaCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Email).Named("Email").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Direccion).Named("Direccion").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.Telefono1).Named("Telefono1").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.Telefono2).Named("Telefono2").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.Fax).Named("Fax").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.Celular).Named("Celular").UpdateCheck(UpdateCheck.Never);
				this.Map<Int32>(x => x.AutorizacionNumero).Named("AutorizacionNumero").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<DateTime>(x => x.AutorizacionFecha).Named("AutorizacionFecha").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.AreaGeografica>(x => x.AreaGeografica).ThisKey("AreaGeograficaCodigo").OtherKey("Codigo").Storage("areaGeografica");
				this.HasOne<Evaluaciones.Comuna>(x => x.Comuna).ThisKey("RegionCodigo,CiudadCodigo,ComunaCodigo").OtherKey("RegionCodigo,CiudadCodigo,Codigo").Storage("comuna");
				this.HasOne<Evaluaciones.Empresa>(x => x.Empresa).ThisKey("EmpresaId").OtherKey("Id").Storage("empresa");
			}
		}

		public class CiudadConfiguration : Mapping<Evaluaciones.Ciudad>
		{
			public CiudadConfiguration()
			{
				this.Named("dbo.Ciudad");

				this.Map<Int16>(x => x.RegionCodigo).Named("RegionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int16>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Region>(x => x.Region).ThisKey("RegionCodigo").OtherKey("Codigo").Storage("region");
			}
		}

		public class ComunaConfiguration : Mapping<Evaluaciones.Comuna>
		{
			public ComunaConfiguration()
			{
				this.Named("dbo.Comuna");

				this.Map<Int16>(x => x.RegionCodigo).Named("RegionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int16>(x => x.CiudadCodigo).Named("CiudadCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int16>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Ciudad>(x => x.Ciudad).ThisKey("RegionCodigo,CiudadCodigo").OtherKey("RegionCodigo,Codigo").Storage("ciudad");
			}
		}

		public class DiaConfiguration : Mapping<Evaluaciones.Dia>
		{
			public DiaConfiguration()
			{
				this.Named("dbo.Dia");

				this.Map<Int16>(x => x.Numero).Named("Numero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Boolean>(x => x.Laboral).Named("Laboral").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class EmpresaConfiguration : Mapping<Evaluaciones.Empresa>
		{
			public EmpresaConfiguration()
			{
				this.Named("dbo.Empresa");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Rut).Named("Rut").UpdateCheck(UpdateCheck.Never).DbGenerated();
				this.Map<Int32>(x => x.RutCuerpo).Named("RutCuerpo").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Char>(x => x.RutDigito).Named("RutDigito").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.RazonSocial).Named("RazonSocial").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Nullable<Int16>>(x => x.RegionCodigo).Named("RegionCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int16>>(x => x.CiudadCodigo).Named("CiudadCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int16>>(x => x.ComunaCodigo).Named("ComunaCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Direccion).Named("Direccion").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Email).Named("Email").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.PaginaWeb).Named("PaginaWeb").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.Telefono1).Named("Telefono1").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.Telefono2).Named("Telefono2").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.Fax).Named("Fax").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.Celular).Named("Celular").UpdateCheck(UpdateCheck.Never);
				this.Map<Boolean>(x => x.Bloqueada).Named("Bloqueada").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Comuna>(x => x.Comuna).ThisKey("RegionCodigo,CiudadCodigo,ComunaCodigo").OtherKey("RegionCodigo,CiudadCodigo,Codigo").Storage("comuna");
			}
		}

		public class EstadoCivilConfiguration : Mapping<Evaluaciones.EstadoCivil>
		{
			public EstadoCivilConfiguration()
			{
				this.Named("dbo.EstadoCivil");

				this.Map<Int16>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class FeriadoConfiguration : Mapping<Evaluaciones.Feriado>
		{
			public FeriadoConfiguration()
			{
				this.Named("dbo.Feriado");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Nullable<Guid>>(x => x.EmpresaId).Named("EmpresaId").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Guid>>(x => x.CentroCostoId).Named("CentroCostoId").UpdateCheck(UpdateCheck.Never);
				this.Map<DateTime>(x => x.Fecha).Named("Fecha").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Calendario>(x => x.Calendario).ThisKey("Fecha").OtherKey("Fecha").Storage("calendario");
				this.HasOne<Evaluaciones.CentroCosto>(x => x.CentroCosto).ThisKey("EmpresaId,CentroCostoId").OtherKey("EmpresaId,Id").Storage("centroCosto");
				this.HasOne<Evaluaciones.Empresa>(x => x.Empresa).ThisKey("EmpresaId").OtherKey("Id").Storage("empresa");
			}
		}

		public class MesConfiguration : Mapping<Evaluaciones.Mes>
		{
			public MesConfiguration()
			{
				this.Named("dbo.Mes");

				this.Map<Int32>(x => x.Numero).Named("Numero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Inicial).Named("Inicial").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class NacionalidadConfiguration : Mapping<Evaluaciones.Nacionalidad>
		{
			public NacionalidadConfiguration()
			{
				this.Named("dbo.Nacionalidad");

				this.Map<Int16>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Boolean>(x => x.Extranjero).Named("Extranjero").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class NivelEducacionalConfiguration : Mapping<Evaluaciones.NivelEducacional>
		{
			public NivelEducacionalConfiguration()
			{
				this.Named("dbo.NivelEducacional");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class PersonaConfiguration : Mapping<Evaluaciones.Persona>
		{
			public PersonaConfiguration()
			{
				this.Named("dbo.Persona");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Run).Named("Run").UpdateCheck(UpdateCheck.Never).DbGenerated();
				this.Map<Int32>(x => x.RunCuerpo).Named("RunCuerpo").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Char>(x => x.RunDigito).Named("RunDigito").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).DbGenerated();
				this.Map<String>(x => x.Nombres).Named("Nombres").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.ApellidoPaterno).Named("ApellidoPaterno").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.ApellidoMaterno).Named("ApellidoMaterno").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Email).Named("Email").UpdateCheck(UpdateCheck.Never);
				this.Map<Int16>(x => x.SexoCodigo).Named("SexoCodigo").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Nullable<DateTime>>(x => x.FechaNacimiento).Named("FechaNacimiento").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int16>>(x => x.NacionalidadCodigo).Named("NacionalidadCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int16>>(x => x.EstadoCivilCodigo).Named("EstadoCivilCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.NivelEducacionalCodigo).Named("NivelEducacionalCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int16>>(x => x.RegionCodigo).Named("RegionCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int16>>(x => x.CiudadCodigo).Named("CiudadCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int16>>(x => x.ComunaCodigo).Named("ComunaCodigo").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.VillaPoblacion).Named("VillaPoblacion").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Direccion).Named("Direccion").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.Telefono).Named("Telefono").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.Celular).Named("Celular").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Observaciones).Named("Observaciones").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Ocupacion).Named("Ocupacion").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Int32>>(x => x.TelefonoLaboral).Named("TelefonoLaboral").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.DireccionLaboral).Named("DireccionLaboral").UpdateCheck(UpdateCheck.Never);

				this.HasOne<Evaluaciones.Comuna>(x => x.Comuna).ThisKey("RegionCodigo,CiudadCodigo,ComunaCodigo").OtherKey("RegionCodigo,CiudadCodigo,Codigo").Storage("comuna");
				this.HasOne<Evaluaciones.EstadoCivil>(x => x.EstadoCivil).ThisKey("EstadoCivilCodigo").OtherKey("Codigo").Storage("estadoCivil");
				this.HasOne<Evaluaciones.Nacionalidad>(x => x.Nacionalidad).ThisKey("NacionalidadCodigo").OtherKey("Codigo").Storage("nacionalidad");
				this.HasOne<Evaluaciones.NivelEducacional>(x => x.NivelEducacional).ThisKey("NivelEducacionalCodigo").OtherKey("Codigo").Storage("nivelEducacional");
				this.HasOne<Evaluaciones.Sexo>(x => x.Sexo).ThisKey("SexoCodigo").OtherKey("Codigo").Storage("sexo");
			}
		}

		public class PuebloIndigenaConfiguration : Mapping<Evaluaciones.PuebloIndigena>
		{
			public PuebloIndigenaConfiguration()
			{
				this.Named("dbo.PuebloIndigena");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class RegionConfiguration : Mapping<Evaluaciones.Region>
		{
			public RegionConfiguration()
			{
				this.Named("dbo.Region");

				this.Map<Int16>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class SecuenciaConfiguration : Mapping<Evaluaciones.Secuencia>
		{
			public SecuenciaConfiguration()
			{
				this.Named("dbo.Secuencia");

				this.Map<String>(x => x.Clave).Named("Clave").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class SecuenciaCentroCostoConfiguration : Mapping<Evaluaciones.SecuenciaCentroCosto>
		{
			public SecuenciaCentroCostoConfiguration()
			{
				this.Named("dbo.SecuenciaCentroCosto");

				this.Map<Guid>(x => x.EmpresaId).Named("EmpresaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.CentroCostoId).Named("CentroCostoId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Clave).Named("Clave").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.CentroCosto>(x => x.CentroCosto).ThisKey("EmpresaId,CentroCostoId").OtherKey("EmpresaId,Id").Storage("centroCosto");
				this.HasOne<Evaluaciones.Empresa>(x => x.Empresa).ThisKey("EmpresaId").OtherKey("Id").Storage("empresa");
			}
		}

		public class SecuenciaEmpresaConfiguration : Mapping<Evaluaciones.SecuenciaEmpresa>
		{
			public SecuenciaEmpresaConfiguration()
			{
				this.Named("dbo.SecuenciaEmpresa");

				this.Map<Guid>(x => x.EmpresaId).Named("EmpresaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Clave).Named("Clave").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Empresa>(x => x.Empresa).ThisKey("EmpresaId").OtherKey("Id").Storage("empresa");
			}
		}

		public class SemanaConfiguration : Mapping<Evaluaciones.Semana>
		{
			public SemanaConfiguration()
			{
				this.Named("dbo.Semana");

				this.Map<Int32>(x => x.AnioNumero).Named("AnioNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.MesNumero).Named("MesNumero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.Numero).Named("Numero").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<DateTime>(x => x.Inicio).Named("Inicio").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<DateTime>(x => x.Termino).Named("Termino").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.AnioMes>(x => x.AnioMes).ThisKey("AnioNumero,MesNumero").OtherKey("AnioNumero,MesNumero").Storage("anioMes");
			}
		}

		public class SexoConfiguration : Mapping<Evaluaciones.Sexo>
		{
			public SexoConfiguration()
			{
				this.Named("dbo.Sexo");

				this.Map<Int16>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Char>(x => x.Letra).Named("Letra").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class FunctionMapping : FunctionMapping<Context>
		{
			public FunctionMapping()
			{
				this.Map(x => x.FormatInt(
					Parameter<Int32>(param => param.Named("Number"))
					)).Named("dbo.FormatInt").Composable();

				this.Map(x => x.GetRunDigito(
					Parameter<Int32>(param => param.Named("RunCuerpo"))
					)).Named("dbo.GetRunDigito").Composable();
			}
		}
		#endregion
	}
}