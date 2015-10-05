using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BankingService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "bank/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
