# mbran-umbraco-autofac
Autofac bootstrap for Umbraco websites. 

Install through Nuget: Install-Package MBran.Autofac

This will auto register all classes that ends with "Controller", "Service" and "Repository". UmbracoDatabase, UmbracoContext, and UmbracoHelper are also registered.

1. Install Autofac from NuGet: Install-Package Autofac
2. Create a class on your Umbraco app and inherit from IApplicationEventHandler.
3. Paste the following code":

```cs

using MBran.Autofac;
using Umbraco.Core;

public class AutofacEventHandler : IApplicationEventHandler
{
    public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, 
        ApplicationContext applicationContext)
    {
        var builder = IoCBuilder.Instance.GetBuilder(applicationContext);
        builder.BuildContainer();
    }
}

```

To register your own services, create an Autofac Module. For example:

```cs

using Autofac;

public class RegisterUmbraco : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //register your services here
        builder.RegisterType<MyService>()
               .As<IService>();
    }
}

```
