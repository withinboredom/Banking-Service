using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.DynamicData;
using System.Web.Http;
using KeyVault.Models.Auth;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace KeyVault.Controllers
{
    [RoutePrefix("auth")]
    public class AuthController : ApiController
    {
        [Route("create")]
        [HttpPut]
        public IAuth CreateAuth([FromBody] Auth authRequest)
        {
            var request = authRequest as Auth;

            if (request == null)
            {
                throw new NullReferenceException("Request of invalid type");
            }

            request.PartitionKey = ToKey(request.Tenant);
            request.RowKey = ToKey(request.UserId);

            var exists = GetObject<Auth>("auth", request.PartitionKey, request.RowKey);

            var perms = request.Permissions;
            request.Permissions = new List<Permissions>();

            foreach (var perm in perms)
            {
                request.Permissions.Add(GetObject<Permissions>("permission", ToKey(perm.TenantId),
                    ToKey(perm.PermissionId)));
            }

            return SetObject("auth", request);
        }

        private CloudTable GetTable(string table)
        {
            var connectionString = CloudConfigurationManager.GetSetting("Auth:Storage");
            var storage = CloudStorageAccount.Parse(connectionString);

            var client = storage.CreateCloudTableClient().GetTableReference(table);
            client.CreateIfNotExists();
            return client;
        }

        private T GetObject<T>(string table, string partition, string row) where T : class, ITableEntity
        {
            return GetTable(table).Execute(TableOperation.Retrieve<T>(partition, row)).Result as T;
        }

        private T SetObject<T>(string table, T obj) where T : class, ITableEntity
        {
            return GetTable(table).Execute(TableOperation.InsertOrReplace(obj)).Result as T;
        }

        private string ToKey(Guid t)
        {
            var hash = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(t.ToString()));
            var sb = new StringBuilder();
            foreach (var b in hash)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        [Route("permission/create")]
        [HttpPut]
        public void CreatePermission([FromBody] Permissions permission)
        {
            var perm = permission as Permissions;

            if (perm == null)
            {
                throw new ArgumentNullException(nameof(permission), "WTF");
            }

            var existing = GetObject<Permissions>("permission", ToKey(permission.TenantId),
                ToKey(permission.PermissionId));

            perm.PartitionKey = ToKey(perm.TenantId);
            perm.RowKey = ToKey(perm.PermissionId);

            if (existing != null && existing.Description != perm.Description)
            {
                throw new StorageException("Already exists!");
            }

            SetObject("permission", perm);
        }
    }
}
