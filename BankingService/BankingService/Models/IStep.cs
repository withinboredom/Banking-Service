using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingService.Models
{
    public interface IStep
    {
        /// <summary>
        /// The flow Id for which this step is for
        /// </summary>
        Guid FlowId { get; }

        /// <summary>
        /// The challenge for this step (pin, question, etc)
        /// </summary>
        string Challenge { get; }

        /// <summary>
        /// The response to the challenge
        /// </summary>
        string Response { get; set; }
    }
}