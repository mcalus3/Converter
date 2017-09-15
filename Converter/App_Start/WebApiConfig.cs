using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using Converter.App_Start;

namespace Converter
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Insert(0, new TextMediaTypeFormatter());

            config.Routes.MapHttpRoute(
                name: "Converter",
                routeTemplate: "api/converter/{type}",
                defaults: new { controller = "converter" }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
