using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;

namespace Banker
{
    public class Functions
    {
        public static void ProcessLogin([ServiceBusTrigger("bank_login_queue")] BrokeredMessage message,
            TextWriter logger)
        {
            logger.WriteLine("Got message!");
        }

        public static void ProcessLoginDev([ServiceBusTrigger("bank_login_queue_dev")] BrokeredMessage message,
            TextWriter logger)
        {
            logger.WriteLine("Got dev message!");
        }
    }
}
