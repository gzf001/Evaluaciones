using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Areas.Administracion.Models
{
    public class ChangePassword
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la contraseña actual.")]
        [Display(Name = "Contraseña Actual:")]
        public string Password
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el nuevo password.")]
        [Display(Name = "Nueva Contraseña:")]
        public string Password1
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe repetir el nuevo password.")]
        [Display(Name = "Confirme Contraseña:")]
        [Compare("Password1", ErrorMessage = "La nueva contraseña y su confirmación son diferentes")]
        public string Password2
        {
            get;
            set;
        }
    }
}