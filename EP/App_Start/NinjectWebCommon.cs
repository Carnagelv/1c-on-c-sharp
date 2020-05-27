[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(OneC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(OneC.App_Start.NinjectWebCommon), "Stop")]

namespace OneC.App_Start
{
    using System;
    using System.Web;
    using OneC.BusinessLogic.Infrastructure;
    using OneC.BusinessLogic.Managers;
    using OneC.BusinessLogic.Services;
    using OneC.EntityData.Context;
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
            DependencyResolver.Initialize(kernel);
            kernel.Bind<IDataContext>().To<DataContext>().InRequestScope();

            kernel.Bind<ITableColumnService>().To<TableColumnService>();
            kernel.Bind<ITableColumnManager>().To<TableColumnManager>();

            kernel.Bind<ITableItemService>().To<TableItemService>();
            kernel.Bind<ITableItemManager>().To<TableItemManager>();
        }
    }
}
