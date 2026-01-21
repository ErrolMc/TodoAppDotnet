using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using TodoAppBackend.Data;
using TodoAppBackend.Repositories;
using TodoAppBackend.Repositories.Concrete;

namespace TodoAppBackend.App_Start
{
    public static class AutofacConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // Register API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register DbContext with InstancePerRequest lifetime
            builder.RegisterType<ApplicationDbContext>()
                .AsSelf()
                .InstancePerRequest();

            // Register repositories
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerRequest();

            builder.RegisterType<TodoItemRepository>()
                .As<ITodoItemRepository>()
                .InstancePerRequest();

            // Build the container
            var container = builder.Build();

            // Set the dependency resolver
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
