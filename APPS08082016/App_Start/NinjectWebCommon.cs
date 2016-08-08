using APPS08082016.Core.EntityModel;
using APPS08082016.Data.DatabaseContext;
using APPS08082016.Data.DatabaseContext.Contract;
using APPS08082016.Data.Repository;
using APPS08082016.Data.Repository.Contract;
using APPS08082016.Service;
using APPS08082016.Service.Contract;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(APPS08082016.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(APPS08082016.App_Start.NinjectWebCommon), "Stop")]

namespace APPS08082016.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

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
            kernel.Bind<IDbContext>().To<ObjectContext>();
            kernel.Bind<IRepository<EmployeeInfoEntity>>().To<EfRepository<EmployeeInfoEntity>>();
            kernel.Bind<IEmployeeServices>().To<EmployeeServices>();
        }
    }
}
