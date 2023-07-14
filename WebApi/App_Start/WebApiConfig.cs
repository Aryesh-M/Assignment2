using System;
using System.Linq;
using System.Web.Http;


namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Web API configuration and services
            //config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Clear();
            //config.Formatters.Add(new System.Net.Http.Formatting.FormDataMediaTypeFormatter());

            //Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
