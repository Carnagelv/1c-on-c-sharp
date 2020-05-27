[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(EP.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(EP.App_Start.NinjectWebCommon), "Stop")]

namespace EP.App_Start
{
    using System;
    using System.Web;
    using EP.BusinessLogic.Infrastructure;
    using EP.BusinessLogic.Managers;
    using EP.BusinessLogic.Services;
    using EP.EntityData.Context;
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

            kernel.Bind<IUserProfileManager>().To<UserProfileManager>();
            kernel.Bind<IFriendManager>().To<FriendManager>();
            kernel.Bind<IRequestManager>().To<RequestManager>();
            kernel.Bind<INewsManager>().To<NewsManager>();
            kernel.Bind<ITeamManager>().To<TeamManager>();
            kernel.Bind<IFeedManager>().To<FeedManager>();
            kernel.Bind<IMessageManager>().To<MessageManager>();
            kernel.Bind<ITournamentManager>().To<TournamentManager>();
            kernel.Bind<IPlayerManager>().To<PlayerManager>();
            kernel.Bind<IMovieManager>().To<MovieManager>();
            kernel.Bind<IMatchManager>().To<MatchManager>();

            kernel.Bind<IUserProfileService>().To<UserProfileService>();           
            kernel.Bind<IFriendService>().To<FriendService>();          
            kernel.Bind<IRequestToFriendService>().To<RequestToFriendService>();
            kernel.Bind<INewsService>().To<NewsService>();
            kernel.Bind<INewsLikeService>().To<NewsLikeService>();
            kernel.Bind<INewsCommentaryService>().To<NewsCommentaryService>();
            kernel.Bind<ITeamService>().To<TeamService>();
            kernel.Bind<IFeedService>().To<FeedService>();
            kernel.Bind<IMessageService>().To<MessageService>();
            kernel.Bind<ITournamentService>().To<TournamentService>();
            kernel.Bind<IPlayerService>().To<PlayerService>();
            kernel.Bind<IMovieService>().To<MovieService>();
            kernel.Bind<IMatchService>().To<MatchService>();
        }
    }
}
