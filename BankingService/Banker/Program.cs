﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace Banker
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            try
            {
                var host = new JobHost();
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
