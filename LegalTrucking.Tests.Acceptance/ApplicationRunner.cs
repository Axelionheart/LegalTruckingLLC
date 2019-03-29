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
            _webDriver = new ChromeDriver("D:\\dev\\selenium-drivers");
        }

        internal void LogIn(String user, String pwd)
        {
            _webDriver.Navigate().GoToUrl("http://localhost:5000");
            _webDriver.FindElement(By.LinkText("Login")).Click();
            var usernameField =_webDriver.FindElement(By.Id("Input_Email"));
            var passwordField = _webDriver.FindElement(By.Id("Input_Password"));
            var submitBtn = _webDriver.FindElement(By.XPath("//button[@type='submit'][text()='Log in']"));

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