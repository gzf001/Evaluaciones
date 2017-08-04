using System.Web.Mvc;

namespace Evaluaciones.Web.UI.Areas.BancoPregunta
{
    public class BancoPreguntaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BancoPregunta";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BancoPregunta_default",
                "BancoPregunta/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Referencias",
                "BancoPregunta/Pregunta/Referencias/{tipoEducacion}/{grado}/{sector}",
                defaults: new { area = "BancoPregunta", controller = "Pregunta", action = "Referencias", tipoEducacion = "", grado = "", sector = "" }
            );

            context.MapRoute(
                "GetReferencia",
                "BancoPregunta/Pregunta/GetReferencia/{id}",
                defaults: new { area = "BancoPregunta", controller = "Pregunta", action = "GetReferencia", id = "" }
            );
        }
    }
}