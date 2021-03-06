﻿using Evaluaciones.Web.UI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Evaluaciones.Web.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            this.Application["NumberUsers"] = 0;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            this.Application["NumberUsers"] = ((int)this.Application["NumberUsers"]) + 1;
        }

        void Session_End(object sender, EventArgs e)
        {
            this.Application["NumberUsers"] = ((int)this.Application["NumberUsers"]) - 1;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if ((sender as System.Web.HttpApplication).Context.AllErrors.Select<Exception, string>(x => x.Message).Contains(Evaluaciones.Web.CustomError.SinPermiso_403.ToString()))
            {
                this.Response.Redirect("/Utils/Error", true);
            }
        }
    }
}