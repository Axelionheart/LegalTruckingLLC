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
        private readonly Browser _browser;

        public ApplicationRunner()
        {
            _browser = new Browser(new ChromeDriver("D:/dev"));
        }

        public Browser Browser
        {
            get { return _browser; }
        }

        public void Login(String username, String pwd)
        {
               Browser.Login(username, pwd);
        }

        public void End()
        {
            Browser.Close();
        }

        internal void HasShownUserWelcome(String welcomeMsg)
        {
            var hasMsg = Browser.PageContains(welcomeMsg);
            Assert.True(hasMsg, "Page does not contain the welcome message");
        }

        internal Guid RequestService(String byWho, String theirPwd)
        {
            Browser.CompleteServiceRequest();
            Browser.Wait();
            Guid serviceId = Guid.Parse(Browser.GetElementText("service_id"));
            return serviceId;
        }

        internal void RequestToCompleteService(Guid id)
        {
            Browser.GoToURL("http://localhost:5000/tasks");
            
        }

        internal void HasShownErrorMsg(string errMsg)
        {
            var hasMsg = Browser.PageContains(errMsg);
            Assert.True(hasMsg, "Page does not contain the welcome message");
        }

        internal void CreateLogin(string userName, string email, string pwd)
        {
            Browser.GoToURL("http://localhost:5000/Account/Register");
            Browser.Wait();
            var usernameField = Browser.GetElement("UserName");
            var emailField = Browser.GetElement("Email");
            var passwordField = Browser.GetElement("Password");

            usernameField.SendKeys(userName);
            emailField.SendKeys(email);
            passwordField.SendKeys(pwd);

            Browser.SubmitForm();

        }

        public void HasShownStatusComplete(Guid id)
        {
            throw new NotImplementedException();
        }
        internal void hasShownRequestedServiceStatusAsNew(Guid serviceId)
        {
            Browser.GoToURL("http://localhost:5000/services/search");
            Browser.Wait();
            Browser.SearchBoxSearchFor(serviceId.ToString());
            Browser.SubmitForm();
            var status = Browser.GetElementText("status");
            Assert.Equal(status, "New");
        }

      
    }
}