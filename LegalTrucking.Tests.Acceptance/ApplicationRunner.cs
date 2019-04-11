using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace LegalTrucking.Tests.Acceptance
{
    internal class ApplicationRunner
    {
        private IWebDriver _webDriver;
      
        public ApplicationRunner()
        {
            _webDriver = new ChromeDriver("D:/dev/selenium-drivers");
        }

        internal void LogIn(String user, String pwd)
        {
            GoToHomepage();
            Login(user, pwd);
        }

     
        internal void HasShownUserWelcome(String welcomeMsg)
        {
            var hasMsg = _webDriver.PageSource.Contains(welcomeMsg);
            Assert.True(hasMsg, "Page does not contain the welcome message");
        }

        internal void end()
        {
            _webDriver.Dispose();
        }

        internal void RequestService(String byWho, String theirPwd)
        {
            GoToHomepage();
            Login(byWho, theirPwd);
            
        }

        internal void hasShownRequestedServiceAsUnassigned(int serviceId)
        {
            throw new NotImplementedException();
        }

        private void Login(string user, string pwd)
        {
            var usernameField = _webDriver.FindElement(By.Id("UserName"));
            var passwordField = _webDriver.FindElement(By.Id("Password"));
            var submitBtn = _webDriver.FindElement(By.Id("SubmitButton"));

            usernameField.SendKeys(user);
            passwordField.SendKeys(pwd);
            submitBtn.Click();
        }

        private void GoToHomepage()
        {
            _webDriver.Navigate().GoToUrl("http://localhost:5000");
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

    }
}