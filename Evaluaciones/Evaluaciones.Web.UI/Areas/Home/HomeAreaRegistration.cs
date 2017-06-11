using System.Web.Mvc;

namespace Evaluaciones.Web.UI.Areas.Home
{
    public class HomeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Home";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Home_default",
                url: "",
                defaults: new { area = "Home", controller = "Account", action = "Login" }
            );

            context.MapRoute(
                "Frontend_Default",
                "Home/{controller}/{action}",
                new { area = "Home", controller = "Account", action = "Login" }
            );
        }
    }
}