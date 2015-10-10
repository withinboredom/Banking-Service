using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Secrets;
using Utility;

namespace SecretsLibrary
{
    public class SecretManager
    {
        private readonly string _connectionString;

        public SecretManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ISecret CreateSecret(string secretName, ISecret secret)
        {
            secret.Name = secretName;

            var store = new StoredSecret(secret);
            var table = Cloud.GetCoud().GetTable("secrets", _connectionString);
            var exists = Cloud.GetCoud().GetObject<StoredSecret>(table, store.PartitionKey);
            
            store.Version = exists.Count();

            return new Secret(Cloud.GetCoud().SetObject(table, store));
        }
    }
}
