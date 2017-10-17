using BasicDDD.BasicApplication.App_Start;
using BasicDDD.BasicApplication.AutoMapper;
using Ninject;
using Ninject.Modules;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;

namespace BasicDDD.BasicApplication
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AutoMapperConfig.RegisterMappings();
            Mapper.Initialize(x => x.AddProfile<AutoMapperConfig>());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            




            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            var ninjectResolver = new NinjectDependencyResolver(kernel);

            DependencyResolver.SetResolver(ninjectResolver); // MVC
            GlobalConfiguration.Configuration.DependencyResolver = ninjectResolver; // Web API
        }
    }

}
