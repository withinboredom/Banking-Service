using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace BankingService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "binaries\\selenium-server-standalone-2.47.1.jar");
            var process = new ProcessStartInfo("java.exe", "-Dmyprocessname=selenium -jar " + path)
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };

            Process proc;
            proc = Process.Start(process);

        }
    }
}
