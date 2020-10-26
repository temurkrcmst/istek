using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace istek
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // we dont need jQueryFormEncoder
            {
                var remove = config.Formatters.FirstOrDefault<MediaTypeFormatter>((i) => typeof(System.Web.Http.ModelBinding.JQueryMvcFormUrlEncodedFormatter) == i.GetType());
                if (remove != null) config.Formatters.Remove(remove);
            }

            // V1 Api Router
            config.Routes.MapHttpRoute("Movie Default", "api/v1/{action}", new { controller = "Home" });
            config.Routes.MapHttpRoute("Movie", "api/v1/movie/{action}", new { controller = "Movie" });




            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

         

        }
    }
}
