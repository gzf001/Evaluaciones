using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Models.Home
{
    public class RecoveryPass
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el R.U.N.")]
        public string RUN
        {
            get;
            set;
        }
    }
}