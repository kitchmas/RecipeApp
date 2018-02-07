using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RecipeApp.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(null, "",
            //    new { controller = "Home", action = "Index" }
            //    );

            //routes.MapRoute(null, "Recipe/Edit/{id}",
            //    new { controller = "Recipe", action = "edit", id = "id"});

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );



            routes.MapRoute(null, "",
                new { controller = "Home", action = "Index" }
                );

        }
    }
}
