using System;
using System.Collections.Generic;

namespace KeyVault.Models.Auth
{
    public interface IAuth
    {
        List<Permissions> Permissions { get; set; }
        Guid UserId { get; set; }
        string Username { get; set; }
    }
}