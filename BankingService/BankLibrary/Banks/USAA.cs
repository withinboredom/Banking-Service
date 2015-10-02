using System;
using System.Threading;
using BankLibrary.DataConstructs;
using OpenQA.Selenium;

namespace BankLibrary.Banks
{
    /// <summary>
    /// A interface to USAA
    /// </summary>
    public class USAA
    {
        private readonly IWebDriver _driver;

        /// <summary>
        /// Indicates that a username is required
        /// </summary>
        public event Action RequireUserName;

        /// <summary>
        /// Indicates a password is required
        /// </summary>
        public event Action RequirePassword;

        /// <summary>
        /// Indicates that a pin is required
        /// </summary>
        public event Action RequirePin;

        /// <summary>
        /// Indicates a login was successfully made
        /// </summary>
        public event Action LoginComplete;

        /// <summary>
        /// Indicates an invalid login
        /// </summary>
        public event Action LoginFailed;

        /// <summary>
        /// Indicates that a question is required
        /// </summary>
        public event Action<string> RequireQuestion;

        public USAA(IWebDriver driver)
        {
            this._driver = driver;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Saves the current browser state for loading in the future
        /// </summary>
        /// <param name="step">The current step we are on when we save state</param>
        /// <returns>The page state</returns>
        public PageState SaveState(int step)
        {
            var cookies = _driver.Manage().Cookies.AllCookies;
            var page = _driver.Url;

            var state = new PageState() {cookies = cookies, Url = page, Step = step};
            return state;
        }

        /// <summary>
        /// Loads a page state
        /// </summary>
        /// <param name="state">The state to load</param>
        /// <returns>The current step we are on</returns>
        public int LoadState(PageState state)
        {
            _driver.Manage().Cookies.DeleteAllCookies();

            if (state == null)
            {
                return 0;
            }

            foreach (var cookie in state.cookies)
            {
                _driver.Manage().Cookies.AddCookie(cookie);
            }
            
            _driver.Navigate().GoToUrl(state.Url);

            return state.Step;
        }

        /// <summary>
        /// Logs into the bank with the current credentials
        /// </summary>
        /// <param name="creds">The credentials to use</param>
        /// <param name="state">The state to load from, null to start at the beginning</param>
        public void Login(Credentials creds, PageState state = null)
        {
            try
            {
                var step = LoadState(state);

                switch (step)
                {
                    case 0:
                        _driver.Navigate().GoToUrl("https://www.usaa.com");

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
                        var user = _driver.FindElement(By.Id("usaaNum"));
                        var pass = _driver.FindElement(By.Id("usaaPass"));
                        var login = _driver.FindElement(By.Id("login"));

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
                        var pin = _driver.FindElement(By.Id("id3"));
                        var next = _driver.FindElement(By.Id("idf"));
                        pin.SendKeys(creds.Pin);
                        next.Click();
                        var question = _driver.FindElement(By.CssSelector("label[for='id3']")).Text;

                        while (question.ToLower() == "pin")
                        {
                            Thread.Sleep(100);
                            question = _driver.FindElement(By.CssSelector("label[for='id3']")).Text;
                        }

                        if (string.IsNullOrEmpty(creds.Question))
                        {
                            RequireQuestion?.Invoke(question);
                            SaveState(4);
                            break;
                        }
                        goto case 4;
                    case 4:
                        var answer = _driver.FindElement(By.Id("id3"));
                        var submit = _driver.FindElement(By.Id("id10"));
                        answer.SendKeys(creds.Question);
                        submit.Click();
                        goto case 5;
                    case 5:
                        LoginComplete?.Invoke();
                        _driver.Close();
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