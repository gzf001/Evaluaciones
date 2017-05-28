using System.Collections.Generic;
using System.Linq;

namespace Evaluaciones.Membresia
{
    public class Empresa
    {
        public static bool Exists(Evaluaciones.Empresa empresa, Evaluaciones.Membresia.Rol rol)
        {
            List<Evaluaciones.Empresa> empresas = Query.GetEmpresas(rol).ToList<Evaluaciones.Empresa>();

            if (empresas.Count == 0)
            {
                return true;
            }
            else if (empresas.Count == 1)
            {
                return empresas[0].Equals(empresa);
            }
            else 
            {
                return false;
            }
        }

        public static List<Evaluaciones.Empresa> GetAll(Evaluaciones.Persona persona, Aplicacion aplicacion)
        {
            return
                (
                from query in Query.GetEmpresas(persona, aplicacion)
                orderby query.RazonSocial
                select query
                ).ToList<Evaluaciones.Empresa>();
        }

        public static List<Evaluaciones.Empresa> GetAll(Rol rol)
        {
            return
                (
                from query in Query.GetEmpresas(rol)
                orderby query.RazonSocial
                select query
                ).ToList<Evaluaciones.Empresa>();
        }
    }
}
