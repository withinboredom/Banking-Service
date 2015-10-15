﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.9.7.0
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KeyVaultClient;
using KeyVaultClient.Models;
using Microsoft.Rest;

namespace KeyVaultClient
{
    public static partial class SecretOperationsExtensions
    {
        /// <param name='operations'>
        /// Reference to the KeyVaultClient.ISecretOperations.
        /// </param>
        /// <param name='secretName'>
        /// Required.
        /// </param>
        /// <param name='secret'>
        /// Required.
        /// </param>
        public static Secret CreateSecret(this ISecretOperations operations, string secretName, Secret secret)
        {
            return Task.Factory.StartNew((object s) => 
            {
                return ((ISecretOperations)s).CreateSecretAsync(secretName, secret);
            }
            , operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }
        
        /// <param name='operations'>
        /// Reference to the KeyVaultClient.ISecretOperations.
        /// </param>
        /// <param name='secretName'>
        /// Required.
        /// </param>
        /// <param name='secret'>
        /// Required.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        public static async Task<Secret> CreateSecretAsync(this ISecretOperations operations, string secretName, Secret secret, CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Microsoft.Rest.HttpOperationResponse<KeyVaultClient.Models.Secret> result = await operations.CreateSecretWithOperationResponseAsync(secretName, secret, cancellationToken).ConfigureAwait(false);
            return result.Body;
        }
        
        /// <param name='operations'>
        /// Reference to the KeyVaultClient.ISecretOperations.
        /// </param>
        /// <param name='secretName'>
        /// Required.
        /// </param>
        public static object DeleteSecret(this ISecretOperations operations, string secretName)
        {
            return Task.Factory.StartNew((object s) => 
            {
                return ((ISecretOperations)s).DeleteSecretAsync(secretName);
            }
            , operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }
        
        /// <param name='operations'>
        /// Reference to the KeyVaultClient.ISecretOperations.
        /// </param>
        /// <param name='secretName'>
        /// Required.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        public static async Task<object> DeleteSecretAsync(this ISecretOperations operations, string secretName, CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Microsoft.Rest.HttpOperationResponse<object> result = await operations.DeleteSecretWithOperationResponseAsync(secretName, cancellationToken).ConfigureAwait(false);
            return result.Body;
        }
        
        /// <param name='operations'>
        /// Reference to the KeyVaultClient.ISecretOperations.
        /// </param>
        /// <param name='secretName'>
        /// Required.
        /// </param>
        /// <param name='version'>
        /// Required.
        /// </param>
        public static Secret GetSecret(this ISecretOperations operations, string secretName, int version)
        {
            return Task.Factory.StartNew((object s) => 
            {
                return ((ISecretOperations)s).GetSecretAsync(secretName, version);
            }
            , operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }
        
        /// <param name='operations'>
        /// Reference to the KeyVaultClient.ISecretOperations.
        /// </param>
        /// <param name='secretName'>
        /// Required.
        /// </param>
        /// <param name='version'>
        /// Required.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        public static async Task<Secret> GetSecretAsync(this ISecretOperations operations, string secretName, int version, CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Microsoft.Rest.HttpOperationResponse<KeyVaultClient.Models.Secret> result = await operations.GetSecretWithOperationResponseAsync(secretName, version, cancellationToken).ConfigureAwait(false);
            return result.Body;
        }
        
        /// <param name='operations'>
        /// Reference to the KeyVaultClient.ISecretOperations.
        /// </param>
        /// <param name='secretName'>
        /// Required.
        /// </param>
        public static Secret GetSecretByName(this ISecretOperations operations, string secretName)
        {
            return Task.Factory.StartNew((object s) => 
            {
                return ((ISecretOperations)s).GetSecretByNameAsync(secretName);
            }
            , operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }
        
        /// <param name='operations'>
        /// Reference to the KeyVaultClient.ISecretOperations.
        /// </param>
        /// <param name='secretName'>
        /// Required.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        public static async Task<Secret> GetSecretByNameAsync(this ISecretOperations operations, string secretName, CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Microsoft.Rest.HttpOperationResponse<KeyVaultClient.Models.Secret> result = await operations.GetSecretByNameWithOperationResponseAsync(secretName, cancellationToken).ConfigureAwait(false);
            return result.Body;
        }
    }
}