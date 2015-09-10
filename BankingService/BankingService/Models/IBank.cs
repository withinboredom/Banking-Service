using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BankingService.Models
{
    /// <summary>
    /// A enumeration of possible states for a given step
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LoginStepStatus
    {
        Unkown,
        Pending,
        InProgress,
        Ready,
        Complete
    }

    public struct Money
    {
        private int actualValue;

        public decimal dValue => actualValue / 100M;

        public int iValue
        {
            get { return actualValue; }
            set { actualValue = value; }
        }

        public string currency { get; }

        public override bool Equals(object obj)
        {
            if (obj is Money)
            {
                var m = (Money) obj;

                return (m.iValue == this.iValue && m.currency == this.currency);
            }

            return base.Equals(obj);
        }

        public override string ToString() => $"{currency}{dValue:C}";

        public Money(int value, string currency = "$")
        {
            actualValue = value;
            this.currency = currency;
        }

        public Money(Money mon)
        {
            this.currency = mon.currency;
            this.actualValue = mon.iValue;
        }
    }

    public struct BalanceObject
    {
        public Money currentBalance { get; }
        public Money availableBalance { get; }
        public string accountNumber { get; }

        public BalanceObject(Money currentBal, Money availMoney, string acct)
        {
            this.currentBalance = currentBal;
            this.availableBalance = availMoney;
            this.accountNumber = acct;
        }

        public BalanceObject(BalanceObject bal)
        {
            this.accountNumber = bal.accountNumber;
            this.availableBalance = bal.availableBalance;
            this.currentBalance = bal.currentBalance;
        }
    }

    /// <summary>
    /// Guaranteed bank information
    /// </summary>
    public interface IBank
    {
        #region Administrative Data
        /// <summary>
        /// Official display name of the bank
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The official address of the bank
        /// </summary>
        string Address { get; }

        #endregion

        #region Login Flow

        /// <summary>
        /// Initiates a login flow to a bank
        /// </summary>
        /// <param name="username">The user name to use</param>
        /// <param name="password">The password to use</param>
        /// <returns>The flow id to use</returns>
        IFlow BeginLoginFlow(string username, string password);

        /// <summary>
        /// Gets the realtime status of a login flow
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        LoginStepStatus GetRealtimeFlowStatus(IFlow query);

        /// <summary>
        /// Get the next step to a flow
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<IStep> GetFlowResult(IFlow query);

        /// <summary>
        /// Respond to a set of challenges
        /// </summary>
        /// <param name="responses">The response to the challenges</param>
        /// <returns>The flow for this set of responses</returns>
        IFlow IssueResponseToChallenge(IEnumerable<IStep> responses);

        #endregion

        /// <summary>
        /// Gets a list of balances
        /// </summary>
        /// <returns>The balance</returns>
        IEnumerable<BalanceObject> GetBalances();

        /// <summary>
        /// Gets the given account balance
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        BalanceObject GetBalance(string account);
    }
}