using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace InfrastructureLibrary
{
    public static class Infrastrucure
    {
        #region queues

        /// <summary>
        /// A cached queue connection string
        /// </summary>
        private static string _queueConnectionString;

        /// <summary>
        /// Get the connection string for queues
        /// </summary>
        /// <returns>The connection string</returns>
        private static string GetQueueConnectionString()
        {
            return _queueConnectionString ??
                   (_queueConnectionString = CloudConfigurationManager.GetSetting("AzureWebJobsServiceBus"));
        }

        /// <summary>
        /// Get the namespace manager
        /// </summary>
        /// <returns>The namespsace manager</returns>
        public static NamespaceManager GetQueueNamespaceManager()
        {
            return NamespaceManager.CreateFromConnectionString(GetQueueConnectionString());
        }

        /// <summary>
        /// Create a queue
        /// </summary>
        /// <param name="queueName">The queue to create</param>
        /// <param name="description">The description of the queue</param>
        /// <returns>true if the queue was created</returns>
        public static bool CreateQueue(string queueName, QueueDescription description = null)
        {
            var namespaceManager = GetQueueNamespaceManager();
#if DEBUG
            queueName += "_dev";
#endif
            if (description == null)
            {
                description = new QueueDescription(queueName)
                {
                    LockDuration = TimeSpan.FromMinutes(3)
                };
            }

            if (namespaceManager.QueueExists(queueName)) return false;

            namespaceManager.CreateQueue(description);

            return true;
        }

        /// <summary>
        /// Creates a queue client
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public static QueueClient GetQueueClient(string queueName)
        {
            return QueueClient.CreateFromConnectionString(GetQueueConnectionString(), queueName);
        }

        #endregion

        #region storage
        #endregion
    }
}
