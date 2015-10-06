using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyVault.Models
{
    public class Secret : ISecret
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public Guid Id { get; set; }

        public Secret() { }

        public Secret(ISecret secret)
        {
            Value = secret.Value;
            ContentType = secret.ContentType;
            Id = secret.Id;
        }
    }
}