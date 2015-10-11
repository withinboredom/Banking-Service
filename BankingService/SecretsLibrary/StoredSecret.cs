using System;
using System.Linq;
using System.Reflection.Emit;
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
            Name = secret.Name;
            Version = secret.Version;
            PartitionKey = Cloud.GetCoud().ToKey(Name);
            RowKey = PartitionKey + ":" + Version.ToString();
        }

        public static StoredSecret FromTable(CloudTable table, string name, int? version)
        {
            if (!version.HasValue)
            {
                var exists = Cloud.GetCoud().GetObject<StoredSecret>(table, Cloud.GetCoud().ToKey(name));
                version = exists.Count();
            }

            return Cloud.GetCoud().GetObject<StoredSecret>(table, Cloud.GetCoud().ToKey(name), version.ToString());
        }
    }
}