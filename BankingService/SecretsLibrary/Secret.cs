using System;
using Interfaces.Secrets;

namespace SecretsLibrary
{
    public class Secret : ISecret
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public string ContentType { get; set; }
        public Guid Id { get; set; }

        public Secret() { }

        public Secret(ISecret secret)
        {
            Value = secret.Value;
            Name = secret.Name;
            Version = secret.Version;
            ContentType = secret.ContentType;
            Id = secret.Id;
        }
    }
}