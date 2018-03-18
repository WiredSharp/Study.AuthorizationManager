using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Study.AuthorizationManager.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple =true)]
    public abstract class AuthorizationPolicyAttribute : Attribute, IAuthorizationFilter
    {
        public bool AllowMultiple => true;

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                if (await IsAuthorizedAsync(actionContext))
                {
                    return await continuation();
                }
                else
                {
                    return actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "user is not authorized");
                }
            }
            else
            {
                return actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "user not authenticated");
            }
        }

        protected abstract Task<bool> IsAuthorizedAsync(HttpActionContext actionContext);
        //{
        //    string name = actionContext.RequestContext.Principal.Identity.Name;
        //    return actionContext.GetService<AllowAllPolicy>().IsAuthorized(name);
        //}
    }
}