﻿using System;
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

            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = 
                Newtonsoft.Json.PreserveReferencesHandling.Objects;

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = 
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "HomeApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "CounterApi",
                routeTemplate: "api/homes/{homeid}/counters/{id}",
                defaults: new { homeid = "-1", counterId = RouteParameter.Optional }
            );
        }
}
}
