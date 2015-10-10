using System.Diagnostics;
using System.Linq;

namespace Testing
{
    // Start/stop azure storage emulator from code:
    // http://stackoverflow.com/questions/7547567/how-to-start-azure-storage-emulator-from-within-a-program
    // Credits to David Peden http://stackoverflow.com/users/607701/david-peden for sharing this!
    public static class AzureStorageEmulatorManager
    {
        private const string WindowsAzureStorageEmulatorPath = @"C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator\WAStorageEmulator.exe";
        private const string Win7ProcessName = "WAStorageEmulator";
        private const string Win8ProcessName = "WASTOR~1";

        private static readonly ProcessStartInfo startStorageEmulator = new ProcessStartInfo
        {
            FileName = WindowsAzureStorageEmulatorPath,
            Arguments = "start",
        };

        private static readonly ProcessStartInfo stopStorageEmulator = new ProcessStartInfo
        {
            FileName = WindowsAzureStorageEmulatorPath,
            Arguments = "stop",
        };

        private static Process GetProcess()
        {
            return Process.GetProcessesByName(Win7ProcessName).FirstOrDefault() ?? Process.GetProcessesByName(Win8ProcessName).FirstOrDefault();
        }

        public static bool IsProcessStarted()
        {
            return GetProcess() != null;
        }

        public static void StartStorageEmulator()
        {
            if (!IsProcessStarted())
            {
                using (Process process = Process.Start(startStorageEmulator))
                {
                    process.WaitForExit();
                }
            }
        }

        public static void StopStorageEmulator()
        {
            using (Process process = Process.Start(stopStorageEmulator))
            {
                process.WaitForExit();
            }
        }
    }
}