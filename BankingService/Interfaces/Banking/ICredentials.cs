using System;

namespace Interfaces.Banking
{
    public interface ICredentials
    {
        /// <summary>
        /// A username
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// A password
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// A pin
        /// </summary>
        string Pin { get; set; }

        /// <summary>
        /// An answer to a question
        /// </summary>
        string Question { get; set; }

        /// <summary>
        /// A bank id for these credentials
        /// </summary>
        Guid? BankId { get; set; }

        /// <summary>
        /// The step id this is meant for
        /// </summary>
        Guid? StepId { get; set; }
    }
}