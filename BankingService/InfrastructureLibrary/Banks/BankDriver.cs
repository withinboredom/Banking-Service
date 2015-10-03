using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLibrary.DataConstructs;
using Interfaces;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;

namespace InfrastructureLibrary.Banks
{
    class BankDriver : IBankDriver
    {
        public IStepDefinition Login(ICredentials creds, Guid bankId)
        {
            var connString = CloudConfigurationManager.GetSetting("AzureQueue");
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connString);
            var client = QueueClient.CreateFromConnectionString(connString);
            var queue = "bank_login_queue";
#if DEBUG
            queue += "_dev";
#endif
            if (!namespaceManager.QueueExists(queue))
            {
                namespaceManager.CreateQueue(new QueueDescription(queue) { LockDuration = TimeSpan.FromMinutes(3) });
            }

            var message = new BrokeredMessage(creds)
            {

            };

            return new StepDefinition();
        }

        public IStepDefinition DoStep(Guid step, ICredentials creds)
        {
            return new StepDefinition();
        }
    }
}
