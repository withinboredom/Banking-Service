using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretsLibrary;

namespace KeyVaultTests
{
    [TestClass]
    public class TestSecret
    {
        [TestMethod]
        public void TestCreateSecret()
        {
            var manager = new SecretManager("nothing");
        }
    }
}
