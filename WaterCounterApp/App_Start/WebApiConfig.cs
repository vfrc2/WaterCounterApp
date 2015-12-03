using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace WaterCounterApp
{
    public static class WebApiConfig
    {

 

        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            try {
                config.Formatters.Remove(config.Formatters.XmlFormatter); }
            catch { }

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = 
                new CamelCasePropertyNamesContractResolver();

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
}
}
