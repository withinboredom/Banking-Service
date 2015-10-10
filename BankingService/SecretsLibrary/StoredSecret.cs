using System;
using Interfaces.Secrets;
using Microsoft.WindowsAzure.Storage.Table;
using Utility;

namespace SecretsLibrary
{
    public class StoredSecret : TableEntity, ISecret
    {
        public string ContentType { get; set; }
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }

        public StoredSecret() { }

        public StoredSecret(ISecret secret)
        {
            ContentType = secret.ContentType;
            Id = secret.Id;
            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
            Value = secret.Value;
            PartitionKey = Cloud.GetCoud().ToKey(secret.Name);
            RowKey = secret.Version.ToString();
        }

        public static StoredSecret FromTable(CloudTable table, string name, int version)
        {
            return Cloud.GetCoud().GetObject<StoredSecret>(table, Cloud.GetCoud().ToKey(name), version.ToString());
        }
    }
}