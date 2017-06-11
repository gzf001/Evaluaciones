using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Models
{
    public class PartialHeader
    {
        public PartialHeader(Guid id)
        {
            Evaluaciones.Membresia.Usuario usuario = Evaluaciones.Membresia.Usuario.Get(id);

            this.UserName = string.Format("{0} {1} {2}", usuario.Persona.Nombres, usuario.Persona.ApellidoPaterno, string.IsNullOrEmpty(usuario.Persona.ApellidoMaterno) ? usuario.Persona.ApellidoMaterno : string.Empty);
        }

        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        [Display(Name = "Unidad:")]
        public string Unidad
        {
            get
            {
                return "Unidad en duro";
            }
        }

        [Display(Name = "Nivel:")]
        public string Nivel
        {
            get
            {
                return "Nivel en duro";
            }
        }
    }
}