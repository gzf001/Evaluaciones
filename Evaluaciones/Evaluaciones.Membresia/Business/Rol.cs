using System;
using System.Collections.Generic;
using System.Linq;

namespace Evaluaciones.Membresia
{
	public partial class Rol
	{
		public static Rol Alumno
		{
			get
			{
				return Rol.Get("alumno");
			}
		}

		public static Rol Apoderado
		{
			get
			{
				return Rol.Get("apoderado");
			}
		}

		public static Rol AdministradorRecursosPedagogicos
		{
			get
			{
				return Rol.Get(new Guid("783DA887-CEC8-4DFF-B3BC-31DA128F5E63"));
			}
		}

		public static Rol Profesor
		{
			get
			{
				return Rol.Get(new Guid("8EDC7FB4-2FAC-4CA2-9B58-0E8FA6529AC5"));
			}
		}

		public static Rol DirectivoRecursoPedagogico
		{
			get
			{
				return Rol.Get(new Guid("18C18160-7ABC-4564-B74F-BEF7DD30E11D"));
			}
		}

		public static Rol Get(Guid id)
		{
			return Query.GetRoles().SingleOrDefault<Rol>(x => x.Id == id);
		}

		public static Rol Get(string clave)
		{
			return Query.GetRoles().SingleOrDefault<Rol>(x => x.Clave == clave);
		}

		public static bool IsAlumno(Persona persona)
		{
			return Query.GetRolPersonaCentroCostos(persona).Any<RolPersonaCentroCosto>(x => x.Rol.Equals(Rol.Alumno));
		}

		public static List<Rol> GetAll()
		{
			return
				(
				from query in Query.GetRoles()
				orderby query.AmbitoCodigo, query.Clave, query.Nombre
				select query
				).ToList<Rol>();
		}

		public static List<Rol> GetAll(Aplicacion aplicacion)
		{
			return
				(
				from query in Query.GetRoles(aplicacion)
				orderby query.AmbitoCodigo, query.Nombre
				select query
				).ToList<Rol>();
		}

		public static List<Rol> GetAll(Evaluaciones.Ambito ambito, Aplicacion aplicacion, bool showProtected)
		{
			return
				(
				from query in Query.GetRoles(ambito, aplicacion, showProtected)
				orderby query.Clave, query.Nombre
				select query
				).ToList<Rol>();
		}

		public static List<Rol> GetAll(Evaluaciones.Ambito ambito, bool showProtected)
		{
			return
				(
				from query in Query.GetRoles(ambito, showProtected)
				orderby query.Clave, query.Nombre
				select query
				).ToList<Rol>();
		}

		public static List<Rol> GetAll(Evaluaciones.Ambito ambito, List<Aplicacion> aplicaciones, bool showProtected)
		{
			List<Rol> roles = new List<Rol>();

			foreach(Aplicacion aplicacion in aplicaciones)
			{
				foreach (Rol rol in Query.GetRoles(ambito, aplicacion, showProtected))
				{
					if (!roles.Contains<Rol>(rol))
					{
						roles.Add(rol);
					}
				}
			}

			return roles;
		}

		public static List<Rol> GetAll(Evaluaciones.Empresa empresa, Evaluaciones.CentroCosto centroCosto, Persona persona)
		{
			return
				(
				from query in Query.GetRoles(empresa, centroCosto, persona)
				orderby query.AmbitoCodigo, query.Clave, query.Nombre
				select query
				).ToList<Rol>();
		}

		public static List<Rol> GetAll(Persona persona, Aplicacion aplicacion)
		{
			return
				(
				from query in Query.GetRoles(persona, aplicacion)
				orderby query.AmbitoCodigo, query.Clave, query.Nombre
				select query
				).ToList<Rol>();
		}

		public static List<Rol> GetAll(Persona persona, Evaluaciones.Empresa empresa)
		{
			return
				(
				from query in Query.GetRoles(persona, empresa)
				orderby query.AmbitoCodigo, query.Clave, query.Nombre
				select query
				).ToList<Rol>();
		}
    }
}