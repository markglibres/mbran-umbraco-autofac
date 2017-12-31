# mbran-umbraco-autofac
Autofac bootstrap for Umbraco websites

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
