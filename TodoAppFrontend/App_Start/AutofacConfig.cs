using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using TodoAppFrontend.Services;
using TodoAppFrontend.Services.Concrete;

namespace TodoAppFrontend.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            // Register MVC controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register model binders
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // Register web abstractions like HttpContextBase
            builder.RegisterModule<AutofacWebTypesModule>();

            // Register filters
            builder.RegisterFilterProvider();

            // Register services
            builder.RegisterType<AuthService>().As<IAuthService>().SingleInstance();
            builder.RegisterType<TodoService>().As<ITodoService>().InstancePerRequest();

            // Build the container
            var container = builder.Build();

            // Set the dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
