using System;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

using LegalTrucking.IntakePlus.Core.Adapters.Exceptions;
using LegalTrucking.IntakePlus.Core.Domain.Authentication;
using LegalTrucking.IntakePlus.Core.Ports.Commands.Authentication;
using LegalTrucking.IntakePlus.Core.Ports.Handlers.Authentication;
using LegalTrucking.IntakePlus.Web.Ui.Membership.Data;

namespace LegalTrucking.IntakePlus.Web.Ui.Membership
{
    public class CosmosDBMembership : ICustomMembership
    {
        private IHttpContextAccessor _context;
        private IUserRepository _userRepository;
        private ISessionRepository _sessionRepository;

        public CosmosDBMembership(IHttpContextAccessor context, CustomMembershipOptions options, 
            IUserRepository userRepository,
            ISessionRepository sessionRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            Options = options;
        }

        public CustomMembershipOptions Options { get; private set; }

        public async Task<RegisterResult> RegisterAsync(string userName, string email, string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var userRequest = new CreateUserCommand(
                    username: userName,
                    email: email,
                    pwdHash: passwordHash
            );

            var user = new User();

                try
                {
                    var handler = new CreateUserCommandHandler(_userRepository);
                    userRequest = await handler.HandleAsync(userRequest);
                    user = handler.getUser();
                }
                catch (EntityAlreadyExistsException exc)
                {
                    //TODO reduce breadth of exception statement
                    return RegisterResult.GetFailed("Username is already in use");
                }

                await SignInAsync(user);

                return RegisterResult.GetSuccess();
            
        }

        public async Task<LoginResult> LoginAsync(string userName, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(userName);

            if (user == null)
            {
                return LoginResult.GetFailed();
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return LoginResult.GetFailed();
            }

            await SignInAsync(user);

            return LoginResult.GetSuccess();
        }

        public async Task<bool> ValidateLoginAsync(ClaimsPrincipal principal)
        {
            var sessionId = principal.FindFirstValue("sessionId");

            if (sessionId == null)
            {
                return false;
            }

            var session = await _sessionRepository.GetByIdAsync(new Guid(sessionId));

            if (session.IsLoggedOut())
            {
                return false;
            }

            return true;
        }

        public async Task LogoutAsync()
        {
            await _context.HttpContext.SignOutAsync();

            var sessionId = _context.HttpContext.User.FindFirstValue("sessionid");

            if (sessionId != null)
            {
                var cmd = new LogoutCommand(new Guid(sessionId));
                await new LogoutCommandHandler(this._sessionRepository).HandleAsync(cmd);
            }
        }

        private async Task SignInAsync(User user)
        {        
            var command = new CreateSessionCommand(user.Id);
            command = await new CreateSessionCommandHandler(this._sessionRepository).HandleAsync(command);

            var identity = new ClaimsIdentity(Options.AuthenticationType);
            identity.AddClaim(new Claim("sessionId", command.Id.ToString()));
            identity.AddClaim(new Claim("name", (user.Username).ToString()));
            await _context.HttpContext.SignInAsync(new ClaimsPrincipal(identity));
        }
    }
}
