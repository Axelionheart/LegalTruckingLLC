using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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

        internal Guid RequestService(String byWho, String theirPwd)
        {
            GoToHomepage();
            Login(byWho, theirPwd);
            _webDriver.FindElement(By.Name("services")).SendKeys("Service Request" + Keys.Enter);
            var autoOptions = _webDriver.FindElement(By.Id("search-services"));
            autoOptions.SendKeys("2290");

            var optionsToSelect = _webDriver.FindElements(By.XPath("//div[@class='sbqs_c']"));

            foreach(var option in optionsToSelect)
            {
                if (option.Text.Equals("2290 Intake Form"))
                {
                    option.Click();
                    break;
                }
            }

            _webDriver.FindElement(By.Id("CompanyName")).SendKeys("Big Trucks LLC");
            _webDriver.FindElement(By.Id("CustomerName")).SendKeys("Big Truck LLC");
            _webDriver.FindElement(By.Id("Address")).SendKeys("1844 16th Ave. S. Tampa, Fl. 22712");
            _webDriver.FindElement(By.Id("Phone")).SendKeys("999-999-9999");
            _webDriver.FindElement(By.Id("Email")).SendKeys("adrian@bigtrucks.com");
            _webDriver.FindElement(By.Id("EIN")).SendKeys("1233456789");
            _webDriver.FindElement(By.Id("TypeVehicle")).SendKeys("SEMI");
            _webDriver.FindElement(By.Id("VIN")).SendKeys("666666666666666666");

            _webDriver.FindElement(By.Id("SubmitButton")).Click();
            Wait();
            Guid serviceId = Guid.Parse(_webDriver.FindElement(By.Id("service_id")).Text);
            return serviceId;
        }

        internal void hasShownRequestedServiceStatusAsNew(Guid serviceId)
        {
            _webDriver.Navigate().GoToUrl("http://localhost:5000/services/search");
            Wait();
            _webDriver.FindElement(By.Id("search_box")).SendKeys(serviceId.ToString());
            SubmitForm();
            var status = _webDriver.FindElement(By.Id("status")).Text;
            Assert.Equal(status, "New");
        }

        private void SubmitForm()
        {
            _webDriver.FindElement(By.Id("SubmitButton")).Click();
        }

        private void Login(string user, string pwd)
        {
            var usernameField = _webDriver.FindElement(By.Id("UserName"));
            var passwordField = _webDriver.FindElement(By.Id("Password"));
            usernameField.SendKeys(user);
            passwordField.SendKeys(pwd);
            SubmitForm();
        }

        private void GoToHomepage()
        {
            _webDriver.Navigate().GoToUrl("http://localhost:5000");
            Wait();
        }

        private void Wait()
        {
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
    }
}