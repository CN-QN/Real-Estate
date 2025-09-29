using System.Web.Mvc;
using System.Web.Routing;

namespace RealEstate
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name :"Properties",
                url : "{controller}/{action}/{id}",
                defaults : new {controller = "Property", action = "Index",id = UrlParameter.Optional}
                );
            
        }
    }
}
