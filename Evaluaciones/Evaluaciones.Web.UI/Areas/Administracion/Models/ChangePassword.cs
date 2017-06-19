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
        [MinLength(8, ErrorMessage = "La contraseña actual debe tener como mínimo 8 caracteres")]
        [MaxLength(8, ErrorMessage = "La contraseña actual debe tener como máximo 8 caracteres")]
        [Display(Name = "Contraseña Actual:")]
        public string Password
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el nuevo password.")]
        [MinLength(8, ErrorMessage = "La nueva contraseña debe tener como mínimo 8 caracteres")]
        [MaxLength(8, ErrorMessage = "La nueva contraseña debe tener como máximo 8 caracteres")]
        [Display(Name = "Nueva Contraseña:")]
        public string Password1
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe repetir el nuevo password.")]
        [MinLength(8, ErrorMessage = "La confirmación de la nueva contraseña debe tener como mínimo 8 caracteres")]
        [MaxLength(8, ErrorMessage = "La confirmación de la nueva contraseña debe tener como máximo 8 caracteres")]
        [Display(Name = "Confirme Contraseña:")]
        [Compare("Password1", ErrorMessage = "La nueva contraseña y su confirmación son diferentes")]
        public string Password2
        {
            get;
            set;
        }
    }
}