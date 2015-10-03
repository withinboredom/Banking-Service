using System;
using BankLibrary.DataConstructs;

namespace BankLibrary.Banks
{
    public interface IBankDriver
    {
        /// <summary>
        /// Attempts to login with a set of credentials
        /// </summary>
        /// <param name="creds">The credentials to login with</param>
        /// <returns>A step</returns>
        StepDefinition Login(Credentials creds);

        /// <summary>
        /// Performs a step with a given set of credentials
        /// </summary>
        /// <param name="step">The step to operate on</param>
        /// <param name="creds">The credentials to operate with</param>
        /// <returns>The next step</returns>
        StepDefinition DoStep(Guid step, Credentials creds);
    }
}