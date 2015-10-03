using System;

namespace Interfaces
{
    public interface IBankDriver
    {
        /// <summary>
        /// Attempts to login with a set of credentials
        /// </summary>
        /// <param name="creds">The credentials to login with</param>
        /// <returns>A step</returns>
        IStepDefinition Login(ICredentials creds);

        /// <summary>
        /// Performs a step with a given set of credentials
        /// </summary>
        /// <param name="step">The step to operate on</param>
        /// <param name="creds">The credentials to operate with</param>
        /// <returns>The next step</returns>
        IStepDefinition DoStep(Guid step, ICredentials creds);
    }
}