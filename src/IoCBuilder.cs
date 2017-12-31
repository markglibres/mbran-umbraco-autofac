using Autofac;
using System;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace MBran.Autofac
{
    public class IoCBuilder
    {
        private static readonly Lazy<IoCBuilder> _instance = 
            new Lazy<IoCBuilder>(() => new IoCBuilder());
        public static IoCBuilder Instance => _instance.Value;

        private ContainerBuilder _builder;

        private IoCBuilder()
        {
            
        }
               

        public ContainerBuilder GetBuilder(ApplicationContext applicationContext)
        {
            if(_builder != null)
            {
                return _builder;
            }

            _builder = new ContainerBuilder();
            
            _builder.RegisterInstance(applicationContext.Services.ContentService).As<IContentService>();
            
            _builder.RegisterAssemblies();

            return _builder;
        }
    }
}
