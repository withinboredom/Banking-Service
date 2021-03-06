﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyVaultClient;
using Microsoft.Azure;
using Microsoft.Azure.WebJobs;
using System.Net.Http;
using KeyVaultClient.Models;

namespace BootStrap
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var client = new KeyVault(new Uri(CloudConfigurationManager.GetSetting("KeyVault")));

            var jobStorage = client.Secret.GetSecretByName("JobStorage");
            var sb = client.Secret.GetSecretByName("ServiceBus");

            if (jobStorage.Value != CloudConfigurationManager.GetSetting("JobStorage"))
            {
                jobStorage = new Secret()
                {
                    ContentType = "String",
                    Name = "JobStorage",
                    Value = CloudConfigurationManager.GetSetting("JobStorage")
                };

                jobStorage = client.Secret.CreateSecret("JobStorage", jobStorage);
            }

            if (sb.Value != CloudConfigurationManager.GetSetting("ServiceBus"))
            {
                sb = new Secret()
                {
                    ContentType = "String",
                    Name = "ServiceBus",
                    Value = CloudConfigurationManager.GetSetting("ServiceBus")
                };

                sb = client.Secret.CreateSecret("ServiceBus", sb);
            }

            var jobStorageConnString = jobStorage.Value;

            var host = new JobHost(new JobHostConfiguration(jobStorageConnString));
            
            host.Start();

            Console.WriteLine("Bootstrapped");

            host.Stop();
        }
    }
}
