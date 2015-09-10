using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingService.Models
{
    /// <summary>
    /// A flow abstraction
    /// </summary>
    
    public interface IFlow
    {
        /// <summary>
        /// An identifier for the aggregate that it comes from
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// An id specific to this flow
        /// </summary>
        Guid FlowId { get; set; }
    }
}