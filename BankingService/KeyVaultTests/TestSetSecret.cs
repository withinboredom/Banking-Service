using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretsLibrary;
using Testing;
using Utility;

namespace KeyVaultTests
{
    [TestClass]
    public class TestSecret
    {
        [TestInitialize]
        public void Startup()
        {
            if (!AzureStorageEmulatorManager.IsProcessStarted())
            {
                AzureStorageEmulatorManager.StartStorageEmulator();
            }
        }

        [TestCleanup]
        public void Shutdown()
        {
            if (AzureStorageEmulatorManager.IsProcessStarted())
            {
                AzureStorageEmulatorManager.StopStorageEmulator();
            }
        }

        private SecretManager createManager()
        {
            var reader = new AppSettingsReader();
            var setting = "Auth:Storage";

            var manager = new SecretManager(reader.GetValue(setting, setting.GetType()) as string, Cloud.GetCoud());
            return manager;
        }

        [TestMethod]
        public void TestCreateSecret()
        {
            var manager = createManager();
            var test = manager.CreateSecret("test", new Secret() {ContentType = "text", Value = "hello world"});
            Assert.AreEqual(test.Name, "test");
        }

        [TestMethod]
        public void TestAddVersion()
        {
            var manager = createManager();
            var beginning = manager.CreateSecret("test", new Secret() {ContentType = "text", Value = "hello"});
            var ending = manager.CreateSecret("test", new Secret() {ContentType = "text", Value = "wee"});
            Assert.AreEqual(beginning.Version + 1, ending.Version);
        }
    }
}
