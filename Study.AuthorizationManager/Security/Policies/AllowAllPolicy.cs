using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Study.AuthorizationManager.Security.Policies
{
    public class AllowAllPolicy
    {
        public string Name => "Allows All";

        public string Application { get; set; }

        public string Role { get; set; }

        public Task<bool> IsAuthorizedAsync(HttpActionContext actionContext)
        {
            return Task.FromResult(true);
        }
    }
}