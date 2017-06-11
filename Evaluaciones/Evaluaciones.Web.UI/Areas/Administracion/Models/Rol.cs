using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Areas.Administracion.Models
{
    public class Rol : Evaluaciones.Membresia.Rol
    {
        public string Accion
        {
            get;
            set;
        }

        public string Estado
        {
            get;
            set;
        }

        public class Roles
        {
            public List<Rol> data
            {
                get;
                set;
            }
        }
    }
}