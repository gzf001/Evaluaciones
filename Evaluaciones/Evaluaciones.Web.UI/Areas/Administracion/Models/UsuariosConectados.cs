using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Areas.Administracion.Models
{
    public class UsuariosConectados
    {
        [Display(Name = "Número de usuarios conectados en este momento:")]
        public int NumeroUsuarios
        {
            get;
            set;
        }
    }
}