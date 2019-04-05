using CosmosDBRepository;
using CosmosDBRepository.Users;
using LegalTrucking.IntakePlus.Web.Ui.Membership.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Web.Ui.Membership
{
    public class CosmosDBMembership : ICustomMembership
    {
        private IHttpContextAccessor _context;
        private Persistence _persistence;

        public CosmosDBMembership(IHttpContextAccessor context, CustomMembershipOptions options, Persistence persistence)
        {
            _context = context;
            _persistence = persistence;
            Options = options;
        }

        public CustomMembershipOptions Options { get; private set; }

        public async Task<RegisterResult> RegisterAsync(string userName, string email, string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new LoginUser()
            {
                Username = userName,
                Email = email,
                PasswordHash = passwordHash
            };

            try
            {
                user = await _persistence.Users.CreateUserAsync(user);
            }
            catch (Exception exc)
            {
                //TODO reduce breadth of exception statement
                return RegisterResult.GetFailed("Username is already in use");
            }

            await SignInAsync(user);

            return RegisterResult.GetSuccess();
        }

        public async Task<LoginResult> LoginAsync(string userName, string password)
        {
            var user = await _persistence.Users.GetUserByUsernameAsync(userName);

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

            var session = await _persistence.Users.GetSessionAsync(sessionId);
            if (session.LogoutTime.HasValue)
            {
                return false;
            }

            return true;
        }

        public async Task LogoutAsync()
        {
            await _context.HttpContext.SignOutAsync();

            var sessionId = _context.HttpContext.User.FindFirstValue("sessionId");
            if (sessionId != null)
            {
                var session = await _persistence.Users.GetSessionAsync(sessionId);
                session.LogoutTime = DateTime.UtcNow;
                await _persistence.Users.UpdateSessionAsync(session);
            }
        }


        private async Task SignInAsync(LoginUser user)
        {
            // key the login to a server-side session id to make it easy to invalidate later
            var session = new LoginSession()
            {
                UserId = user.Id,
                CreationTime = DateTime.UtcNow
            };
            session = await _persistence.Users.CreateSessionAsync(session);

            var identity = new ClaimsIdentity(Options.AuthenticationType);
            identity.AddClaim(new Claim("sessionId", session.Id));
            await _context.HttpContext.SignInAsync(new ClaimsPrincipal(identity));
        }
    }
}
