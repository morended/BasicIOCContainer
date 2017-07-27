using BasicIocContainer;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MoviesApp;

namespace MoviesApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IContainer container = new BasicContainer();
            ControllerBuilder.Current.SetControllerFactory(new BasicIocControllerFactory(container));
        }
    }
}
