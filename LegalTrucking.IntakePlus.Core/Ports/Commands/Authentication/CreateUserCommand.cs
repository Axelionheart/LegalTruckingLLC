using LegalTrucking.IntakePlus.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Ports.Commands.Authentication
{
    public class CreateUserCommand : Command
    {
        public CreateUserCommand(String username, String email, String pwdHash) 
            : base(Guid.NewGuid())
        {
            Username = username;
            Email = email;
            PasswordHash = pwdHash;
        }

        public String Username { get; internal set; }
        public String Email { get; internal set; }
        public String PasswordHash { get; internal set; }
    }
}
