using Microsoft.AspNetCore.Identity;
using Owin;
using System.Web.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseOwinAppBuilder(owinApp =>
{
    var httpConfig = new HttpConfiguration();
    httpConfig.Routes.MapHttpRoute(
        name: "DefaultApi",
        routeTemplate: "api/{controller}/{id}",
        defaults: new { id = RouteParameter.Optional }
    );
    owinApp.UseWebApi(httpConfig);
});

app.Run();
