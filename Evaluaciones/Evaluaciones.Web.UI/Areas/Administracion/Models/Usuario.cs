using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Areas.Administracion.Models
{
    public class Usuario : Evaluaciones.Membresia.Usuario
    {
        public Evaluaciones.FindType FindType
        {
            get;
            set;
        }

        public string Accion
        {
            get;
            set;
        }

        public string Run
        {
            get;
            set;
        }

        public string Nombre
        {
            get;
            set;
        }

        public string UltimoLogin
        {
            get;
            set;
        }

        public string Estado
        {
            get;
            set;
        }

        public string FechaNacimientoString
        {
            get;
            set;
        }

        public class Usuarios
        {
            public List<Usuario> data
            {
                get;
                set;
            }
        }
    }
}