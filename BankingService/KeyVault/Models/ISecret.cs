using System;

namespace KeyVault.Models
{
    public interface ISecret
    {
        string ContentType { get; set; }
        Guid Id { get; set; }
        string Value { get; set; }
        string Name { get; set; }
        int Version { get; set; }
    }
}