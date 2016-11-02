using System.Web.Mvc;
using System.Web.Routing;

namespace AutoCadet
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Site",
                "{action}/{prettyUrl}",
                new { controller = "Home", action = "Index", prettyUrl = UrlParameter.Optional }
                );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{prettyUrl}",
                new {controller = "Home", action = "Index", prettyUrl = UrlParameter.Optional}
                );
        }
    }
}
