using System;
using System.Web.Http;

namespace Study.AuthorizationManager.Helpers
{
    internal static class HttpConfigurationExtensions
    {
        public static TService GetService<TService>(this HttpConfiguration configuration)
        {
            return (TService)configuration.GetService(typeof(TService));
        }

        public static object GetService(this HttpConfiguration configuration, Type serviceType)
        {
            return configuration.DependencyResolver.GetService(serviceType);
        }
    }
}