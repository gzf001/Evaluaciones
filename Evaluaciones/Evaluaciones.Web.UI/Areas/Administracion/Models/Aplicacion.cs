using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluaciones.Web.UI.Areas.Administracion.Models
{
    public class Aplicacion : Evaluaciones.Membresia.Aplicacion
    {
        public Aplicacion() : base()
        {
            this.SelectedPerfil = new List<int>();

            this.Perfiles = new List<SelectListItem>();
        }

        public string Accion
        {
            get;
            set;
        }

        public List<int> SelectedPerfil
        {
            get;
            set;
        }

        public List<SelectListItem> Perfiles
        {
            get;
            set;
        }

        public new class Aplicaciones
        {
            public List<Aplicacion> data
            {
                get;
                set;
            }
        }
    }
}