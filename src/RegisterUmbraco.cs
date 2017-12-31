using Autofac;
using Autofac.Integration.Mvc;
using System.Configuration;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Web;

namespace MBran.Autofac
{
    public class RegisterUmbraco : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacWebTypesModule());
            var connectionString = ConfigurationManager.AppSettings["umbracoDbDSN"];

            if (!string.IsNullOrEmpty(connectionString))
            {
                builder.RegisterInstance(ApplicationContext.Current.DatabaseContext.Database)
                .As<UmbracoDatabase>()
                .InstancePerRequest();
            }

            builder.Register(c => UmbracoContext.Current).As<UmbracoContext>()
                .InstancePerRequest();
            builder.Register(c => new UmbracoHelper(UmbracoContext.Current))
                .As<UmbracoHelper>()
                .InstancePerRequest();

        }
    }
}
