﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.9.7.0
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KeyVaultClient.Models;
using Microsoft.Rest;

namespace KeyVaultClient
{
    public partial interface ISecretOperations
    {
        /// <param name='secretName'>
        /// Required.
        /// </param>
        /// <param name='secret'>
        /// Required.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        Task<HttpOperationResponse<Secret>> CreateSecretWithOperationResponseAsync(string secretName, Secret secret, CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        
        /// <param name='secretName'>
        /// Required.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        Task<HttpOperationResponse<object>> DeleteSecretWithOperationResponseAsync(string secretName, CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        
        /// <param name='secretName'>
        /// Required.
        /// </param>
        /// <param name='version'>
        /// Required.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        Task<HttpOperationResponse<Secret>> GetSecretWithOperationResponseAsync(string secretName, int version, CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        
        /// <param name='secretName'>
        /// Required.
        /// </param>
        /// <param name='cancellationToken'>
        /// Cancellation token.
        /// </param>
        Task<HttpOperationResponse<Secret>> GetSecretByNameWithOperationResponseAsync(string secretName, CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    }
}
