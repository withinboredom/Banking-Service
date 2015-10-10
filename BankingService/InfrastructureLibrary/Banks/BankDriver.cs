using System;
using DataLibrary.DataConstructs;
using Interfaces;
using Interfaces.Banking;
using Microsoft.ServiceBus.Messaging;

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

            Infrastrucure.SendBackedMessage(queue, new BrokeredMessage(creds), "steps", step);
            
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
