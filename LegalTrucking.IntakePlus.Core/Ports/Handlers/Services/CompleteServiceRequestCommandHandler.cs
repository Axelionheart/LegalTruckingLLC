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
        private IRepository<ServiceRequest, ServiceRequestDocument> _repository;
        
        public CompleteServiceRequestCommandHandler(IRepository<ServiceRequest, ServiceRequestDocument> repository,
            INotifier notifier)
        {
            _repository = repository;
        }

        public async Task<CompleteServiceRequestCommand> HandleAsync(CompleteServiceRequestCommand command)
        {
          
            return command;
        }
    }
}
