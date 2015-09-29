using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingService.Models.DataConstructs
{
    /// <summary>
    /// A simple set of credentials
    /// </summary>
    public class Credentials
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