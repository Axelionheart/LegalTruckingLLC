using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LegalTrucking.IntakePlus.Core.Adapters.Infrastructure.Notifications;
using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Common;
using LegalTrucking.IntakePlus.Core.Domain.Services;
using LegalTrucking.IntakePlus.Core.Ports.Commands.Services;


namespace LegalTrucking.IntakePlus.Core.Ports.Handlers.Services
{
    public class CompleteServiceRequestCommandHandler : IRequestHandler<CompleteServiceRequestCommand>
    {
        private IServiceRequestRepository _repository;
        
        public CompleteServiceRequestCommandHandler(IServiceRequestRepository repository,
            INotifier notifier)
        {
            _repository = repository;
        }

        public async Task<CompleteServiceRequestCommand> HandleAsync(CompleteServiceRequestCommand command)
        {
            var request = await _repository.GetByIdAsync(command.RequestId);
            request.Completed();
            await _repository.UpdateAsync(request);
            return command;
        }
    }
}
