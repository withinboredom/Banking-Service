using System;

namespace KeyVault.Models.Auth
{
    public interface IPermissions
    {
        string Description { get; set; }
        Guid PermissionId { get; set; }
        Guid TenantId { get; set; }
    }
}