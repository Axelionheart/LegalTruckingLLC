using LegalTrucking.IntakePlus.Core.Domain.Common;
using LegalTrucking.IntakePlus.Core.Ports.Commands.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Ports.Handlers.Authentication
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand>
    {
       
        public Task<LoginCommand> HandleAsync(LoginCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
