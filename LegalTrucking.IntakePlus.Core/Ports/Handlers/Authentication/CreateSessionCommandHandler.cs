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
    public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand>
    {
        private readonly IRepository<LoginSession, LoginSessionDocument> repository;

        public CreateSessionCommandHandler(IRepository<LoginSession, LoginSessionDocument> repository)
        {
            this.repository = repository;
        }

        public async Task<CreateSessionCommand> HandleAsync(CreateSessionCommand command)
        {
            var session = new LoginSession(
               user: new Id(command.Id),
               created: DateTime.Now
               );

            await this.repository.AddAsync(session);

            command.Id = session.Id;
            return command;
        }
    }
}
