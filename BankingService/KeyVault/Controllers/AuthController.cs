using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
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
        public IAuth CreateAuth([FromBody] IAuth authRequest)
        {
            var request = authRequest as Auth;

            if (request == null)
            {
                throw new NullReferenceException("Request of invalid type");
            }

            request.PartitionKey = SHA1.Create(request.Tenant.ToString()).ToString();
            request.RowKey = SHA1.Create(request.UserId.ToString()).ToString();

            authRequest.Permissions = new List<Permissions>();

            var admin = new Permissions()
            {
                Description = "Admin",
                PermissionId = Guid.NewGuid()
            };

            authRequest.Permissions.Add(admin);

            return request;
        }

        private CloudTable GetTable(string table)
        {
            var connectionString = CloudConfigurationManager.GetSetting("Auth:Storage");
            var storage = CloudStorageAccount.Parse(connectionString);

            var client = storage.CreateCloudTableClient().GetTableReference(table);
            client.CreateIfNotExists();
            return client;
        }

        private T GetObject<T>(string table, string partition, string row) where T : class
        {
            return GetTable(table).Execute(TableOperation.Retrieve(partition, row)).Result as T;
        }

        private void SetObject<T>(string table, T obj) where T : ITableEntity
        {
            GetTable(table).Execute(TableOperation.InsertOrReplace(obj));
        }

        private string ToKey(Guid t)
        {
            return SHA1.Create(t.ToString()).ToString();
        }

        [Route("permission/create")]
        [HttpPut]
        public void CreatePermission([FromBody] IPermissions permission)
        {
            var perm = permission as Permissions;

            if (perm == null)
            {
                throw new ArgumentNullException(nameof(permission), "WTF");
            }

            var existing = GetObject<Permissions>("permission", ToKey(permission.TenantId),
                ToKey(permission.PermissionId));

            if (existing != null && existing.Description != perm.Description)
            {
                throw new StorageException("Already exists!");
            }

            SetObject("permission", perm);
        }
    }
}
