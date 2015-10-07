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
        /// <summary>
        /// Gets a table
        /// </summary>
        /// <param name="table">The table name to get</param>
        /// <param name="connectionString">The connection</param>
        /// <returns>A Cloud Table</returns>
        public static CloudTable GetTable(string table, string connectionString)
        {
            var storage = CloudStorageAccount.Parse(connectionString);
            var client = storage.CreateCloudTableClient().GetTableReference(table);
            client.CreateIfNotExists();
            return client;
        }

        /// <summary>
        /// Get's an object from the database
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize as</typeparam>
        /// <param name="table">The table object to connect to</param>
        /// <param name="partition">The partition key to get</param>
        /// <param name="row">The row key to get</param>
        /// <returns>An object from the database</returns>
        public static T GetObject<T>(CloudTable table, string partition, string row) where T : class, ITableEntity
        {
            return table.Execute(TableOperation.Retrieve<T>(partition, row)).Result as T;
        }

        /// <summary>
        /// Get's an object from the database
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize as</typeparam>
        /// <param name="table">The table object to connect to</param>
        /// <param name="partition">The partition key to get</param>
        /// <param name="row">The row key to get</param>
        /// <returns>An object from the database</returns>
        public static T GetObject<T>(CloudTable table, Guid partition, Guid row) where T : class, ITableEntity
        {
            return GetObject<T>(table, ToKey(partition), ToKey(row));
        }

        /// <summary>
        /// Get's just a sliver of a partition
        /// </summary>
        /// <typeparam name="T">The type of object</typeparam>
        /// <param name="table">The table to fetch from</param>
        /// <param name="pageSize">The number of elements to get</param>
        /// <param name="partition">The partition to get from</param>
        /// <param name="token">A continuation token</param>
        /// <returns></returns>
        private static TableQuerySegment<T> GetSegmentEntities<T>(CloudTable table, int pageSize, string partition,
            TableContinuationToken token) where T : class, ITableEntity, new()
        {
            var query =
                new TableQuery<T>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal,
                    partition));
            query.TakeCount = pageSize;
            return table.ExecuteQuerySegmented<T>(query, token);
        }

        /// <summary>
        /// Gets all objects from a specific partion key
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <param name="table">The table to search in</param>
        /// <param name="partition">The partition key to get</param>
        /// <returns></returns>
        public static IEnumerable<T> GetObject<T>(CloudTable table, string partition) where T : class, ITableEntity, new()
        {
            var result = GetSegmentEntities<T>(table, 100, partition, null);
            while (result.Results.Count > 0)
            {
                foreach (var ufs in result.Results)
                {
                    yield return ufs;
                }
                if (result.ContinuationToken != null)
                {
                    result = GetSegmentEntities<T>(table, 100, partition, result.ContinuationToken);
                }
                else
                {
                    yield break;
                }
            }
        }

        /// <summary>
        /// Sets an object in the database
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize</typeparam>
        /// <param name="table">The table to set</param>
        /// <param name="obj">The object to save</param>
        /// <returns>The saved object</returns>
        public static T SetObject<T>(CloudTable table, T obj) where T : class, ITableEntity
        {
            return table.Execute(TableOperation.InsertOrReplace(obj)).Result as T;
        }

        /// <summary>
        /// Converts a GUID to a SHA1 string
        /// </summary>
        /// <param name="id">The id to convert to SHA1</param>
        /// <returns>The SHA1</returns>
        public static string ToKey(Guid id)
        {
            return ToKey(id.ToString());
        }

        /// <summary>
        /// Converts an arbitrary string to a SHA1
        /// </summary>
        /// <param name="id">The string to convert</param>
        /// <returns>The SHA1</returns>
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
