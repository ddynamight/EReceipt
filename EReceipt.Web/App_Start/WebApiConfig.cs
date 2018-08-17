using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace EReceipt.Web
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
                routeTemplate: "api/{controller}/{action}/{tag}",
                defaults: new { controller = "home", action = "index", tag = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //    name: "CompaniesApi",
            //    routeTemplate: "api/companies/{action}/{tag}",
            //    defaults: new { action = "getcompanies", tag = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "InvoicesApi",
            //    routeTemplate: "api/invoices/{action}/{tag}",
            //    defaults: new { action = "getinvoices", tag = RouteParameter.Optional }
            //);

            


            //var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            var json = config.Formatters.JsonFormatter;
            json.UseDataContractJsonSerializer = true;
            //json.Indent = true;

            //var jsonFormater = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            //jsonFormater.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

            //var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //json.UseDataContractJsonSerializer = true;
        }
    }
}
