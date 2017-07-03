using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Areas.Administracion.Models
{
    public class Empresa : Evaluaciones.Empresa
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

        public string Estado
        {
            get;
            set;
        }

        public new class Empresas
        {
            public List<Empresa> data
            {
                get;
                set;
            }
        }
    }
}