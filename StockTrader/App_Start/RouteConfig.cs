using System.Web.Mvc;
using System.Web.Routing;

namespace StockTrader
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
     "Default", // Route name
     "{controller}/{action}/{id}", // URL with parameters
     new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
     new string[] { "MyCompany.MyProject.WebMvc.Controllers" }
);
        }
    }
}