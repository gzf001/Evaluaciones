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
        }
    }
}