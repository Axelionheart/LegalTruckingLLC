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
        private readonly IRepository<ServiceRequest, ServiceRequestDocument> repository;
        private readonly IScheduler scheduler;

        public RequestServiceCommandHandler(IScheduler scheduler, IRepository<ServiceRequest, ServiceRequestDocument> repository)
        {
            this.repository = repository;
            this.scheduler = scheduler;
        }

        public  async Task<ServiceRequestCommand> HandleAsync(ServiceRequestCommand command)
        {
            var request = scheduler.Schedule(new ScheduledDate(command.RequestedOn), 
                                                new Id(command.ClientId), 
                                                new Id(command.ServiceId));

            //using (IUnitOfWork uof = unitOfWorkFactory.CreateUnitOfWork())
            //{
            //    repository.UnitOfWork = uof;
            //    repository.Add(request);
            //    uof.Commit();
            //}

            command.Id = request.Id;
            return command;
        }
    }
}
