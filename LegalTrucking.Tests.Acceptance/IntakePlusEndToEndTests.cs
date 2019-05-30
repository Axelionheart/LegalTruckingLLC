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
        private readonly String EXISTING_USER_ERROR_MSG = "Username is already in use";

        public IntakePlusEndToEndTests()
        {
            _server = new IntakePlusBackEndServer();
            _application = new ApplicationRunner();
        }

        [Fact(DisplayName = "Displays Welcome Message On Login")]
        public void DisplaysWelcomeWhenUserLogsIn()
        {
            try
            {
                _server.StartServingApplication();
                _application.Login(USERNAME, PASSWORD);
                _application.HasShownUserWelcome(WELCOME_MSG);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
            finally
            {
                _application.End();
                _server.StopServingApplication();
            }
        }

        [Fact(DisplayName = "Cannot create duplicate login")]
        public void CreateDuplicateLogin()
        {
            try
            {
                _server.StartServingApplication();
                _application.CreateLogin(USERNAME, USERNAME, PASSWORD);
                _application.HasShownErrorMsg(EXISTING_USER_ERROR_MSG);
            }
            catch (Exception ex) {
                Assert.True(false, ex.Message);
            }
            finally
            {
                _application.End();
                _server.StopServingApplication();
            }
        }

        [Fact(DisplayName = "Create New Service Request")]
        public void CreateNewServiceRequest()
        {
            try { 
            _server.StartServingApplication();
            _application.Login(USERNAME, PASSWORD);
            var id = _application.RequestService(USERNAME, PASSWORD);
            _application.hasShownRequestedServiceStatusAsNew(id);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
            finally
            {
                _application.End();
                _server.StopServingApplication();
            }
        }

        [Fact(DisplayName = "Complete Service Request")]
        public void CompleteServiceRequest()
        {
            try { 
            _server.StartServingApplication();
            _application.Login(USERNAME, PASSWORD);
            var id = _application.RequestService(USERNAME, PASSWORD);
            _application.hasShownRequestedServiceStatusAsNew(id);
            _application.RequestToCompleteService(id);
            _application.HasShownStatusComplete(id);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
            finally
            {
                _application.End();
                _server.StopServingApplication();
            }
        }
    }
}
