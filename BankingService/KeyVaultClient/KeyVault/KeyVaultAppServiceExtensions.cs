using System;
using System.Net.Http;
using Microsoft.Azure.AppService;

namespace KeyVaultClient
{
    public static class KeyVaultAppServiceExtensions
    {
        public static KeyVault CreateKeyVault(this IAppServiceClient client)
        {
            return new KeyVault(client.CreateHandler());
        }

        public static KeyVault CreateKeyVault(this IAppServiceClient client, params DelegatingHandler[] handlers)
        {
            return new KeyVault(client.CreateHandler(handlers));
        }

        public static KeyVault CreateKeyVault(this IAppServiceClient client, Uri uri, params DelegatingHandler[] handlers)
        {
            return new KeyVault(uri, client.CreateHandler(handlers));
        }

        public static KeyVault CreateKeyVault(this IAppServiceClient client, HttpClientHandler rootHandler, params DelegatingHandler[] handlers)
        {
            return new KeyVault(rootHandler, client.CreateHandler(handlers));
        }
    }
}
