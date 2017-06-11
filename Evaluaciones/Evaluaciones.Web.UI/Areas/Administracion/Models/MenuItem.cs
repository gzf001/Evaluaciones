using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Areas.Administracion.Models
{
    public class MenuItem : Evaluaciones.Membresia.MenuItem
    {
        [Display(Name = "Aplicación:")]
        public string NombreAplicacion
        {
            get;
            set;
        }
    }
}