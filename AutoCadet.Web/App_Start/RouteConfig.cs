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
                "Default",
                "{action}/{prettyUrl}",
                new { controller = "Home", action = "Index", prettyUrl = UrlParameter.Optional }
                );
        }
    }
}
