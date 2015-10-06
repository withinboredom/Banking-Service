using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Utility
{
    public static class Cloud
    {
        public static CloudTable GetTable(string table, string connectionString)
        {
            var storage = CloudStorageAccount.Parse(connectionString);
            var client = storage.CreateCloudTableClient().GetTableReference(table);
            client.CreateIfNotExists();
            return client;
        }

        public static T GetObject<T>(CloudTable table, string partition, string row) where T : class, ITableEntity
        {
            return table.Execute(TableOperation.Retrieve<T>(partition, row)).Result as T;
        }

        public static T SetObject<T>(CloudTable table, T obj) where T : class, ITableEntity
        {
            return table.Execute(TableOperation.InsertOrReplace(obj)).Result as T;
        }

        public static string ToKey(Guid id)
        {
            return ToKey(id.ToString());
        }

        public static string ToKey(string id)
        {
            var hash = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(id));
            var sb = new StringBuilder();
            foreach (var b in hash)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
