using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using BankingService.Models;
using BankingService.Models.Banks;

namespace BankingService.Controllers
{
    public class InstitutionController : ApiController
    {
        /// <summary>
        /// Begins a login flow to a bank
        /// </summary>
        /// <param name="username">The username to signin with</param>
        /// <param name="password">The password to signin with</param>
        /// <param name="institution">The institution to signin to</param>
        /// <returns>Flow state</returns>
        [method: HttpPost]
        [ActionName("BeginLogin")]
        public IFlow BeginLogin(string username, string password, Institutions institution)
        {
            return new Flow() { FlowId = Guid.NewGuid(), Id = Guid.NewGuid() };
        }

        [method: HttpGet]
        [ActionName("GetBankInformation")]
        public IBank GetBankInformation(Institutions institutions)
        {
            return new Usaa();
        }

        [method: HttpPost]
        [ActionName("GetRealtimeLoginStepStatus")]
        public LoginStepStatus GetRealtimeLoginStepStatus(IFlow query)
        {
            return LoginStepStatus.Complete;
        }

        [method: HttpPost]
        [ActionName("GetFlowResult")]
        IEnumerable<IStep> GetFlowResult(IFlow query)
        {
            var ret = new List<IStep>();
            var step = new BasicStep(query.FlowId, "Ping?");
            ret.Add(step);

            return ret;
        }

        [method: HttpPost]
        [ActionName("RespondToChallenge")]
        IFlow RespondToChallenge(IEnumerable<IStep> responses)
        {
            return new Flow() { FlowId = Guid.NewGuid(), Id = Guid.NewGuid() };
        }

        [method: HttpPost]
        [ActionName("GetBalances")]
        IEnumerable<BalanceObject> GetBalances(Guid id)
        {
            var bank = new Usaa();
            return bank.GetBalances();
        } 
    }
}
