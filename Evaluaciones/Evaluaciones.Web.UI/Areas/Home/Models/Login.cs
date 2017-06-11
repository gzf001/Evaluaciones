using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Areas.Home.Models
{
    public class Login
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el R.U.N.")]
        public string Run
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar su password.")]
        public string Password
        {
            get;
            set;
        }

        [Display(Name = "Recuerdame!")]
        public bool RememberMe
        {
            get;
            set;
        }

        public string ReturnUrl
        {
            get;
            set;
        }
    }
}