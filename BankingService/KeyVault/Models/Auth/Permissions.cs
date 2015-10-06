using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace KeyVault.Models.Auth
{
    public class Permissions : TableEntity, IPermissions
    {
        public Guid PermissionId { get; set; }
        public Guid TenantId { get; set; }
        public string Description { get; set; }
    }
}