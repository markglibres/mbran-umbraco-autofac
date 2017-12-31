# mbran-umbraco-autofac
Autofac bootstrap for Umbraco websites

This will auto register all classes that ends with "Controller", "Service" and "Repository". 

1. Create a class that inherits from IApplicationEventHandler.
2. Paste the following code":

```cs

public class AutofacEventHandler : IApplicationEventHandler
{
    public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
    {
        var builder = IoCBuilder.Instance.GetBuilder(applicationContext);
        builder.BuildContainer();
    }
}

```

To register your own services, create an Autofac Module. For example:

```cs

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
