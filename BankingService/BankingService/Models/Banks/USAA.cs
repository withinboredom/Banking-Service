using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using BankingService.Models.DataConstructs;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace BankingService.Models.Banks
{
    public class USAA
    {
        private IWebDriver driver;

        public event Action RequireUserName;
        public event Action RequirePassword;
        public event Action RequirePin;
        public event Action LoginComplete;
        public event Action LoginFailed;
        public event Action<string> RequireQuestion;

        public USAA(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        public PageState saveState(int step)
        {
            var cookies = driver.Manage().Cookies.AllCookies;
            var page = driver.Url;

            var state = new PageState() {cookies = cookies, Url = page, Step = step};
            return state;
        }

        public int LoadState(PageState state)
        {
            driver.Manage().Cookies.DeleteAllCookies();

            if (state == null)
            {
                return 0;
            }

            foreach (var cookie in state.cookies)
            {
                driver.Manage().Cookies.AddCookie(cookie);
            }
            
            driver.Navigate().GoToUrl(state.Url);

            return state.Step;
        }

        public void Login(Credentials creds, PageState state = null)
        {
            try
            {
                var step = LoadState(state);

                switch (step)
                {
                    case 0:
                        driver.Navigate().GoToUrl("https://www.usaa.com");

                        if (string.IsNullOrEmpty(creds.UserName))
                        {
                            RequireUserName?.Invoke();
                            saveState(1);
                            break;
                        }
                        goto case 1;
                    case 1:

                        if (string.IsNullOrEmpty(creds.Password))
                        {
                            RequirePassword?.Invoke();
                            saveState(2);
                            break;
                        }
                        goto case 2;
                    case 2:
                        var user = driver.FindElement(By.Id("usaaNum"));
                        var pass = driver.FindElement(By.Id("usaaPass"));
                        var login = driver.FindElement(By.Id("login"));

                        user.SendKeys(creds.UserName);
                        pass.SendKeys(creds.Password);
                        login.Click();

                        if (string.IsNullOrEmpty(creds.Pin))
                        {
                            RequirePin?.Invoke();
                            saveState(3);
                            break;
                        }
                        goto case 3;
                    case 3:
                        var pin = driver.FindElement(By.Id("id3"));
                        var next = driver.FindElement(By.Id("idf"));
                        pin.SendKeys(creds.Pin);
                        next.Click();
                        var question = driver.FindElement(By.CssSelector("label[for='id3']")).Text;

                        while (question.ToLower() == "pin")
                        {
                            question = driver.FindElement(By.CssSelector("label[for='id3']")).Text;
                        }

                        if (string.IsNullOrEmpty(creds.Question))
                        {
                            RequireQuestion?.Invoke(question);
                            saveState(4);
                            break;
                        }
                        goto case 4;
                    case 4:
                        var answer = driver.FindElement(By.Id("id3"));
                        var submit = driver.FindElement(By.Id("id10"));
                        answer.SendKeys(creds.Question);
                        submit.Click();
                        goto case 5;
                    case 5:
                        LoginComplete?.Invoke();
                        driver.Close();
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