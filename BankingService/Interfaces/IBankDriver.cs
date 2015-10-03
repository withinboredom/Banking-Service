using System;

namespace Interfaces
{
    public interface IBankDriver : IDisposable
    {
        /// <summary>
        /// Attempts to login with a set of credentials
        /// </summary>
        /// <param name="creds">The credentials to login with</param>
        /// <param name="bankId">The id of the bank to login to</param>
        /// <returns>A step</returns>
        IStepDefinition Login(ICredentials creds, Guid bankId);

        /// <summary>
        /// Performs a step with a given set of credentials
        /// </summary>
        /// <param name="step">The step to operate on</param>
        /// <param name="creds">The credentials to operate with</param>
        /// <returns>The next step</returns>
        IStepDefinition DoStep(Guid step, ICredentials creds);
    }
}