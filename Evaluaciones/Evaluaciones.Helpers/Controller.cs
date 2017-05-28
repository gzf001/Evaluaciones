using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluaciones.Helpers
{
    public abstract class Controller : System.Web.Mvc.Controller
    {
        public Evaluaciones.Membresia.Usuario CurrentUsuario
        {
            get
            {
                Evaluaciones.Membresia.Usuario usuario = Evaluaciones.Membresia.Usuario.Get(new Guid(this.User.Identity.Name));

                return usuario;
            }
        }
    }
}