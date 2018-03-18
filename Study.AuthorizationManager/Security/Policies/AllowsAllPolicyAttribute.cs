using Study.AuthorizationManager.Helpers;
using System;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Study.AuthorizationManager.Security.Policies
{
    public class AllowsAllPolicyAttribute : AuthorizationPolicyAttribute
    {
        protected override Task<bool> IsAuthorizedAsync(HttpActionContext actionContext)
        {
            AllowAllPolicy policy = actionContext.GetService<AllowAllPolicy>();
            return policy.IsAuthorizedAsync(actionContext);
        }
    }
}