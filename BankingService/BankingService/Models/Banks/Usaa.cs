using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingService.Models.Banks
{
    public class Usaa : IBank
    {
        public string Name => "USAA";

        public string Address => "Somewhere";

        public IFlow BeginLoginFlow(string username, string password)
        {
            throw new NotImplementedException();
        }

        public LoginStepStatus GetRealtimeFlowStatus(IFlow query)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IStep> GetFlowResult(IFlow query)
        {
            throw new NotImplementedException();
        }

        public IFlow IssueResponseToChallenge(IEnumerable<IStep> responses)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BalanceObject> GetBalances()
        {
            var ret = new List<BalanceObject>();
            var bal = new BalanceObject(new Money(1200), new Money(1000), "duh");
            ret.Add(bal);
            return ret;
        }

        public BalanceObject GetBalance(string account)
        {
            throw new NotImplementedException();
        }
    }
}