using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evaluaciones.Web.UI.Models
{
    public class ObjetivoAprendizaje
    {
        public Guid EjeId
        {
            get;
            set;
        }

        public int Numero
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

        public class ObjetivosAprendizaje
        {
            public List<ObjetivoAprendizaje> data
            {
                get;
                set;
            }
        }
    }
}