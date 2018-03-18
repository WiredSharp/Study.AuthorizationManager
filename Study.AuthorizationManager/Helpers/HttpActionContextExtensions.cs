using System.Web.Http.Controllers;

namespace Study.AuthorizationManager.Helpers
{
    internal static class HttpActionContextExtensions
    {
        public static TService GetService<TService>(this HttpActionContext actionContext)
        {
            return actionContext.RequestContext.Configuration.GetService<TService>();
        }
    }
}