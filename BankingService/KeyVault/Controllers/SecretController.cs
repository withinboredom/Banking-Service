using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KeyVault.Models;
using Microsoft.Azure;
using Utility;

namespace KeyVault.Controllers
{
    [RoutePrefix("secrets")]
    public class SecretController : ApiController
    {
        [Route("{secretName}")]
        [HttpPut]
        public Secret CreateSecret([FromUri] string secretName, [FromBody] Secret secret)
        {
            secret.Name = secretName;

            var store = new StoredSecret(secret);

            var table = Cloud.GetTable("secrets", CloudConfigurationManager.GetSetting("Auth:Storage"));
            return new Secret(Cloud.SetObject(table, store));
        }

        [Route("{secretName}/{id}")]
        [HttpGet]
        public Secret GetSecret([FromUri] string secretName, [FromUri] Guid id)
        {
            var table = Cloud.GetTable("secrets", CloudConfigurationManager.GetSetting("Auth:Storage"));
            return new Secret(StoredSecret.FromTable(table, id, secretName));
        }
    }
}
