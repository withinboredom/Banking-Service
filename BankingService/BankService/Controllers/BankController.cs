using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLibrary.DataConstructs;
using InfrastructureLibrary;
using InfrastructureLibrary.Banks;

namespace BankingService.Controllers
{
    /// <summary>
    /// Access to a banking institution
    /// </summary>
    [RoutePrefix("bank")]
    public class BankController : ApiController
    {
        private BankInformation getUSAA()
        {
            
            var bank = new BankInformation()
            {
                Canonical = "USAA",
                Homepage = new Uri("http://usaa.com"),
                Id = Guid.Parse("3dd8f83d-d338-4157-a1b0-9d39a443796f"),
                Logo = new Uri("http://hahah.com"),
                Steps = new List<StepDefinition>()
            };

            var user = new StepDefinition() { Data = "User Name", Field = "UserName" };
            var pass = new StepDefinition() { Data = "Password", Field = "Password" };
            var pin = new StepDefinition() { Data = "Pin", Field = "Pin" };
            var q = new StepDefinition() { Data = "What is your mother's maiden name?", Field = "Question" };

            bank.Steps.Add(user);
            bank.Steps.Add(pass);
            bank.Steps.Add(pin);
            bank.Steps.Add(q);

            return bank;
        }

        /// <summary>
        /// Gets a list of all supported banking institutions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IEnumerable<BankInformation> Get()
        {
            var supported = new List<BankInformation> { getUSAA() };

            return supported;
        }

        /// <summary>
        /// Gets information about a specific banking institution
        /// </summary>
        /// <param name="id">The name of the institution to get more information about</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}")]
        public BankInformation GetById(Guid id)
        {
            return getUSAA();
        }

        /// <summary>
        /// Kicks off a login process, returns information for the next step
        /// </summary>
        /// <param name="id">The id of the bank</param>
        /// <param name="creds">Login credentials</param>
        /// <returns>The step id</returns>
        [HttpPost]
        [Route("{id:guid}/login")]
        public StepDefinition Login(Guid id, [FromBody] Credentials creds)
        {
            /*driver = new BankDriver(
                new USAA(
                    new ChromeDriver(
                        ChromeDriverService.CreateDefaultService(AppDomain.CurrentDomain.RelativeSearchPath + "/binaries"), new ChromeOptions(), TimeSpan.FromSeconds(30))));
            //var r = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), new DesiredCapabilities("PhantomJs", "", Platform.CurrentPlatform));
            //driver = new BankDriver(
            //    new USAA(
            //        r
            //    ));
            return driver.Login(creds);*/
            BankDriver driver = new BankDriver();
            return driver.Login(creds, id) as StepDefinition;
        }

        /// <summary>
        /// Performs the next step in a multi-factor login
        /// </summary>
        /// <param name="id">The banking institution</param>
        /// <param name="stepId">The step id we are performing</param>
        /// <param name="creds">The credentials for the next step</param>
        /// <returns>The step id</returns>
        [HttpPost]
        [Route("{id:guid}/login/step/{stepId:guid}")]
        public StepDefinition Step(Guid id, Guid stepId, [FromBody] Credentials creds)
        {
            //return driver.DoStep(stepId, creds);
            return new StepDefinition();
        }

        /// <summary>
        /// Gets information about a given step
        /// </summary>
        /// <param name="id">The id of the banking institution</param>
        /// <param name="stepId">The step id to get more information about</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}/login/step/{stepId:guid}")]
        public StepDefinition GetStep(Guid id, Guid stepId)
        {
            //return driver.Steps[stepId];
            return new StepDefinition();
        }
    }
}
