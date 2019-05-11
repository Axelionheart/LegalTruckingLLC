using LegalTrucking.IntakePlus.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Ports.Commands.Authentication
{
    public class LoginCommand : Command
    {
        public LoginCommand(String username, String pwd) : base(Guid.NewGuid())
        {
            UserName = username;
            Password = pwd;
        }

        public String UserName { get; internal set; }
        public String Password { get; internal set; }
    }
}
