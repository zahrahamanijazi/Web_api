using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Part2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Web API routes
            config.Formatters.Add(new BsonMediaTypeFormatter());
            config.MapHttpAttributeRoutes();

            config.EnableCors();
            //var cors = new EnableCorsAttribute("https://localhost:44306", "*", "*");
            //config.EnableCors(cors); 

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
