using System;
using DataLibrary.DataConstructs;
using OpenQA.Selenium;

namespace BankLibrary.Banks
{
    public abstract class Bank : IDisposable
    {
        protected IWebDriver Driver;

        protected Bank(IWebDriver driver)
        {
            this.Driver = driver;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Indicates that a username is required
        /// </summary>
        public virtual event Action RequireUserName;

        /// <summary>
        /// Indicates a password is required
        /// </summary>
        public virtual event Action RequirePassword;

        /// <summary>
        /// Indicates that a pin is required
        /// </summary>
        public virtual event Action RequirePin;

        /// <summary>
        /// Indicates a login was successfully made
        /// </summary>
        public virtual event Action LoginComplete;

        /// <summary>
        /// Indicates an invalid login
        /// </summary>
        public virtual event Action LoginFailed;

        /// <summary>
        /// Indicates that a question is required
        /// </summary>
        public virtual event Action<string> RequireQuestion;

        /// <summary>
        /// Saves the current browser state for loading in the future
        /// </summary>
        /// <param name="step">The current step we are on when we save state</param>
        /// <returns>The page state</returns>
        public PageState SaveState(int step)
        {
            var cookies = Driver.Manage().Cookies.AllCookies;
            var page = Driver.Url;

            var state = new PageState() {Cookies = cookies, Url = page, Step = step};
            return state;
        }

        /// <summary>
        /// Loads a page state
        /// </summary>
        /// <param name="state">The state to load</param>
        /// <returns>The current step we are on</returns>
        public int LoadState(PageState state)
        {
            Driver.Manage().Cookies.DeleteAllCookies();

            if (state == null)
            {
                return 0;
            }

            foreach (var cookie in state.Cookies)
            {
                Driver.Manage().Cookies.AddCookie(cookie);
            }
            
            Driver.Navigate().GoToUrl(state.Url);

            return state.Step;
        }

        /// <summary>
        /// Logs into the bank with the current credentials
        /// </summary>
        /// <param name="creds">The credentials to use</param>
        /// <param name="state">The state to load from, null to start at the beginning</param>
        public abstract void Login(Credentials creds, PageState state = null);

        public void Dispose()
        {
            Driver.Dispose();
        }
    }
}