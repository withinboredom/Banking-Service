using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Table;
using SecretsLibrary;
using Utility;

namespace KeyVault.Controllers
{
    [RoutePrefix("secrets")]
    public class SecretController : ApiController
    {
        [Route("{secretName:alpha}")]
        [HttpPut]
        public Secret CreateSecret([FromUri] string secretName, [FromBody] Secret secret)
        {
            var manager = new SecretManager(CloudConfigurationManager.GetSetting("Auth:Storage"), Cloud.GetCoud());
            return new Secret(manager.CreateSecret(secretName, secret));
        }

        [Route("{secretName:alpha}/{version:int?}")]
        [HttpGet]
        public Secret GetSecret([FromUri] string secretName, [FromUri] int? version = null)
        {
            var manager = new SecretManager(CloudConfigurationManager.GetSetting("Auth:Storage"), Cloud.GetCoud());
            return new Secret(manager.GetSecret(secretName, version));
        }

        [Route("{secretName:alpha}")]
        [HttpGet]
        public Secret GetSecretByName([FromUri] string secretName)
        {
            var manager = new SecretManager(CloudConfigurationManager.GetSetting("Auth:Storage"), Cloud.GetCoud());
            return new Secret(manager.GetSecret(secretName));
        }

        [Route("{secretName:alpha}")]
        [HttpDelete]
        public void DeleteSecret([FromUri] string secretName)
        {
            var manager = new SecretManager(CloudConfigurationManager.GetSetting("Auth:Storage"), Cloud.GetCoud());
            manager.DeleteSecret(new Secret() {Name = secretName});
        }
    }
}
