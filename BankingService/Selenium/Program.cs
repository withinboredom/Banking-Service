using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace Selenium
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "binaries\\selenium-server-standalone-2.47.1.jar");
            var process = new ProcessStartInfo("D:\\Program Files (x86)\\Java\\jdk1.8.0_25\\bin\\java.exe", "-Djava.net.preferIPv4Stack=true -jar " + path)
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
            catch (Exception ex)
            {
                try
                {
                    var dunc = Process.Start(local);
                    wait(dunc);
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to start selenium");
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
                try
                {
                    var host = new JobHost();
                    // The following code ensures that the WebJob will be running continuously
                    host.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to connect to storage --- assuming development?");
                }
                proc.WaitForExit();
            }
            else
            {
                throw new Exception("Failed to start selenium");
            }

            Environment.Exit(1);
        }
    }
}
