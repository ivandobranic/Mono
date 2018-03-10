using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MVC.App_Start;
using Ninject;

namespace MVC
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            config.Formatters.XmlFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data"));
            config.EnableCors();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{model}/{id}",
                defaults: new {id = RouteParameter.Optional, model = RouteParameter.Optional }
            );
          
        }
        
    }
}
