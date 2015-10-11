using System;
using System.Configuration;
using System.Threading;
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
                Thread.Sleep(TimeSpan.FromSeconds(2));
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
        public void TestOverwriteSecret()
        {
            var manager = createManager();
            var test = manager.CreateSecret("overwrite", new Secret() {ContentType = "text", Value = "hellow"});
            var over = manager.CreateSecret("overwrite", new Secret(test));
            Assert.AreNotEqual(test.Version, over.Version, "Can't overwrite");
        }

        [TestMethod]
        public void TestAddVersion()
        {
            var manager = createManager();
            var beginning = manager.CreateSecret("test", new Secret() {ContentType = "text", Value = "hello"});
            var ending = manager.CreateSecret("test", new Secret() {ContentType = "text", Value = "wee"});
            Assert.AreEqual(beginning.Version + 1, ending.Version);
        }

        [TestMethod]
        public void TestGetSecret()
        {
            var manager = createManager();
            var beginning = manager.CreateSecret("get", new Secret() {ContentType = "text", Value = "one"});
            beginning.Value = "two";
            var ending = manager.CreateSecret("get", beginning);

            var first = manager.GetSecret("get", beginning.Version);
            var last = manager.GetSecret("get", null);

            Assert.AreEqual(beginning.Version, first.Version, "Get by version");
            Assert.AreEqual(ending.Version, last.Version, "Null version");
        }

        [TestMethod]
        public void TestGetNullSecret()
        {
            var manager = createManager();
            var invalid = manager.GetSecret("null");
            Assert.IsNull(invalid);
        }

        [TestMethod]
        public void TestDeleteSecret()
        {
            var manager = createManager();
            var beginning = manager.CreateSecret("get", new Secret() { ContentType = "text", Value = "one" });
            var success = manager.DeleteSecret(new Secret() { Name = "get" });
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void TestNonExistantDelete()
        {
            var manager = createManager();
            var success = manager.DeleteSecret(new Secret() {Name = "null"});
            Assert.IsTrue(success);
        }
    }
}
