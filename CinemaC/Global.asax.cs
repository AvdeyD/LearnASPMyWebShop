using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using CinemaC.Interfaces;
using CinemaC.Services;
using LightInject;

namespace CinemaC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new ServiceContainer();
            container.RegisterControllers();
            container.EnableAnnotatedConstructorInjection();
            InitAutomapperProfiles(container);
            container.Register<HttpContextBase>(factory => new HttpContextWrapper(HttpContext.Current),
                new PerRequestLifeTime());
            container.Register<ITicketService, EntityTicketsServise>(new PerRequestLifeTime());
            container.EnableMvc();
        }


        private static void InitAutomapperProfiles(ServiceContainer conteiner)
        {
            var assembly = Assembly.GetCallingAssembly();
            var definedTypes = assembly.DefinedTypes;
            var profiles = definedTypes.Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t) && !t.IsAbstract)
                .ToArray();

            void ConfigAction(IMapperConfigurationExpression cfg)
            {
                foreach (var profile in profiles.Select(x=>x.AsType()))
                {
                    cfg.AddProfile(profile);
                }
            }
            Mapper.Initialize(ConfigAction);
            var config = (MapperConfiguration) Mapper.Configuration;
            config.AssertConfigurationIsValid();
            conteiner.Register(sp => config.CreateMapper(), new PerRequestLifeTime());
        }
    }
}
