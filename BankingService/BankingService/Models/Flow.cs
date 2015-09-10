using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingService.Models
{
    public class Flow : IFlow
    {
        public Guid Id { get; set; }
        public Guid FlowId { get; set; }
    }
}