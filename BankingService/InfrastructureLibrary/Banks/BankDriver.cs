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

namespace InfrastructureLibrary.Banks
{
    public class BankDriver : IBankDriver
    {
        public IStepDefinition Login(ICredentials creds, Guid bankId)
        {
            var connString = CloudConfigurationManager.GetSetting("AzureWebJobsServiceBus");
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connString);
            var queue = "bank_login_queue";
#if DEBUG
            queue += "_dev";
#endif
            if (!namespaceManager.QueueExists(queue))
            {
                namespaceManager.CreateQueue(new QueueDescription(queue) { LockDuration = TimeSpan.FromMinutes(3) });
            }

            var client = QueueClient.CreateFromConnectionString(connString, queue);

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
