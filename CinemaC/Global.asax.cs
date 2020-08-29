using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CinemaC.Interfaces;
using CinemaC.Services;
using LightInject;

namespace CinemaC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new ServiceContainer();
            container.RegisterControllers();
            container.EnableAnnotatedConstructorInjection();
            container.Register<ITicketService, JsonTicketService>(new PerRequestLifeTime());
            container.EnableMvc();
        }
    }
}
