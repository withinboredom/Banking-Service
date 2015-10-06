using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;
using Utility;

namespace KeyVault.Models
{
    public class StoredSecret : TableEntity, ISecret
    {
        public string ContentType { get; set; }
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }

        public StoredSecret() { }

        public StoredSecret(ISecret secret)
        {
            ContentType = secret.ContentType;
            Id = secret.Id;
            Value = secret.Value;
            PartitionKey = Cloud.ToKey(secret.Id);
            RowKey = Cloud.ToKey(secret.Name);
        }

        public static StoredSecret FromTable(CloudTable table, Guid id, string name)
        {
            return Cloud.GetObject<StoredSecret>(table, Cloud.ToKey(id), Cloud.ToKey(name));
        }
    }
}