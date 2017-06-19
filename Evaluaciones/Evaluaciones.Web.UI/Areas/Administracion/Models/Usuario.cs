using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public ChangePassword ChangePass
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

        public class ChangePassword
        {
            [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el nuevo password.")]
            [MinLength(8, ErrorMessage = "La contraseña número 1 debe tener como mínimo 8 caracteres")]
            [MaxLength(8, ErrorMessage = "La contraseña número 1 debe tener como máximo 8 caracteres")]
            [Display(Name = "Contraseña:")]
            public string Password1
            {
                get;
                set;
            }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Debe repetir el nuevo password.")]
            [MinLength(8, ErrorMessage = "La contraseña número 2 debe tener como mínimo 8 caracteres")]
            [MaxLength(8, ErrorMessage = "La contraseña número 2 debe tener como máximo 8 caracteres")]
            [Display(Name = "Confirme Contraseña:")]
            [Compare("Password1", ErrorMessage = "La nueva contraseña y su confirmación son diferentes")]
            public string Password2
            {
                get;
                set;
            }
        }
    }
}