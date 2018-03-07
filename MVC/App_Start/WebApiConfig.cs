﻿using System;
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
            config.MapHttpAttributeRoutes();
            config.EnableCors();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional }
            );
          
        }
        
    }
}