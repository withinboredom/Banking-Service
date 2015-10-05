using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLibrary.DataConstructs;
using Interfaces;
using Microsoft.Azure;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace InfrastructureLibrary.Banks
{
    /// <summary>
    /// Infrastructure bank driver
    /// </summary>
    public class BankDriver : IBankDriver
    {
        /// <summary>
        /// Queues a login command with a set of credentials
        /// </summary>
        /// <param name="creds">The credentials to login with</param>
        /// <param name="bankId">The bank id to use</param>
        /// <returns>A step in progress</returns>
        public IStepDefinition Login(ICredentials creds, Guid bankId)
        {
            var queue = "bank_login_queue"; //todo: Make this an app setting
            Infrastrucure.CreateQueue(queue);

            var client = Infrastrucure.GetQueueClient(queue);

            creds.BankId = bankId;

            var step = new StepDefinition()
            {
                Id = Guid.NewGuid(),
                Successful = false,
                Field = "",
                Data = "",
                NextStep = null
            };

            creds.StepId = step.Id;

            var message = new BrokeredMessage(creds);
            client.Send(message);

            return step;
        }

        public IStepDefinition DoStep(Guid step, ICredentials creds)
        {
            return new StepDefinition();
        }

        public void Dispose()
        {
            
        }
    }
}
