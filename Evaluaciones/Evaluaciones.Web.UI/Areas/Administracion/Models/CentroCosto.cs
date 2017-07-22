using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Areas.Administracion.Models
{
    public class CentroCosto : Evaluaciones.CentroCosto
    {
        public string Accion
        {
            get;
            set;
        }

        public string AreaGeograficaNombre
        {
            get;
            set;
        }

        public string FechaAutorizacionString
        {
            get;
            set;
        }

        public new class CentrosCosto
        {
            public List<CentroCosto> data
            {
                get;
                set;
            }
        }
    }
}