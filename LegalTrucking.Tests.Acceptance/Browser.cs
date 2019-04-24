using System;
using OpenQA.Selenium;

namespace LegalTrucking.Tests.Acceptance
{
    internal class Browser
    {
        private IWebDriver _webDriver;

        public Browser(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public String GetElementText(String element)
        {
            return _webDriver.FindElement(By.Id("element")).Text;
        }

        public bool PageContains(String text)
        {
             return _webDriver.PageSource.Contains(text);
        }

        public void Close()
        {
            _webDriver.Quit();
        }

        public void SubmitForm()
        {
            _webDriver.FindElement(By.Id("SubmitButton")).Click();
        }

        public void Login(string user, string pwd)
        {
            GoToHomepage();
            Wait();
            var usernameField = _webDriver.FindElement(By.Id("UserName"));
            var passwordField = _webDriver.FindElement(By.Id("Password"));
            usernameField.SendKeys(user);
            passwordField.SendKeys(pwd);
            SubmitForm();
        }

        public void GoToHomepage()
        {
            _webDriver.Navigate().GoToUrl("http://localhost:5000");
            Wait();
        }

        public void GoToURL(String url)
        {
            _webDriver.Navigate().GoToUrl(url);
        }

        public void SearchBoxSearchFor(String searchTxt)
        {
            _webDriver.FindElement(By.Id("search_box")).SendKeys(searchTxt);
        }

        public void Wait()
        {
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public void CompleteServiceRequest()
        {
            _webDriver.FindElement(By.Name("services")).SendKeys("Service Request" + Keys.Enter);
            var autoOptions = _webDriver.FindElement(By.Id("search-services"));
            autoOptions.SendKeys("2290");

            var optionsToSelect = _webDriver.FindElements(By.XPath("//div[@class='sbqs_c']"));

            foreach (var option in optionsToSelect)
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
        }

        public IWebElement GetElement(String elementId)
        {
            return _webDriver.FindElement(By.Id(elementId));
        }
    }
}