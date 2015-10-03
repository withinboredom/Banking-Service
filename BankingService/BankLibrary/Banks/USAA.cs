using System;
using System.Threading;
using BankLibrary.DataConstructs;
using OpenQA.Selenium;

namespace BankLibrary.Banks
{
    /// <summary>
    /// A interface to USAA
    /// </summary>
    public class USAA : Bank
    {
        public USAA(IWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Logs into the bank with the current credentials
        /// </summary>
        /// <param name="creds">The credentials to use</param>
        /// <param name="state">The state to load from, null to start at the beginning</param>
        public override void Login(Credentials creds, PageState state = null)
        {
            try
            {
                var step = LoadState(state);

                switch (step)
                {
                    case 0:
                        Driver.Navigate().GoToUrl("https://www.usaa.com");

                        if (string.IsNullOrEmpty(creds.UserName))
                        {
                            RequireUserName?.Invoke();
                            SaveState(1);
                            break;
                        }
                        goto case 1;
                    case 1:

                        if (string.IsNullOrEmpty(creds.Password))
                        {
                            RequirePassword?.Invoke();
                            SaveState(2);
                            break;
                        }
                        goto case 2;
                    case 2:
                        var user = Driver.FindElement(By.Id("usaaNum"));
                        var pass = Driver.FindElement(By.Id("usaaPass"));
                        var login = Driver.FindElement(By.Id("login"));

                        user.SendKeys(creds.UserName);
                        pass.SendKeys(creds.Password);
                        login.Click();

                        if (string.IsNullOrEmpty(creds.Pin))
                        {
                            RequirePin?.Invoke();
                            SaveState(3);
                            break;
                        }
                        goto case 3;
                    case 3:
                        var pin = Driver.FindElement(By.Id("id3"));
                        var next = Driver.FindElement(By.Id("idf"));
                        pin.SendKeys(creds.Pin);
                        next.Click();
                        var question = Driver.FindElement(By.CssSelector("label[for='id3']")).Text;

                        while (question.ToLower() == "pin")
                        {
                            Thread.Sleep(100);
                            question = Driver.FindElement(By.CssSelector("label[for='id3']")).Text;
                        }

                        if (string.IsNullOrEmpty(creds.Question))
                        {
                            RequireQuestion?.Invoke(question);
                            SaveState(4);
                            break;
                        }
                        goto case 4;
                    case 4:
                        var answer = Driver.FindElement(By.Id("id3"));
                        var submit = Driver.FindElement(By.Id("id10"));
                        answer.SendKeys(creds.Question);
                        submit.Click();
                        goto case 5;
                    case 5:
                        LoginComplete?.Invoke();
                        Driver.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                LoginFailed?.Invoke();
            }
        }
    }
}