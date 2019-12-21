using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PlanIt
{
    public class RouteConfig
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            "Clubs",                                           // Route name
            "WelcomeClub!",                            // URL with parameters
            new { controller = "Clubs", action = "Index" }  // Parameter defaults
            );

            routes.MapRoute(
            "Student",                                           // Route name
            "HelloStudent!",                            // URL with parameters
            new { controller = "Students", action = "Index" }  // Parameter defaults
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
        }
        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }

    }
}