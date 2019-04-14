using System;
using Xunit;

namespace LegalTrucking.Tests.Acceptance
{
    public class IntakePlusEndToEndTests 
    {

        private IntakePlusBackEndServer _server;
        private ApplicationRunner _application;

        private readonly String USERNAME = "atillman@gmail.com";
        private readonly String PASSWORD = "testing123";
        private readonly String WELCOME_MSG = "Welcome, Adrian";

        public IntakePlusEndToEndTests()
        {
            _server = new IntakePlusBackEndServer();
            _application = new ApplicationRunner();
        }

        [Fact(DisplayName = "Displays Welcome Message On Login")]
        public void DisplaysWelcomeWhenUserLogsIn()
        {
            _server.StartServingApplication();
            _application.LogIn(USERNAME, PASSWORD);
            _application.HasShownUserWelcome(WELCOME_MSG);
            _application.end();
            _server.StopServingApplication();
        }

        [Fact(DisplayName = "Create New Service Request")]
        public void CreateNewServiceRequest()
        {
            _server.StartServingApplication();
            _application.LogIn(USERNAME, PASSWORD);
            _application.RequestService(USERNAME, PASSWORD);
            _application.hasShownRequestedServiceStatusAsNew(1);
            _application.end();
            _server.StartServingApplication();
        }
    }
}
