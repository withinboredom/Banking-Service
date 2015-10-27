using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KeyVaultClient;
using Microsoft.Azure;
using Microsoft.Azure.WebJobs;

namespace Selenium
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program : IDisposable
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

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds((1 - (retry/10F)*10F)));
                retry = retry - 1;
            }
        }

        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;

            var jobStorageSecret = GetStorage(10);

            try
            {
                var host = new JobHost(new JobHostConfiguration(jobStorageSecret));
                // The following code ensures that the WebJob will be running continuously
                host.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect to storage --- assuming development?");
                Console.WriteLine(ex.Message);
            }

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "binaries\\selenium-server-standalone-2.47.1.jar");
            var process = new ProcessStartInfo("D:\\Program Files (x86)\\Java\\jdk1.8.0_25\\bin\\java.exe", "-Djava.net.preferIPv4Stack=true -jar " + path + " -role hub")
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };

            var local = new ProcessStartInfo("java.exe", "-jar " + path)
            {
                UseShellExecute = false,
                CreateNoWindow = false
            };

            try
            {
                var proc = Process.Start(process);
                wait(proc);
            }
            catch
            {
                try
                {
                    var dunc = Process.Start(local);
                    wait(dunc);
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to start selenium" + e.Message);
                }
            }
        }

        private static void CurrentDomainOnProcessExit(object sender, EventArgs eventArgs)
        {
            proc_running.Kill();
        }

        private static Process proc_running;

        static void wait(Process proc)
        {
            proc_running = proc;

            if (proc != null)
            {
                proc.WaitForExit();
            }
            else
            {
                throw new Exception("Failed to start selenium");
            }

            Environment.Exit(1);
        }

        public void Dispose()
        {
            proc_running.Kill();
        }
    }
}
