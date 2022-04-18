using Microsoft.Owin.Builder;
using Microsoft.Owin.BuilderProperties;
using Owin;

public static class OwinApplicationBuilderExtensions
{
    public static IApplicationBuilder UseOwinAppBuilder(this IApplicationBuilder app, Action<IAppBuilder> configuration)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        return app.UseOwin(setup => setup(next =>
        {
            var builder = new AppBuilder();
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            var webHostEnv = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            var properties = new AppProperties(builder.Properties);
            properties.AppName = webHostEnv.ApplicationName;
            properties.OnAppDisposing = lifetime.ApplicationStopping;
            properties.DefaultApp = next;

            configuration(builder);

            return builder.Build<Func<IDictionary<string, object>, Task>>();
        }));
    }
}
