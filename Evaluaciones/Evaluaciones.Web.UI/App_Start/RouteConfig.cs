﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Evaluaciones.Web.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Login",
               url: "Home/Account/Login",
               defaults: new { area = "Home", controller = "Account", action = "Login" }
            );

            #region Utilidades

            routes.MapRoute(
                name: "Utils",
                url: "{controller}/{action}",
                defaults: new { controller = "Utils", action = "GenerateId" }
            );

            #endregion

            #region Ciudades y Comunas

            routes.MapRoute(
                name: "Ciudades",
                url: "Evaluaciones/Ciudades/{regionCodigo}",
                defaults: new { controller = "Evaluaciones", action = "Ciudades", regionCodigo = "" }
            );

            routes.MapRoute(
                name: "Comunas",
                url: "Evaluaciones/Comunas/{regionCodigo}/{ciudadCodigo}",
                defaults: new { controller = "Evaluaciones", action = "Comunas", regionCodigo = "", ciudadCodigo = "" }
            );

            #endregion

            #region Centros de costo

            routes.MapRoute(
                name: "CentrosCosto",
                url: "Evaluaciones/CentrosCosto/{empresaId}",
                defaults: new { controller = "Evaluaciones", action = "CentrosCosto", empresaId = "" }
            );

            #endregion

            #region Grados

            routes.MapRoute(
                name: "Grados",
                url: "Educacion/Grados/{tipoEducacion}",
                defaults: new { controller = "Educacion", action = "Grados", tipoEducacion = "" }
            );

            #endregion

            #region Sectores

            routes.MapRoute(
                name: "Sectores",
                url: "Educacion/Sectores/{tipoEducacion}/{grado}",
                defaults: new { controller = "Educacion", action = "Sectores", tipoEducacion = "", grado = "" }
            );

            #endregion

            #region BasesCurriculares

            routes.MapRoute(
                name: "Ejes",
                url: "BaseCurricular/Ejes/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{unidadId}",
                defaults: new { controller = "BaseCurricular", action = "Ejes", tipoEducacionCodigo = "", gradoCodigo = "", sectorId = "", unidadId = "" }
            );

            routes.MapRoute(
                name: "ObjetivosAprendizaje",
                url: "BaseCurricular/ObjetivosAprendizaje/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{unidadId}/{ejeId}",
                defaults: new { controller = "BaseCurricular", action = "ObjetivosAprendizaje", tipoEducacionCodigo = "", gradoCodigo = "", sectorId = "", unidadId = "", ejeId = "" }
            );

            routes.MapRoute(
                name: "GetObjetivosAprendizaje",
                url: "BaseCurricular/GetObjetivosAprendizaje/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}/{unidadId}/{ejeId}",
                defaults: new { controller = "BaseCurricular", action = "GetObjetivosAprendizaje", tipoEducacionCodigo = "", gradoCodigo = "", sectorId = "", unidadId = "", ejeId = "" }
            );

            routes.MapRoute(
                name: "Unidades",
                url: "BaseCurricular/Unidades/{tipoEducacionCodigo}/{gradoCodigo}/{sectorId}",
                defaults: new { controller = "BaseCurricular", action = "Unidades", tipoEducacionCodigo = "", gradoCodigo = "", sectorId = "" }
            );

            #endregion
        }
    }
}