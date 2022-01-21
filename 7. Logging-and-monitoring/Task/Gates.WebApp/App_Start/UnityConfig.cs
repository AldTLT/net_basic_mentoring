using System.Web.Http;
using AutoMapper;
using Gates.BL.Interfaces.Repository;
using Gates.BL.Interfaces.WebApi;
using Gates.BL.Managers;
using Gates.DAL.DataAccess;
using Gates.DAL.Identity;
using Gates.DAL.Mappers;
using Gates.DAL.Repository;
using NLog;
using Unity;
using Unity.WebApi;
using System.Linq;

namespace Gates.WebApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            var configuration = new MapperConfiguration(config => { config.AddProfile<EntityToViewModelProfile>(); });

            // Установка логгера
            var logger = LogManager.GetLogger("MainLogger");
            var enableLogging = LogManager.Configuration.Variables.FirstOrDefault(v => v.Key.Equals("enable"));

            if (enableLogging.Key == null || enableLogging.Value.Text != "true")
            {
                LogManager.DisableLogging();
            }

            // Установка зависимостей.
            container.RegisterType<ITaskManager, TaskManager>();
            container.RegisterType<ICommentManager, CommentManager>();
            var map = new Mapper(configuration);
            container.RegisterInstance<IMapper>(map, InstanceLifetime.Singleton);
            container.RegisterType<IProjectManager, ProjectManager>();
            container.RegisterType<IApplicationUserManager, ApplicationUserManager>();

            container.RegisterType<ITaskRepository, TaskRepository>();
            container.RegisterType<ICommentRepository, CommentRepository>();
            container.RegisterType<IProjectRepository, ProjectRepository>();
            container.RegisterType<IApplicationUserRepository, ApplicationUserRepository>();

            container.RegisterSingleton<UserContext>();
            container.RegisterType<IUserStore, ApplicationUserStore>();

            container.RegisterInstance<ILogger>(logger);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}