using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using MVC.App_Start;
using MVC.Models;
using Project.DAL.Entities;
using Project.Model;
using Project.Model.Common;

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
              
                cfg.CreateMap<IVehicleMake, VehicleMakeViewModel>();
                cfg.CreateMap<VehicleMake, VehicleMakeViewModel>();
                cfg.CreateMap<VehicleMakeViewModel, IVehicleMake>().
                ForMember(x => x.VehicleModel, opt => opt.Ignore());
                cfg.CreateMap<VehicleMakeViewModel, VehicleMake>().
                 ForMember(x => x.VehicleModel, opt => opt.Ignore());
                cfg.CreateMap<IVehicleModel, VehicleModelViewModel>().
                ForMember(x => x.MakeId, opt => opt.MapFrom(source => source.VehicleMake.Id));
                cfg.CreateMap<VehicleModelViewModel, IVehicleModel>().
                ForMember(x => x.VehicleMake, opts => opts.Ignore());

              
                cfg.CreateMap<VehicleMakeEntity, IVehicleMake>().
                ForMember(x => x.VehicleModel, opt => opt.Ignore());
                cfg.CreateMap<VehicleMakeEntity, VehicleMake>().
                ForMember(x => x.VehicleModel, opt => opt.Ignore());
                cfg.CreateMap<IVehicleMake, VehicleMakeEntity>().
                ForMember(x => x.VehicleModelEntity, opt => opt.Ignore());
                cfg.CreateMap<VehicleMake, VehicleMakeEntity>().
                ForMember(x => x.VehicleModelEntity, opt => opt.Ignore());
                cfg.CreateMap<VehicleModelEntity, IVehicleModel>().
                ForMember(x => x.MakeId, opt => opt.MapFrom(source => source.VehicleMakeEntity.Id)).
                ForMember(x => x.VehicleMake, opt => opt.Ignore());
                cfg.CreateMap<IVehicleModel, VehicleModelEntity>().
                ForMember(x => x.VehicleMakeEntity, opts => opts.Ignore());
                cfg.CreateMap<VehicleModelEntity, VehicleModel>().
                ForMember(x => x.MakeId, opt => opt.MapFrom(source => source.VehicleMakeEntity.Id)).
                 ForMember(x => x.VehicleMake, opt => opt.Ignore());
                cfg.CreateMap<VehicleModel, VehicleModelEntity>().
                ForMember(x => x.VehicleMakeEntity, opts => opts.Ignore());

            });
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

        }
        protected void Application_BeginRequest()
        {
            if (Request.Headers.AllKeys.Contains("Origin", StringComparer.CurrentCultureIgnoreCase)
                && Request.HttpMethod == "OPTIONS")
            {
                Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, Pragma, Cache-Control, Authorization ");
                Response.End();
            }
        }
    }
}
