using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Authentication;
using LegalTrucking.IntakePlus.Core.Domain.Common;
using LegalTrucking.IntakePlus.Core.Ports.Commands.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalTrucking.IntakePlus.Core.Ports.Handlers.Authentication
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IRepository<LoginSession, LoginSessionDocument> _repository;

        public LogoutCommandHandler(IRepository<LoginSession, LoginSessionDocument> repo)
        {
            this._repository = repo;
        }

        public async Task<LogoutCommand> HandleAsync(LogoutCommand command)
        {
            LoginSession session = await _repository.GetByIdAsync(command.Id);
            session.LogOut();
            command.Id = session.Id;
            return command;
        }
    }
}
