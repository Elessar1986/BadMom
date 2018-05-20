using BadMom.BLL.Infrastrutcure;
using BadMom.Util;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BadMom
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //NinjectModule badMomModule = new BadMomModule();
            //NinjectModule serviceModule = new ServiceModule("data source = UAPSPC313-22\\SQLEXPRESS; initial catalog = BadMomResource; integrated security = True; MultipleActiveResultSets = True; App = EntityFramework");
            //var kernel = new StandardKernel(badMomModule, serviceModule);
            //DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            DependencyResolver.SetResolver(new NinjectDependencyResolver());


            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            
        }
    }
}
