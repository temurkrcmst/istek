using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace istek
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Routes.MapHttpRoute(
            name: "movieApi",
            routeTemplate: "api/Movie/{action}",
            defaults: new { Controllers = "Movie" });

            config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/Values/{action}",
            defaults: new { id = RouteParameter.Optional, Controllers = "Values" });

            // Web API yolları
            config.MapHttpAttributeRoutes();

        }
    }
}
