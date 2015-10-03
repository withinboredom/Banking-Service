using System;
using System.IO;
using BankLibrary.Banks;
using BankLibrary.DataConstructs;
using Interfaces;
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
            var creds = message.GetBody<Credentials>();
            DoLogin(creds, true);
        }

        private static void DoLogin(ICredentials creds, bool debug = false)
        {
            if (!creds.BankId.HasValue) throw new Exception("No bank id set");

            var driver = BankDriver.CreateDriver(debug);
            driver.Login(creds, creds.BankId.Value);
            driver.Dispose();
        }
    }
}
