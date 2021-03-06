using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using FluentLinqToSql;
namespace Evaluaciones.Membresia
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
					if (HttpContext.Current.Items["Membresia_Context"] == null)
					{
						HttpContext.Current.Items["Membresia_Context"] = new Context();
					}

					return (Context)HttpContext.Current.Items["Membresia_Context"];
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
				.AddFromAssemblyContaining<Evaluaciones.Membresia.Context>()
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

			if (HostingEnvironment.IsHosted) HttpContext.Current.Items["Membresia_Context"] = null;
			else Context.instancia = null;
		}

		#region Tables
		public Table<Evaluaciones.Membresia.Accion> Acciones
		{
			get { return this.GetTable<Evaluaciones.Membresia.Accion>(); }
		}

		public Table<Evaluaciones.Membresia.Aplicacion> Aplicaciones
		{
			get { return this.GetTable<Evaluaciones.Membresia.Aplicacion>(); }
		}

		public Table<Evaluaciones.Membresia.AplicacionPerfil> AplicacionPerfiles
		{
			get { return this.GetTable<Evaluaciones.Membresia.AplicacionPerfil>(); }
		}

		public Table<Evaluaciones.Membresia.Auditoria> Auditorias
		{
			get { return this.GetTable<Evaluaciones.Membresia.Auditoria>(); }
		}

		public Table<Evaluaciones.Membresia.CentroCostoAplicacion> CentroCostoAplicaciones
		{
			get { return this.GetTable<Evaluaciones.Membresia.CentroCostoAplicacion>(); }
		}

		public Table<Evaluaciones.Membresia.EmpresaAplicacion> EmpresaAplicaciones
		{
			get { return this.GetTable<Evaluaciones.Membresia.EmpresaAplicacion>(); }
		}

		public Table<Evaluaciones.Membresia.Menu> Menus
		{
			get { return this.GetTable<Evaluaciones.Membresia.Menu>(); }
		}

		public Table<Evaluaciones.Membresia.MenuItem> MenuItemes
		{
			get { return this.GetTable<Evaluaciones.Membresia.MenuItem>(); }
		}

		public Table<Evaluaciones.Membresia.MenuItemAccion> MenuItemAcciones
		{
			get { return this.GetTable<Evaluaciones.Membresia.MenuItemAccion>(); }
		}

		public Table<Evaluaciones.Membresia.Perfil> Perfiles
		{
			get { return this.GetTable<Evaluaciones.Membresia.Perfil>(); }
		}

		public Table<Evaluaciones.Membresia.PerfilUsuario> PerfilUsuarios
		{
			get { return this.GetTable<Evaluaciones.Membresia.PerfilUsuario>(); }
		}

		public Table<Evaluaciones.Membresia.Rol> Roles
		{
			get { return this.GetTable<Evaluaciones.Membresia.Rol>(); }
		}

		public Table<Evaluaciones.Membresia.RolAccion> RolAcciones
		{
			get { return this.GetTable<Evaluaciones.Membresia.RolAccion>(); }
		}

		public Table<Evaluaciones.Membresia.RolPersona> RolPersonas
		{
			get { return this.GetTable<Evaluaciones.Membresia.RolPersona>(); }
		}

		public Table<Evaluaciones.Membresia.RolPersonaCentroCosto> RolPersonaCentroCostos
		{
			get { return this.GetTable<Evaluaciones.Membresia.RolPersonaCentroCosto>(); }
		}

		public Table<Evaluaciones.Membresia.RolPersonaEmpresa> RolPersonaEmpresas
		{
			get { return this.GetTable<Evaluaciones.Membresia.RolPersonaEmpresa>(); }
		}

		public Table<Evaluaciones.Membresia.Usuario> Usuarios
		{
			get { return this.GetTable<Evaluaciones.Membresia.Usuario>(); }
		}
		#endregion

		#region Views
		#endregion

		#region StoredProcedures
		#endregion

		#region Functions
		#endregion

		#region Configuracion
		public class AccionConfiguration : Mapping<Evaluaciones.Membresia.Accion>
		{
			public AccionConfiguration()
			{
				this.Named("Membresia.Accion");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class AplicacionConfiguration : Mapping<Evaluaciones.Membresia.Aplicacion>
		{
			public AplicacionConfiguration()
			{
				this.Named("Membresia.Aplicacion");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Nullable<Guid>>(x => x.MenuId).Named("MenuId").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<Guid>>(x => x.MenuItemId).Named("MenuItemId").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Clave).Named("Clave").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Fa_Icon).Named("Fa_Icon").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Byte>(x => x.Orden).Named("Orden").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Membresia.MenuItem>(x => x.Inicio).ThisKey("Id,MenuId,MenuItemId").OtherKey("AplicacionId,MenuId,Id").Storage("inicio");
			}
		}

		public class AplicacionPerfilConfiguration : Mapping<Evaluaciones.Membresia.AplicacionPerfil>
		{
			public AplicacionPerfilConfiguration()
			{
				this.Named("Membresia.AplicacionPerfil");

				this.Map<Guid>(x => x.AplicacionId).Named("AplicacionId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.PerfilCodigo).Named("PerfilCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<Evaluaciones.Membresia.Aplicacion>(x => x.Aplicacion).ThisKey("AplicacionId").OtherKey("Id").Storage("aplicacion");
				this.HasOne<Evaluaciones.Membresia.Perfil>(x => x.Perfil).ThisKey("PerfilCodigo").OtherKey("Codigo").Storage("perfil");
			}
		}

		public class AuditoriaConfiguration : Mapping<Evaluaciones.Membresia.Auditoria>
		{
			public AuditoriaConfiguration()
			{
				this.Named("Membresia.Auditoria");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.UsuarioId).Named("UsuarioId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.AplicacionId).Named("AplicacionId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.MenuId).Named("MenuId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.MenuItemId).Named("MenuItemId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Actividad).Named("Actividad").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<DateTime>(x => x.Fecha).Named("Fecha").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Membresia.Aplicacion>(x => x.Aplicacion).ThisKey("AplicacionId").OtherKey("Id").Storage("aplicacion");
				this.HasOne<Evaluaciones.Membresia.MenuItem>(x => x.MenuItem).ThisKey("AplicacionId,MenuId,MenuItemId").OtherKey("AplicacionId,MenuId,Id").Storage("menuItem");
				this.HasOne<Evaluaciones.Membresia.Usuario>(x => x.Usuario).ThisKey("UsuarioId").OtherKey("Id").Storage("usuario");
			}
		}

		public class CentroCostoAplicacionConfiguration : Mapping<Evaluaciones.Membresia.CentroCostoAplicacion>
		{
			public CentroCostoAplicacionConfiguration()
			{
				this.Named("Membresia.CentroCostoAplicacion");

				this.Map<Guid>(x => x.EmpresaId).Named("EmpresaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.CentroCostoId).Named("CentroCostoId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.AplicacionId).Named("AplicacionId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<Evaluaciones.Membresia.Aplicacion>(x => x.Aplicacion).ThisKey("AplicacionId").OtherKey("Id").Storage("aplicacion");
				this.HasOne<Evaluaciones.CentroCosto>(x => x.CentroCosto).ThisKey("EmpresaId,CentroCostoId").OtherKey("EmpresaId,Id").Storage("centroCosto");
				this.HasOne<Evaluaciones.Membresia.EmpresaAplicacion>(x => x.EmpresaAplicacion).ThisKey("EmpresaId,AplicacionId").OtherKey("EmpresaId,AplicacionId").Storage("empresaAplicacion");
			}
		}

		public class EmpresaAplicacionConfiguration : Mapping<Evaluaciones.Membresia.EmpresaAplicacion>
		{
			public EmpresaAplicacionConfiguration()
			{
				this.Named("Membresia.EmpresaAplicacion");

				this.Map<Guid>(x => x.EmpresaId).Named("EmpresaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.AplicacionId).Named("AplicacionId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<Evaluaciones.Membresia.Aplicacion>(x => x.Aplicacion).ThisKey("AplicacionId").OtherKey("Id").Storage("aplicacion");
				this.HasOne<Evaluaciones.Empresa>(x => x.Empresa).ThisKey("EmpresaId").OtherKey("Id").Storage("empresa");
			}
		}

		public class MenuConfiguration : Mapping<Evaluaciones.Membresia.Menu>
		{
			public MenuConfiguration()
			{
				this.Named("Membresia.Menu");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Clave).Named("Clave").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class MenuItemConfiguration : Mapping<Evaluaciones.Membresia.MenuItem>
		{
			public MenuItemConfiguration()
			{
				this.Named("Membresia.MenuItem");

				this.Map<Guid>(x => x.AplicacionId).Named("AplicacionId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.MenuId).Named("MenuId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Nullable<Guid>>(x => x.MenuItemId).Named("MenuItemId").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Informacion).Named("Informacion").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Icono).Named("Icono").UpdateCheck(UpdateCheck.Never);
				this.Map<String>(x => x.Url).Named("Url").UpdateCheck(UpdateCheck.Never);
				this.Map<Boolean>(x => x.Visible).Named("Visible").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Boolean>(x => x.MuestraMenus).Named("MuestraMenus").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Int32>(x => x.Orden).Named("Orden").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Membresia.Aplicacion>(x => x.Aplicacion).ThisKey("AplicacionId").OtherKey("Id").Storage("aplicacion");
				this.HasOne<Evaluaciones.Membresia.Menu>(x => x.Menu).ThisKey("MenuId").OtherKey("Id").Storage("menu");
				this.HasOne<Evaluaciones.Membresia.MenuItem>(x => x.Padre).ThisKey("AplicacionId,MenuId,MenuItemId").OtherKey("AplicacionId,MenuId,Id").Storage("padre");
			}
		}

		public class MenuItemAccionConfiguration : Mapping<Evaluaciones.Membresia.MenuItemAccion>
		{
			public MenuItemAccionConfiguration()
			{
				this.Named("Membresia.MenuItemAccion");

				this.Map<Guid>(x => x.AplicacionId).Named("AplicacionId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.MenuId).Named("MenuId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.MenuItemId).Named("MenuItemId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AccionCodigo).Named("AccionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<Evaluaciones.Membresia.Accion>(x => x.Accion).ThisKey("AccionCodigo").OtherKey("Codigo").Storage("accion");
				this.HasOne<Evaluaciones.Membresia.MenuItem>(x => x.MenuItem).ThisKey("AplicacionId,MenuId,MenuItemId").OtherKey("AplicacionId,MenuId,Id").Storage("menuItem");
			}
		}

		public class PerfilConfiguration : Mapping<Evaluaciones.Membresia.Perfil>
		{
			public PerfilConfiguration()
			{
				this.Named("Membresia.Perfil");

				this.Map<Int32>(x => x.Codigo).Named("Codigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();

			}
		}

		public class PerfilUsuarioConfiguration : Mapping<Evaluaciones.Membresia.PerfilUsuario>
		{
			public PerfilUsuarioConfiguration()
			{
				this.Named("Membresia.PerfilUsuario");

				this.Map<Int32>(x => x.PerfilCodigo).Named("PerfilCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.UsuarioId).Named("UsuarioId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Valor).Named("Valor").UpdateCheck(UpdateCheck.Never).NotNull();

				this.HasOne<Evaluaciones.Membresia.Perfil>(x => x.Perfil).ThisKey("PerfilCodigo").OtherKey("Codigo").Storage("perfil");
				this.HasOne<Evaluaciones.Membresia.Usuario>(x => x.Usuario).ThisKey("UsuarioId").OtherKey("Id").Storage("usuario");
			}
		}

		public class RolConfiguration : Mapping<Evaluaciones.Membresia.Rol>
		{
			public RolConfiguration()
			{
				this.Named("Membresia.Rol");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AmbitoCodigo).Named("AmbitoCodigo").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Nombre).Named("Nombre").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<String>(x => x.Clave).Named("Clave").UpdateCheck(UpdateCheck.Never);

				this.HasOne<Evaluaciones.Ambito>(x => x.Ambito).ThisKey("AmbitoCodigo").OtherKey("Codigo").Storage("ambito");
			}
		}

		public class RolAccionConfiguration : Mapping<Evaluaciones.Membresia.RolAccion>
		{
			public RolAccionConfiguration()
			{
				this.Named("Membresia.RolAccion");

				this.Map<Guid>(x => x.RolId).Named("RolId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.AplicacionId).Named("AplicacionId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.MenuId).Named("MenuId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.MenuItemId).Named("MenuItemId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Int32>(x => x.AccionCodigo).Named("AccionCodigo").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<Evaluaciones.Membresia.Accion>(x => x.Accion).ThisKey("AccionCodigo").OtherKey("Codigo").Storage("accion");
				this.HasOne<Evaluaciones.Membresia.MenuItem>(x => x.MenuItem).ThisKey("AplicacionId,MenuId,MenuItemId").OtherKey("AplicacionId,MenuId,Id").Storage("menuItem");
				this.HasOne<Evaluaciones.Membresia.MenuItemAccion>(x => x.MenuItemAccion).ThisKey("AplicacionId,MenuId,MenuItemId,AccionCodigo").OtherKey("AplicacionId,MenuId,MenuItemId,AccionCodigo").Storage("menuItemAccion");
				this.HasOne<Evaluaciones.Membresia.Rol>(x => x.Rol).ThisKey("RolId").OtherKey("Id").Storage("rol");
			}
		}

		public class RolPersonaConfiguration : Mapping<Evaluaciones.Membresia.RolPersona>
		{
			public RolPersonaConfiguration()
			{
				this.Named("Membresia.RolPersona");

				this.Map<Guid>(x => x.RolId).Named("RolId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.PersonaId).Named("PersonaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<Evaluaciones.Persona>(x => x.Persona).ThisKey("PersonaId").OtherKey("Id").Storage("persona");
				this.HasOne<Evaluaciones.Membresia.Rol>(x => x.Rol).ThisKey("RolId").OtherKey("Id").Storage("rol");
			}
		}

		public class RolPersonaCentroCostoConfiguration : Mapping<Evaluaciones.Membresia.RolPersonaCentroCosto>
		{
			public RolPersonaCentroCostoConfiguration()
			{
				this.Named("Membresia.RolPersonaCentroCosto");

				this.Map<Guid>(x => x.RolId).Named("RolId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.PersonaId).Named("PersonaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.EmpresaId).Named("EmpresaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.CentroCostoId).Named("CentroCostoId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<Evaluaciones.CentroCosto>(x => x.CentroCosto).ThisKey("EmpresaId,CentroCostoId").OtherKey("EmpresaId,Id").Storage("centroCosto");
				this.HasOne<Evaluaciones.Empresa>(x => x.Empresa).ThisKey("EmpresaId").OtherKey("Id").Storage("empresa");
				this.HasOne<Evaluaciones.Persona>(x => x.Persona).ThisKey("PersonaId").OtherKey("Id").Storage("persona");
				this.HasOne<Evaluaciones.Membresia.Rol>(x => x.Rol).ThisKey("RolId").OtherKey("Id").Storage("rol");
			}
		}

		public class RolPersonaEmpresaConfiguration : Mapping<Evaluaciones.Membresia.RolPersonaEmpresa>
		{
			public RolPersonaEmpresaConfiguration()
			{
				this.Named("Membresia.RolPersonaEmpresa");

				this.Map<Guid>(x => x.RolId).Named("RolId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.PersonaId).Named("PersonaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<Guid>(x => x.EmpresaId).Named("EmpresaId").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();

				this.HasOne<Evaluaciones.Empresa>(x => x.Empresa).ThisKey("EmpresaId").OtherKey("Id").Storage("empresa");
				this.HasOne<Evaluaciones.Persona>(x => x.Persona).ThisKey("PersonaId").OtherKey("Id").Storage("persona");
				this.HasOne<Evaluaciones.Membresia.Rol>(x => x.Rol).ThisKey("RolId").OtherKey("Id").Storage("rol");
			}
		}

		public class UsuarioConfiguration : Mapping<Evaluaciones.Membresia.Usuario>
		{
			public UsuarioConfiguration()
			{
				this.Named("Membresia.Usuario");

				this.Map<Guid>(x => x.Id).Named("Id").PrimaryKey().UpdateCheck(UpdateCheck.Always).NotNull();
				this.Map<String>(x => x.Password).Named("Password").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Boolean>(x => x.Aprobado).Named("Aprobado").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Boolean>(x => x.Bloqueado).Named("Bloqueado").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<DateTime>(x => x.Creacion).Named("Creacion").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<DateTime>(x => x.UltimaActividad).Named("UltimaActividad").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<DateTime>(x => x.UltimoAcceso).Named("UltimoAcceso").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Nullable<DateTime>>(x => x.UltimoCambioPassword).Named("UltimoCambioPassword").UpdateCheck(UpdateCheck.Never);
				this.Map<Nullable<DateTime>>(x => x.UltimoDesbloqueo).Named("UltimoDesbloqueo").UpdateCheck(UpdateCheck.Never);
				this.Map<Int16>(x => x.NumeroIntentosFallidos).Named("NumeroIntentosFallidos").UpdateCheck(UpdateCheck.Never).NotNull();
				this.Map<Nullable<DateTime>>(x => x.FechaIntentoFallido).Named("FechaIntentoFallido").UpdateCheck(UpdateCheck.Never);

				this.HasOne<Evaluaciones.Persona>(x => x.Persona).ThisKey("Id").OtherKey("Id").Storage("persona");
			}
		}
		#endregion
	}
}