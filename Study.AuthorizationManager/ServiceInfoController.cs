using Study.AuthorizationManager.Security.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Study.AuthorizationManager
{
    [RoutePrefix("info")]
    public class ServiceInfoController : ApiController
    {
        private ServiceInfo _info;

        public ServiceInfoController()
        {
            _info = new ServiceInfo();
        }

        [AllowsAllPolicy()]
        [HttpGet]
        [Route("sec")]
        public Task<IHttpActionResult> GetInfo()
        {
            return Task.FromResult((IHttpActionResult)Ok(_info));
        }

        [HttpGet]
        [Route("ano")]
        public Task<IHttpActionResult> GetAnonymousInfo()
        {
            return Task.FromResult((IHttpActionResult)Ok(_info));
        }
    }
}
