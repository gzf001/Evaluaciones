using System;
using System.Linq;
namespace Evaluaciones.Evaluacion
{
    internal static class Query
    {
        #region Dificultad
        internal static IQueryable<Dificultad> GetDificultades()
        {
            return
                from dificultad in Context.Instancia.Dificultades
                select dificultad;
        }
        #endregion

        #region EmpresaPregunta
        internal static IQueryable<EmpresaPregunta> GetEmpresaPreguntas()
        {
            return
                from empresaPregunta in Context.Instancia.EmpresaPreguntas
                select empresaPregunta;
        }
        #endregion

        #region Evaluacion
        internal static IQueryable<Evaluacion> GetEvaluaciones()
        {
            return
                from evaluacion in Context.Instancia.Evaluaciones
                select evaluacion;
        }
        #endregion

        #region EvaluacionPrueba
        internal static IQueryable<EvaluacionPrueba> GetEvaluacionPruebas()
        {
            return
                from evaluacionPrueba in Context.Instancia.EvaluacionPruebas
                select evaluacionPrueba;
        }
        #endregion

        #region Habilidad
        internal static IQueryable<Habilidad> GetHabilidades()
        {
            return
                from habilidad in Context.Instancia.Habilidades
                select habilidad;
        }
        #endregion

        #region Pregunta
        internal static IQueryable<Pregunta> GetPreguntas()
        {
            return
                from pregunta in Context.Instancia.Preguntas
                select pregunta;
        }
        #endregion

        #region PreguntaAlternativa
        internal static IQueryable<PreguntaAlternativa> GetPreguntaAlternativas()
        {
            return
                from preguntaAlternativa in Context.Instancia.PreguntaAlternativas
                select preguntaAlternativa;
        }
        #endregion

        #region PreguntaBaseCurricular
        internal static IQueryable<PreguntaBaseCurricular> GetPreguntaBaseCurriculares()
        {
            return
                from preguntaBaseCurricular in Context.Instancia.PreguntaBaseCurriculares
                select preguntaBaseCurricular;
        }
        #endregion

        #region PreguntaRecursoCurricular
        internal static IQueryable<PreguntaRecursoCurricular> GetPreguntaRecursoCurriculares()
        {
            return
                from preguntaRecursoCurricular in Context.Instancia.PreguntaRecursoCurriculares
                select preguntaRecursoCurricular;
        }
        #endregion

        #region PreguntaSeleccionMultiple
        internal static IQueryable<PreguntaSeleccionMultiple> GetPreguntaSeleccionMultiples()
        {
            return
                from preguntaSeleccionMultiple in Context.Instancia.PreguntaSeleccionMultiples
                select preguntaSeleccionMultiple;
        }
        #endregion

        #region PreguntaSeleccionMultipleCorrecta
        internal static IQueryable<PreguntaSeleccionMultipleCorrecta> GetPreguntaSeleccionMultipleCorrectas()
        {
            return
                from preguntaSeleccionMultipleCorrecta in Context.Instancia.PreguntaSeleccionMultipleCorrectas
                select preguntaSeleccionMultipleCorrecta;
        }
        #endregion

        #region PreguntaVerdaderoFalso
        internal static IQueryable<PreguntaVerdaderoFalso> GetPreguntaVerdaderoFalsos()
        {
            return
                from preguntaVerdaderoFalso in Context.Instancia.PreguntaVerdaderoFalsos
                select preguntaVerdaderoFalso;
        }
        #endregion

        #region Prueba
        internal static IQueryable<Prueba> GetPruebas()
        {
            return
                from prueba in Context.Instancia.Pruebas
                select prueba;
        }
        #endregion

        #region PruebaPregunta
        internal static IQueryable<PruebaPregunta> GetPruebaPreguntas()
        {
            return
                from pruebaPregunta in Context.Instancia.PruebaPreguntas
                select pruebaPregunta;
        }
        #endregion

        #region RecursosPme
        internal static IQueryable<RecursosPme> GetRecursosPmes()
        {
            return
                from recursosPme in Context.Instancia.RecursosPmes
                select recursosPme;
        }
        #endregion

        #region Referencia
        internal static IQueryable<Referencia> GetReferencias()
        {
            return
                from referencia in Context.Instancia.Referencias
                select referencia;
        }

        internal static IQueryable<Referencia> GetReferencias(Evaluaciones.Educacion.Grado grado, Guid sectorId)
        {
            return
                from referencia in GetReferencias()
                where referencia.Grado == grado
                   && referencia.SectorId == sectorId
                select referencia;
        }
        #endregion

        #region TipoEvaluacion
        internal static IQueryable<TipoEvaluacion> GetTipoEvaluaciones()
        {
            return
                from tipoEvaluacion in Context.Instancia.TipoEvaluaciones
                select tipoEvaluacion;
        }
        #endregion

        #region TipoReactivo
        internal static IQueryable<TipoReactivo> GetTipoReactivos()
        {
            return
                from tipoReactivo in Context.Instancia.TipoReactivos
                select tipoReactivo;
        }
        #endregion
    }
}