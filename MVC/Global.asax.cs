using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using MVC.App_Start;
using MVC.Models;
using Project.Model;


namespace MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           
          
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<VehicleMake, VehicleMakeViewModel>();
                cfg.CreateMap<VehicleMakeViewModel, VehicleMake>().
                ForMember(x => x.VehicleModel, opt => opt.Ignore());
                cfg.CreateMap<VehicleModel, VehicleModelViewModel>().
                ForMember(x => x.MakeId, opt => opt.MapFrom(source => source.VehicleMake.Id));
                cfg.CreateMap<VehicleModelViewModel, VehicleModel>().
                ForMember(x => x.VehicleMake, opts => opts.Ignore());

            });
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

        }
    }
}
