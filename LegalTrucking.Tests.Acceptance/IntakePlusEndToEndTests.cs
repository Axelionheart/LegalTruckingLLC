using System;
using Xunit;

namespace LegalTrucking.Tests.Acceptance
{
    public class IntakePlusEndToEndTests : IDisposable
    {

        private IntakePlusBackEndServer _server;
        private ApplicationRunner _application;

        public IntakePlusEndToEndTests()
        {
            _server = new IntakePlusBackEndServer();
            _application = new ApplicationRunner();
        }

        [Fact(DisplayName = "Displays Welcome Message On Login")]
        public void displaysWelcomeWhenUserLogsIn()
        {
            _server.StartServingApplication();
            _application.LogIn();
            _application.HasShownUserWelcome();
            _server.StopServingApplication();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
