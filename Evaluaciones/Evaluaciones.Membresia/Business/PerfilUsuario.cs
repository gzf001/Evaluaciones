using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Membresia
{
    public partial class PerfilUsuario
    {
        public static PerfilUsuario Get(int perfilCodigo, Guid usuarioId)
		{
			return Query.GetPerfilUsuarios().SingleOrDefault<PerfilUsuario>(x => x.PerfilCodigo == perfilCodigo && x.UsuarioId == usuarioId);
		}

		public static PerfilUsuario Get(Perfil perfil, Usuario usuario)
		{
			return Query.GetPerfilUsuarios().SingleOrDefault<PerfilUsuario>(x => x.Perfil == perfil && x.Usuario == usuario);
		}

		public static List<PerfilUsuario> GetAll()
		{
			return
				(
				from query in Query.GetPerfilUsuarios()
				select query
				).ToList<PerfilUsuario>();
		}
	}
}