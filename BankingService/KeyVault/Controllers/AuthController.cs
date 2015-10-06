using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KeyVault.Models.Auth;

namespace KeyVault.Controllers
{
    [RoutePrefix("auth")]
    public class AuthController : ApiController
    {
        public IAuth CreateAuth([FromBody] IAuth authRequest)
        {
            var connectionString = CloudConfigurationManager
        }
    }
}
