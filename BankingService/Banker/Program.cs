using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyVaultClient;
using Microsoft.Azure;
using Microsoft.Azure.WebJobs;

namespace Banker
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        private static string GetStorage(int retry)
        {
            var vault = new KeyVault(new Uri(CloudConfigurationManager.GetSetting("KeyVault")));
            while (true)
            {
                if (retry <= 0)
                {
                    return null;
                }

                var jobStorageSecret = vault.Secret.GetSecretByName("JobStorage");

                if (jobStorageSecret.Value != null) return jobStorageSecret.Value;

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds((1 - (retry / 10F) * 10F)));
                retry = retry - 1;
            }
        }

        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var jobStorageSecret = GetStorage(10);

            try
            {
                var host = new JobHost(new JobHostConfiguration(jobStorageSecret));
                // The following code ensures that the WebJob will be running continuously
                host.RunAndBlock();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect to storage, assuming development");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
