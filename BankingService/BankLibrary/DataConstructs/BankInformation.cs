using System;
using System.Collections.Generic;

namespace BankLibrary.DataConstructs
{
    /// <summary>
    /// Contains information about a banking institution
    /// </summary>
    public class BankInformation
    {
        /// <summary>
        /// The id that led you this far (eg: USAA)
        /// </summary>
        public string Canonical { get; set; }

        /// <summary>
        /// The id you need everywhere else
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The homepage of this bank
        /// </summary>
        public Uri Homepage { get; set; }

        /// <summary>
        /// The logo of this bank
        /// </summary>
        public Uri Logo { get; set; }

        /// <summary>
        /// The steps required to login
        /// </summary>
        public List<StepDefinition> Steps { get; set; } 
    }
}