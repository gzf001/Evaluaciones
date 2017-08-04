using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Models
{
    public abstract class RecursoCurricular
    {
        public int Numero
        {
            get;
            set;
        }

        public string Nombre
        {
            get;
            set;
        }

        public string Descripcion
        {
            get;
            set;
        }

        public int TipoEducacionCodigo
        {
            get;
            set;
        }

        public int GradoCodigo
        {
            get;
            set;
        }

        public int AnioNumero
        {
            get;
            set;
        }

        public Guid SectorId
        {
            get;
            set;
        }

        public Guid Id
        {
            get;
            set;
        }
    }
}