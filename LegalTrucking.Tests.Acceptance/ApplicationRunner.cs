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
            _webDriver = new ChromeDriver("C:/Users/yasel/dev");
        }

        internal void LogIn(String user, String pwd)
        {
            _webDriver.Navigate().GoToUrl("http://localhost:5000");
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var usernameField =_webDriver.FindElement(By.Id("UserName"));
            var passwordField = _webDriver.FindElement(By.Id("Password"));
            var submitBtn = _webDriver.FindElement(By.Id("SubmitButton"));

            usernameField.SendKeys(user);
            passwordField.SendKeys(pwd);
            submitBtn.Click();
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

    }
}