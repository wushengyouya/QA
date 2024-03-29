﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QA
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

       
            routes.MapRoute(
                name: "home",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Consult", action = "Index", id = UrlParameter.Optional }

            );
        }
    }
}
