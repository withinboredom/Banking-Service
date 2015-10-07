using System;
using KeyVault.Controllers;
using KeyVault.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeyVaultTests
{
    [TestClass]
    public class TestSecret
    {
        [TestMethod]
        public void TestCreateSecret()
        {
            var controller = new SecretController();
            var result = controller.CreateSecret("secretTest",
                new Secret() {ContentType = "string", Id = Guid.Empty, Value = "value"});

            Assert.AreEqual("secretTest", result.Name);
        }
    }
}
