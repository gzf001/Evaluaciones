using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Membresia
{
    public partial class Accion
    {
        public static Accion Acceder
        {
            get
            {
                return Evaluaciones.Membresia.Accion.Get(1);
            }
        }

        public static Accion Agregar
        {
            get
            {
                return Evaluaciones.Membresia.Accion.Get(2);
            }
        }

        public static Accion Editar
        {
            get
            {
                return Evaluaciones.Membresia.Accion.Get(3);
            }
        }

        public static Accion Eliminar
        {
            get
            {
                return Evaluaciones.Membresia.Accion.Get(4);
            }
        }

        public static Accion Aceptar
        {
            get
            {
                return Evaluaciones.Membresia.Accion.Get(5);
            }
        }

        public static Accion Get(Int32 codigo)
        {
            return Query.GetAcciones().SingleOrDefault<Accion>(x => x.Codigo == codigo);
        }

        public static List<Accion> GetAll()
        {
            return
                (
                from query in Query.GetAcciones()
                select query
                ).ToList<Accion>();
        }

        public static List<Accion> GetAll(MenuItem menuItem)
        {
            return
                (
                from query in Query.GetAcciones(menuItem)
                orderby query.Codigo
                select query
                ).ToList<Accion>();
        }
    }
}