using System;
using Interfaces;
using Interfaces.Banking;

namespace DataLibrary.DataConstructs
{
    /// <summary>
    /// A simple set of credentials
    /// </summary>
    public class Credentials : ICredentials
    {
        /// <summary>
        /// A username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// A password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// A pin
        /// </summary>
        public string Pin { get; set; }

        /// <summary>
        /// An answer to a question
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// The bank id these credentials are associated with
        /// </summary>
        public Guid? BankId { get; set; }

        /// <summary>
        /// The step id of these credentials
        /// </summary>
        public Guid? StepId { get; set; }
    }
}