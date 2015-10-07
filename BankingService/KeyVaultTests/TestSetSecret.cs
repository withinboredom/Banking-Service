using System;
using KeyVault.Controllers;
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
        }
    }
}
