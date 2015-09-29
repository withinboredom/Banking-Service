using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingService.Models.DataConstructs
{
    /// <summary>
    /// What's needed
    /// </summary>
    public class StepDefinition
    {
        /// <summary>
        /// The id of the step
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// The field to set
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// The data hint on what the field should contain
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Whether or not this step was successful
        /// </summary>
        public bool? Successful { get; set; }

        /// <summary>
        /// The id of the next step to perform, if applicabale...
        /// </summary>
        public Guid? NextStep { get; set; }
    }
}