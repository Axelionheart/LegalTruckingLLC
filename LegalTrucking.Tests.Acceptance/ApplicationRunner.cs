using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LegalTrucking.Tests.Acceptance
{
    internal class ApplicationRunner
    {
        private IWebDriver _webDriver;

        public ApplicationRunner()
        {
            _webDriver = new ChromeDriver("D:\\dev\\selenium-drivers");
        }

        internal void LogIn()
        {
            _webDriver.Navigate().GoToUrl("https://localhost:44398/Identity/Account/Login");
            _webDriver.FindElement(By.Id("temp"));
        }

        internal void HasShownUserWelcome()
        {
            throw new NotImplementedException();
        }
    }
}