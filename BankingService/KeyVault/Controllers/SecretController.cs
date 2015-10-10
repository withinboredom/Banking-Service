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
            var table = Cloud.GetCoud().GetTable("secrets", CloudConfigurationManager.GetSetting("Auth:Storage"));

            if (version.HasValue) return new Secret(StoredSecret.FromTable(table, secretName, version.Value));

            var exists = Cloud.GetCoud().GetObject<StoredSecret>(table, Cloud.GetCoud().ToKey(secretName));
            version = exists.Count();

            return new Secret(StoredSecret.FromTable(table, secretName, version.Value));
        }

        [Route("{secretName:alpha}")]
        [HttpDelete]
        public void DeleteSecret([FromUri] string secretName)
        {
            var table = Cloud.GetCoud().GetTable("secrets", CloudConfigurationManager.GetSetting("Auth:Storage"));

            var exists = Cloud.GetCoud().GetObject<StoredSecret>(table, Cloud.GetCoud().ToKey(secretName));

            foreach (var secretVersion in exists)
            {
                table.Execute(TableOperation.Delete(secretVersion));
            }
        }
    }
}
