using Interfaces;

namespace BankLibrary.DataConstructs
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
    }
}