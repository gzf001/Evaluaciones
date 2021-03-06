using System;
using System.Collections.Generic;
using System.Linq;
namespace Evaluaciones.Membresia
{
    public partial class Perfil
    {
        public static Perfil PerfilAplicacion
        {
            get
            {
                return Evaluaciones.Membresia.Perfil.Get(1);
            }
        }

        public static Perfil Sostenedor
        {
            get
            {
                return Evaluaciones.Membresia.Perfil.Get(2);
            }
        }

        public static Perfil Establecimiento
        {
            get
            {
                return Evaluaciones.Membresia.Perfil.Get(3);
            }
        }

        public static Perfil PerfilAnio
        {
            get
            {
                return Evaluaciones.Membresia.Perfil.Get(4);
            }
        }

        public static bool Exists(Aplicacion aplicacion, Perfil perfil)
        {
            return Query.GetAplicacionPerfiles().Any<AplicacionPerfil>(x => x.Aplicacion == aplicacion && x.Perfil == perfil);
        }

        public static bool RequireProfile(Aplicacion aplicacion, PerfilType perfilType)
        {
            if (aplicacion == null) return false;

            Perfil perfil = Perfil.Get(perfilType);

            return AplicacionPerfil.Exists(aplicacion, perfil);
        }

        public static Perfil Get(int codigo)
        {
            return Query.GetPerfiles().SingleOrDefault<Perfil>(x => x.Codigo == codigo);
        }

        public static Perfil Get(PerfilType perfilType)
        {
            switch (perfilType)
            {
                case PerfilType.Aplicacion: return Evaluaciones.Membresia.Perfil.PerfilAplicacion;
                case PerfilType.Sostenedor: return Evaluaciones.Membresia.Perfil.Sostenedor;
                case PerfilType.Establecimiento: return Evaluaciones.Membresia.Perfil.Establecimiento;
                case PerfilType.Anio: return Evaluaciones.Membresia.Perfil.PerfilAnio;
                default: return null;
            }
        }

        public static List<Perfil> GetAll()
        {
            return
                (
                from query in Query.GetPerfiles()
                orderby query.Nombre
                select query
                ).ToList<Perfil>();
        }

        public static List<Perfil> GetAll(Aplicacion aplicacion)
        {
            return
                (
                from query in Query.GetPerfiles(aplicacion)
                orderby query.Nombre
                select query
                ).ToList<Perfil>();
        }

        #region Aplicacion

        public static Aplicacion GetAplicacion(Usuario usuario)
        {
            Perfil perfil = Get(PerfilType.Aplicacion);

            PerfilUsuario perfilUsuario = PerfilUsuario.Get(perfil, usuario);

            if (perfilUsuario == null)
            {
                return null;
            }
            else
            {
                return Aplicacion.Get(new Guid(perfilUsuario.Valor));
            }
        }

        public static void SetAplicacion(Usuario usuario, Aplicacion aplicacion)
        {
            Perfil perfil = Get(PerfilType.Aplicacion);

            using (Context context = new Context())
            {
                PerfilUsuario perfilUsuario = PerfilUsuario.Get(perfil, usuario);

                if (aplicacion == null)
                {
                    if (perfilUsuario != null) perfilUsuario.Delete(context);
                }
                else
                {
                    perfilUsuario = new PerfilUsuario
                    {
                        PerfilCodigo = perfil.Codigo,
                        UsuarioId = usuario.Id,
                        Valor = aplicacion.Id.ToString()
                    };

                    perfilUsuario.Save(context);
                }

                context.SubmitChanges();
            }
        }

        #endregion

        #region Sostenedor

        public static Evaluaciones.Empresa GetEmpresa(Usuario usuario)
        {
            Perfil perfil = Get(PerfilType.Sostenedor);

            PerfilUsuario perfilUsuario = PerfilUsuario.Get(perfil, usuario);

            if (perfilUsuario == null)
            {
                return null;
            }
            else
            {
                Evaluaciones.Empresa empresa = Evaluaciones.Empresa.Get(new Guid(perfilUsuario.Valor));

                if (empresa.Bloqueada)
                {
                    throw new Exception("La empresa esta bloqueada");
                }

                return empresa;
            }
        }

        public static void SetEmpresa(Usuario usuario, Evaluaciones.Empresa empresa)
        {
            Perfil perfil = Get(PerfilType.Sostenedor);

            using (Context context = new Context())
            {
                PerfilUsuario perfilUsuario = PerfilUsuario.Get(perfil, usuario);

                if (empresa == null)
                {
                    if (perfilUsuario != null)
                    {
                        perfilUsuario.Delete(context);
                    }
                }
                else
                {
                    perfilUsuario = new PerfilUsuario
                    {
                        PerfilCodigo = perfil.Codigo,
                        UsuarioId = usuario.Id,
                        Valor = empresa.Id.ToString()
                    };

                    perfilUsuario.Save(context);
                }

                context.SubmitChanges();
            }
        }

        #endregion

        #region Establecimiento

        public static Evaluaciones.CentroCosto GetCentroCosto(Usuario usuario)
        {
            try
            {
                Perfil perfil = Get(PerfilType.Establecimiento);

                PerfilUsuario perfilUsuario = PerfilUsuario.Get(perfil, usuario);

                if (perfilUsuario == null)
                {
                    return null;
                }
                else
                {
                    return Evaluaciones.CentroCosto.Get(GetEmpresa(usuario).Id, new Guid(perfilUsuario.Valor));
                }
            }
            catch
            {
            }

            return null;
        }

        public static void SetCentroCosto(Usuario usuario, Evaluaciones.CentroCosto centroCosto)
        {
            Perfil perfil = Get(PerfilType.Establecimiento);

            using (Context context = new Context())
            {
                PerfilUsuario perfilUsuario = PerfilUsuario.Get(perfil, usuario);

                if (centroCosto == null)
                {
                    if (perfilUsuario != null) perfilUsuario.Delete(context);
                }
                else
                {
                    perfilUsuario = new PerfilUsuario
                    {
                        PerfilCodigo = perfil.Codigo,
                        UsuarioId = usuario.Id,
                        Valor = centroCosto.Id.ToString()
                    };

                    perfilUsuario.Save(context);
                }

                context.SubmitChanges();
            }
        }

        #endregion

        #region Año

        public static Anio GetAnio(Usuario usuario)
        {
            PerfilUsuario perfilUsuario = PerfilUsuario.Get(Perfil.PerfilAnio, usuario);

            if (perfilUsuario == null)
            {
                return null;
            }
            else
            {
                return Evaluaciones.Anio.Get(int.Parse(perfilUsuario.Valor));
            }
        }

        public static void SetAnio(Usuario usuario, Anio Anio)
        {
            Perfil perfil = Get(PerfilType.Anio);

            using (Context context = new Context())
            {
                PerfilUsuario perfilUsuario = PerfilUsuario.Get(perfil, usuario);

                if (Anio == null)
                {
                    if (perfilUsuario != null) perfilUsuario.Delete(context);
                }
                else
                {
                    perfilUsuario = new PerfilUsuario
                    {
                        PerfilCodigo = perfil.Codigo,
                        UsuarioId = usuario.Id,
                        Valor = Anio.Numero.ToString()
                    };

                    perfilUsuario.Save(context);
                }

                context.SubmitChanges();
            }
        }

        #endregion
    }
}