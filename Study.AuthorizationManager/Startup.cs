using Microsoft.Owin;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using Serilog;
using SerilogWeb.Classic.WebApi.Enrichers;
using Study.AuthorizationManager.Security.Policies;
using System;
using System.IO;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(Study.AuthorizationManager.Startup))]

namespace Study.AuthorizationManager
{
    public class Startup
    {
        public Startup()
        {
            Log.Logger = new LoggerConfiguration()
                            .Enrich.With<WebApiRouteTemplateEnricher>()
                            .Enrich.With<WebApiActionNameEnricher>()
                            .Enrich.With<WebApiControllerNameEnricher>()
                            .WriteTo.File("Authorization-Study.log")
                            .CreateLogger();
        }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            Configure(config);
            app.Use((context, next) =>
            {
                TextWriter output = context.Get<TextWriter>("host.TraceOutput");
                return next().ContinueWith(result =>
                {
                    output.WriteLine("Scheme {0} : Method {1} : Path {2} : MS {3}",
                    context.Request.Scheme, context.Request.Method, context.Request.Path, GetTime());
                });
            });

            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);
            //app.UseWebApi(config);
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync(getTime() + " My First OWIN App");
            //});
        }

        private void Configure(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<AllowAllPolicy>().ToSelf();
            return kernel;
        }

        private static string GetTime()
        {
            return DateTime.Now.Millisecond.ToString();
        }
    }
}