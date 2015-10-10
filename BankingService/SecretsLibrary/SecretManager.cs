using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Interfaces.Secrets;
using Utility;

namespace SecretsLibrary
{
    public class SecretManager
    {
        private readonly string _connectionString;
        private readonly ICloud _cloud;

        public SecretManager(string connectionString, ICloud cloud)
        {
            _connectionString = connectionString;
            _cloud = cloud;
        }

        public ISecret CreateSecret(string secretName, ISecret secret)
        {
            secret.Name = secretName;

            var store = new StoredSecret(secret);
            var table = _cloud.GetTable("secrets", _connectionString);
            var exists = _cloud.GetObject<StoredSecret>(table, store.PartitionKey);
            
            store.Version = exists.Count();

            return _cloud.SetObject(table, store);
        }
    }
}
