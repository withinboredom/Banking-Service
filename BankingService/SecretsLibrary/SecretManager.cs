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
        private const string Table = "secrets";

        /// <summary>
        /// Creates a secret manager with a connection string and cloud
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="cloud">The cloud</param>
        public SecretManager(string connectionString, ICloud cloud)
        {
            _connectionString = connectionString;
            _cloud = cloud;
        }

        /// <summary>
        /// Creates a secret
        /// </summary>
        /// <param name="secretName">A secret</param>
        /// <param name="secret">More data about the secret</param>
        /// <returns>The saved secret</returns>
        public ISecret CreateSecret(string secretName, ISecret secret)
        {
            secret.Name = secretName;

            var store = new StoredSecret(secret);
            var table = _cloud.GetTable(Table, _connectionString);
            var exists = _cloud.GetObject<StoredSecret>(table, store.PartitionKey);
            
            store.Version = exists.Count() + 1;

            store = new StoredSecret(store);

            return _cloud.SetObject(table, store);
        }

        /// <summary>
        /// Gets a secret by name and version
        /// </summary>
        /// <param name="secretName">The name of the secret</param>
        /// <param name="version">The version of the secret to get</param>
        /// <returns></returns>
        public ISecret GetSecret(string secretName, int? version)
        {
            var table = _cloud.GetTable(Table, _connectionString);
            return new Secret(StoredSecret.FromTable(table, secretName, version));
        }

        /// <summary>
        /// Get's a secret by name
        /// </summary>
        /// <param name="secretName">The name of the secret</param>
        /// <returns></returns>
        public ISecret GetSecret(string secretName)
        {
            return GetSecret(secretName, null);
        }
    }
}
