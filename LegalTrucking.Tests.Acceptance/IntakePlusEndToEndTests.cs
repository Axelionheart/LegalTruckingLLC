using System;
using Xunit;

namespace LegalTrucking.Tests.Acceptance
{
    public class IntakePlusEndToEndTests : IDisposable
    {

        private IntakePlusBackEndServer _server;
        private ApplicationRunner _application;

        private readonly String USERNAME = "atillman@gmail.com";
        private readonly String PASSWORD = "testing123";

        public IntakePlusEndToEndTests()
        {
            _server = new IntakePlusBackEndServer();
            _application = new ApplicationRunner();
        }

        [Fact(DisplayName = "Displays Welcome Message On Login")]
        public void displaysWelcomeWhenUserLogsIn()
        {
            _server.StartServingApplication();
            _application.LogIn(USERNAME, PASSWORD);
            _application.HasShownUserWelcome();
            _server.StopServingApplication();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
