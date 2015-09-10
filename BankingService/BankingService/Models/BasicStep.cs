using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingService.Models
{
    public class BasicStep : IStep
    {
        public Guid FlowId { get; }
        public string Challenge { get; }
        public string Response { get; set; }

        public BasicStep(Guid flow, string challenge, string response = null)
        {
            this.FlowId = flow;
            this.Challenge = challenge;
            this.Response = response;
        }
    }
}