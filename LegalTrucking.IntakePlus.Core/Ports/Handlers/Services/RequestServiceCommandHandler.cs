using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LegalTrucking.IntakePlus.Core.Adapters.Repositories;
using LegalTrucking.IntakePlus.Core.Domain.Common;
using LegalTrucking.IntakePlus.Core.Domain.Services;
using LegalTrucking.IntakePlus.Core.Ports.Commands.Services;

namespace LegalTrucking.IntakePlus.Core.Ports.Handlers.Services
{
    public class RequestServiceCommandHandler: IRequestHandler<ServiceRequestCommand>
    {
        private readonly IRepository<ServiceRequest> repository;
        private readonly IScheduler scheduler;
        private readonly IAmAUnitOfWorkFactory unitOfWorkFactory;

        public RequestServiceCommandHandler(IScheduler scheduler, IRepository<ServiceRequest> repository, IAmAUnitOfWorkFactory unitOfWorkFactory)
        {
            this.repository = repository;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.scheduler = scheduler;
        }

        public  ServiceRequestCommand Handle(ServiceRequestCommand command)
        {
            var request = scheduler.Schedule(new ScheduledDate(command.RequestedOn), 
                                                new Id(command.ClientId), 
                                                new Id(command.ServiceId));

            using (IUnitOfWork uof = unitOfWorkFactory.CreateUnitOfWork())
            {
                repository.UnitOfWork = uof;
                repository.Add(request);
                uof.Commit();
            }

            command.Id = request.Id;
            return command;
        }
    }
}
