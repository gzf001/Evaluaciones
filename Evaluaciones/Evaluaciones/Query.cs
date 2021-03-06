using System;
using System.Linq;
namespace Evaluaciones
{
    internal static class Query
    {
        #region Ambito
        internal static IQueryable<Ambito> GetAmbitos()
        {
            return
                from ambito in Context.Instancia.Ambitos
                select ambito;
        }
        #endregion

        #region Anio
        internal static IQueryable<Anio> GetAnios()
        {
            return
                from Anio in Context.Instancia.Anios
                select Anio;
        }

        internal static IQueryable<Anio> GetAnios(bool activo)
        {
            return
                from Anio in Context.Instancia.Anios
                where Anio.Activo == activo
                select Anio;
        }
        #endregion

        #region AnioMes
        internal static IQueryable<AnioMes> GetAnioMeses()
        {
            return
                from AnioMes in Context.Instancia.AnioMeses
                select AnioMes;
        }

        internal static IQueryable<AnioMes> GetAnioMeses(Anio Anio)
        {
            return
                from AnioMes in GetAnioMeses()
                where AnioMes.Anio == Anio
                select AnioMes;
        }

        internal static IQueryable<AnioMes> GetAnioMeses(DateTime desde, DateTime hasta)
        {
            return
                from AnioMes in Context.Instancia.AnioMeses
                where (AnioMes.Termino >= desde)
                && (AnioMes.Inicio <= hasta)
                select AnioMes;
        }
        #endregion

        #region AreaGeografica
        internal static IQueryable<AreaGeografica> GetAreaGeograficas()
        {
            return
                from areaGeografica in Context.Instancia.AreaGeograficas
                select areaGeografica;
        }
        #endregion

        #region Calendario
        internal static IQueryable<Calendario> GetCalendarios()
        {
            return
                from calendario in Context.Instancia.Calendarios
                select calendario;
        }

        internal static IQueryable<Calendario> GetCalendarios(Anio Anio)
        {
            return
                from calendario in GetCalendarios()
                where calendario.AnioMes.Anio == Anio
                select calendario;
        }

        internal static IQueryable<Calendario> GetCalendarios(AnioMes AnioMes)
        {
            return
                from calendario in GetCalendarios()
                where calendario.AnioMes == AnioMes
                select calendario;
        }

        internal static IQueryable<Calendario> GetCalendarios(AnioMes AnioMes, bool laboral)
        {
            return
                from calendario in GetCalendarios(AnioMes)
                where calendario.Dia.Laboral == laboral
                select calendario;
        }

        internal static IQueryable<Calendario> GetCalendarios(DateTime inicio, DateTime termino)
        {
            return
                from calendario in GetCalendarios()
                where calendario.Fecha >= inicio
                && calendario.Fecha <= termino
                select calendario;
        }

        internal static IQueryable<Calendario> GetCalendarios(Semana semana)
        {
            return
                from calendario in GetCalendarios()
                where calendario.Semana == semana
                select calendario;
        }
        #endregion

        #region CentroCosto
        internal static IQueryable<CentroCosto> GetCentroCostos()
        {
            return
                from centroCosto in Context.Instancia.CentroCostos
                select centroCosto;
        }

        internal static IQueryable<CentroCosto> GetCentroCostos(Empresa empresa)
        {
            return
                from centroCosto in GetCentroCostos()
                where centroCosto.Empresa == empresa
                select centroCosto;
        }
        #endregion

        #region Ciudad
        internal static IQueryable<Ciudad> GetCiudades()
        {
            return
                from ciudad in Context.Instancia.Ciudades
                select ciudad;
        }

        internal static IQueryable<Ciudad> GetCiudades(Region region)
        {
            return
                from ciudad in GetCiudades()
                where ciudad.Region == region
                select ciudad;
        }
        #endregion

        #region Comuna
        internal static IQueryable<Comuna> GetComunas()
        {
            return
                from comuna in Context.Instancia.Comunas
                select comuna;
        }

        internal static IQueryable<Comuna> GetComunas(Ciudad ciudad)
        {
            return
                from comuna in GetComunas()
                where comuna.Ciudad == ciudad
                select comuna;
        }
        #endregion

        #region Dia
        internal static IQueryable<Dia> GetDias()
        {
            return
                from dia in Context.Instancia.Dias
                select dia;
        }

        internal static IQueryable<Dia> GetDias(bool laboral)
        {
            return
                from dia in GetDias()
                where dia.Laboral == laboral
                select dia;
        }
        #endregion

        #region Empresa
        internal static IQueryable<Empresa> GetEmpresas()
        {
            return
                from empresa in Context.Instancia.Empresas
                select empresa;
        }

        internal static IQueryable<Empresa> GetEmpresas(Evaluaciones.FindType findType, string filter)
        {
            switch (findType)
            {
                case FindType.StartsWith: return from empresa in GetEmpresas() where (empresa.RazonSocial.StartsWith(filter)) select empresa;
                case FindType.Contains: return from empresa in GetEmpresas() where (empresa.RazonSocial.Contains(filter)) select empresa;
                case FindType.EndsWith: return from empresa in GetEmpresas() where (empresa.RazonSocial.EndsWith(filter)) select empresa;
                default: return from empresa in GetEmpresas() where (empresa.RazonSocial == filter) select empresa;
            }
        }
        #endregion

        #region EstadoCivil
        internal static IQueryable<EstadoCivil> GetEstadoCiviles()
        {
            return
                from estadoCivil in Context.Instancia.EstadoCiviles
                select estadoCivil;
        }
        #endregion

        #region Feriado
        internal static IQueryable<Feriado> GetFeriados()
        {
            return
                from feriado in Context.Instancia.Feriados
                select feriado;
        }

        internal static IQueryable<Feriado> GetFeriados(Anio Anio)
        {
            return
                from feriado in GetFeriados()
                where feriado.Calendario.AnioMes.Anio == Anio
                && (feriado.EmpresaId == null)
                && (feriado.CentroCostoId == null)
                select feriado;
        }

        internal static IQueryable<Feriado> GetFeriados(CentroCosto centroCosto, Anio Anio)
        {
            return
                from feriado in GetFeriados()
                where feriado.Calendario.AnioMes.Anio == Anio
                && (feriado.Empresa == null || feriado.Empresa == centroCosto.Empresa)
                && (feriado.CentroCosto == null || feriado.CentroCosto == centroCosto)
                select feriado;
        }
        #endregion

        #region Mes
        internal static IQueryable<Mes> GetMeses()
        {
            return
                from mes in Context.Instancia.Meses
                select mes;
        }

        internal static IQueryable<Mes> GetMeses(DateTime desde, DateTime hasta)
        {
            return
                from AnioMes in GetAnioMeses(desde, hasta)
                orderby AnioMes.Inicio
                select AnioMes.Mes;
        }
        #endregion

        #region Nacionalidad
        internal static IQueryable<Nacionalidad> GetNacionalidades()
        {
            return
                from nacionalidad in Context.Instancia.Nacionalidades
                select nacionalidad;
        }
        #endregion

        #region NivelEducacional
        internal static IQueryable<NivelEducacional> GetNivelEducacionales()
        {
            return
                from nivelEducacional in Context.Instancia.NivelEducacionales
                select nivelEducacional;
        }
        #endregion

        #region Persona
        internal static IQueryable<Persona> GetPersonas()
        {
            return
                from persona in Context.Instancia.Personas
                select persona;
        }

        internal static IQueryable<Persona> GetPersonas(FindType findType, string filter)
        {
            switch (findType)
            {
                case FindType.StartsWith: return from usuario in GetPersonas() where (usuario.Nombre.StartsWith(filter)) select usuario;
                case FindType.Contains: return from usuario in GetPersonas() where (usuario.Nombre.Contains(filter)) select usuario;
                case FindType.EndsWith: return from usuario in GetPersonas() where (usuario.Nombre.EndsWith(filter)) select usuario;
                default: return from usuario in GetPersonas() where (usuario.Nombre == filter) select usuario;
            }
        }

        internal static IQueryable<Persona> GetPersonas(int cuerpo, char digito)
        {
            return
                from persona in GetPersonas()
                where persona.RunCuerpo == cuerpo
                && persona.RunDigito == digito
                select persona;
        }
        #endregion

        #region PuebloIndigena
        internal static IQueryable<PuebloIndigena> GetPuebloIndigenas()
        {
            return
                from puebloIndigena in Context.Instancia.PuebloIndigenas
                select puebloIndigena;
        }
        #endregion

        #region Region
        internal static IQueryable<Region> GetRegiones()
        {
            return
                from region in Context.Instancia.Regiones
                select region;
        }
        #endregion

        #region Secuencia
        internal static IQueryable<Secuencia> GetSecuencias()
        {
            return
                from secuencia in Context.Instancia.Secuencias
                select secuencia;
        }
        #endregion

        #region SecuenciaCentroCosto
        internal static IQueryable<SecuenciaCentroCosto> GetSecuenciaCentroCostos()
        {
            return
                from secuenciaCentroCosto in Context.Instancia.SecuenciaCentroCostos
                select secuenciaCentroCosto;
        }

        internal static IQueryable<SecuenciaCentroCosto> GetSecuenciaCentroCostos(Empresa empresa)
        {
            return
                from secuenciaCentroCostos in GetSecuenciaCentroCostos()
                where secuenciaCentroCostos.Empresa == empresa
                select secuenciaCentroCostos;
        }
        #endregion

        #region SecuenciaEmpresa
        internal static IQueryable<SecuenciaEmpresa> GetSecuenciaEmpresas()
        {
            return
                from secuenciaEmpresa in Context.Instancia.SecuenciaEmpresas
                select secuenciaEmpresa;
        }

        internal static IQueryable<SecuenciaEmpresa> GetSecuenciaEmpresas(Empresa empresa)
        {
            return
                from secuenciaEmpresa in GetSecuenciaEmpresas()
                where secuenciaEmpresa.Empresa == empresa
                select secuenciaEmpresa;
        }
        #endregion

        #region Semana
        internal static IQueryable<Semana> GetSemanas()
        {
            return
                from semana in Context.Instancia.Semanas
                select semana;
        }

        internal static IQueryable<Semana> GetSemanas(AnioMes AnioMes)
        {
            return
                from semana in GetSemanas()
                where semana.AnioMes == AnioMes
                select semana;
        }

        internal static IQueryable<Semana> GetSemanas(DateTime desde, DateTime hasta)
        {
            return
                from semana in GetSemanas()
                where semana.Termino >= desde && semana.Inicio <= hasta
                select semana;
        }
        #endregion

        #region Sexo
        internal static IQueryable<Sexo> GetSexos()
        {
            return
                from sexo in Context.Instancia.Sexos
                select sexo;
        }
        #endregion
    }
}