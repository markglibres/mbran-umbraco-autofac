using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using Umbraco.Web;

namespace MBran.Autofac
{
    public static class AutofacBuilderExtensions
    {
        
        public static ContainerBuilder RegisterAssemblies(this ContainerBuilder builder)
        {
            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();
            foreach (var assembly in assemblies)
            {
                builder.RegisterControllers(assembly);
                builder.RegisterApiControllers(assembly);
                builder.RegisterCustomControllers(assembly)
                    .RegisterServices(assembly)
                    .RegisterRepositories(assembly);
                builder.RegisterAssemblyModules(assembly);
            }
            
            return builder;
        }

        public static ContainerBuilder RegisterCustomControllers(this ContainerBuilder builder, Assembly executingAssembly)
        {
            builder.RegisterControllers(typeof(UmbracoApplication).Assembly);
            builder.RegisterApiControllers(typeof(UmbracoApplication).Assembly);

            builder.RegisterAssemblyTypes(executingAssembly)
                .Where(c => c.Name.EndsWith("Controller", StringComparison.CurrentCultureIgnoreCase))
                .AsImplementedInterfaces()
                .InstancePerDependency();
            return builder;
        }

        public static ContainerBuilder RegisterServices(this ContainerBuilder builder, Assembly executingAssembly)
        {
            builder.RegisterAssemblyTypes(executingAssembly)
                .Where(c => c.Name.EndsWith("Service", StringComparison.CurrentCultureIgnoreCase))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            return builder;
        }
        public static ContainerBuilder RegisterRepositories(this ContainerBuilder builder, Assembly executingAssembly)
        {
            builder.RegisterAssemblyTypes(executingAssembly)
                .Where(c => c.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            return builder;
        }

        public static void BuildContainer(this ContainerBuilder builder)
        {
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }

    }
}
