using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace KeyVault.Models.Auth
{
    public class Auth : TableEntity, IAuth
    {
        public string Username { get; set; }
        public Guid UserId { get; set; }
        public List<Permissions> Permissions { get; set; } 
    }
}