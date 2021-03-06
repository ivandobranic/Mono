[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MVC.App_Start.NinjectWebCommon), "Stop")]

namespace MVC.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Project.Common.Caching;
    using Project.DAL.Entities;
    using Project.Model;
    using Project.Model.Common;
    using Project.Repository;
    using Project.Repository.Common;
    using Project.Service;
    using Project.Service.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {

            kernel.Bind<IRepository<VehicleMakeEntity>>().To<GenericRepository<VehicleMakeEntity>>();
            kernel.Bind<IRepository<VehicleModelEntity>>().To<GenericRepository<VehicleModelEntity>>();
            kernel.Bind<IMakeRepository>().To<VehicleMakeRepository>();
            kernel.Bind<IModelRepository>().To<VehicleModelRepository>();
            kernel.Bind<IVehicleMake>().To<VehicleMake>();
            kernel.Bind<IVehicleModel>().To<VehicleModel>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IVehicleMakeService>().To<VehicleMakeService>();
            kernel.Bind<IVehicleModelService>().To<VehicleModelService>();
            kernel.Bind<ICaching>().To<Caching>();
            kernel.Bind<IFilter>().To<Filter>();
            

        }
    }
}
