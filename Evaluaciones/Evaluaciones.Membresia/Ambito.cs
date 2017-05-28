using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Membresia
{
	public static class Ambito
	{
		public static bool IsAplicacion(Persona persona)
		{
			return Query.GetRolPersonas(persona).Any<RolPersona>();
		}

		public static List<Evaluaciones.Ambito> GetAll(Aplicacion aplicacion, Evaluaciones.Empresa empresa, Evaluaciones.CentroCosto centroCosto, Persona persona)
		{
			List<Evaluaciones.Ambito> list = new List<Evaluaciones.Ambito>();

			if (Query.GetRolPersonas(persona, aplicacion).Any<RolPersona>())
			{
				list.Add(Evaluaciones.Ambito.Aplicacion);

                if (empresa != null)
                {
                    list.Add(Evaluaciones.Ambito.Empresa);
                }

                if (centroCosto != null)
                {
                    list.Add(Evaluaciones.Ambito.CentroCosto);
                }
			}
			else if (Query.GetRolPersonaEmpresas(persona, aplicacion, empresa).Any<RolPersonaEmpresa>())
			{
				list.Add(Evaluaciones.Ambito.Empresa);

                if (centroCosto != null)
                {
                    list.Add(Evaluaciones.Ambito.CentroCosto);
                }
			}
			else if (Query.GetRolPersonaCentroCostos(persona, aplicacion, centroCosto).Any<RolPersonaCentroCosto>())
			{
				list.Add(Evaluaciones.Ambito.CentroCosto);
			}

			return list;
		}
	}
}
